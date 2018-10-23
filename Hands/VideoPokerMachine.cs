using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands
{
    public class VideoPokerMachine
    {
        private int _bet;
                
        private Card[] _hand;

        private bool[] _holds;

        private int _state;

        private VideoPokerController _controller;
        private PayTable _payTable;

        public int Credits { get; set; }

        public VideoPokerMachine(VideoPokerController controller)
        {
            _holds = new[] { false, false, false, false, false };
            _state = 0;
            HandsBet = 1;
            _controller = controller;
            Results = new List<HandResult>();
        }

        public void Hold(int cardIndex)
        {
            if (_state == 1)
            {
                if (cardIndex < 0 || cardIndex > 4)
                    throw new InvalidOperationException("Bad card index");
                _holds[cardIndex] = !_holds[cardIndex];
            }
        }

        public void Deal()
        {
            if(_state==0)
            {
                if (Credits < _bet * HandsBet)
                    return;
                Credits -= _bet * HandsBet;
                _holds = new[] { false, false, false, false, false };
                _hand = _controller.DealHand();
                _state = 1;
            }
            else if(_state==1)
            {
                var heldCards = GetHeldCards().ToArray();
                Results.Clear();
                for(var i=0;i< HandsBet; i++)
                {
                    var newResult = _controller.ResolveHand(heldCards, _bet, _payTable);
                    if (i == 0)
                        _hand = newResult.Hand;
                    Credits += newResult.Payout;
                    Results.Add(newResult);
                }
                _state = 0;
            }
        }

        public HandResult CheckCurrentHand()
        {
            return _controller.ResolveHand(_hand, _bet, _payTable);
        }

        public void Bet()
        {
            if(_state==0)
            {
                _bet++;
                if (_bet > 5)
                    _bet -= 5;
            }
        }

        public void MaxBet()
        {
            if (State == 0)
            {
                _bet = 5;
                Deal();
            }
        }

        public void GameSelect(PayTable paytable)
        {
            _payTable = paytable;
        }

        private IEnumerable<Card> GetHeldCards()
        {
            for (var i = 0; i < 5; i++)
                if (_holds[i])
                    yield return _hand[i];
        }

        public Card[] Hand
        {
            get { return _hand; }
        }
        public bool[] Holds
        {
            get { return _holds; }
        }
        public int State
        {
            get { return _state; }
        }
        public int BetPerHand
        {
            get { return _bet; }
        }
        public int HandsBet { get; set; }
        public List<HandResult> Results { get; private set; }
    }
}
