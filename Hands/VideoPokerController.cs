using Hands.Entities;
using Hands.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands
{
    public class VideoPokerController
    {
        private Deck _deck;
        private VideoPokerPayoutCalculator _payoutCalculator;

        public VideoPokerController(VideoPokerPayoutCalculator payoutCalculator)
        {
            _deck = new Deck();
            _payoutCalculator = payoutCalculator;
        }

        public Card[] DealHand()
        {
            _deck.Reset();
            return _deck.Draw(5);
        }

        public void SetHand(Card[] hand)
        {
            _deck.Reset();
            _deck.Remove(hand);
        }

        public HandResult ResolveHand(Card[] heldCards, int bet, PayTable payTable)
        {
            _deck.Shuffle();
            var newCards = _deck.Peek(5 - heldCards.Length);

            var finalHand = newCards.Concat(heldCards).ToArray();

            var payout = _payoutCalculator.GetPayout(payTable, finalHand, bet);

            if (payout.PayLine == null)
                return new HandResult
                {
                    PayLineHit = "None",
                    Payout = 0,
                    Hand = finalHand
                };
            else
                return new HandResult
                {
                    PayLineHit = payout.PayLine.Name,
                    Payout = payout.Payout,
                    Hand = finalHand
                };
        }
    }

    public class HandResult
    {
        public string PayLineHit { get; set; }
        public int Payout { get; set; }
        public Card[] Hand { get; set; }
    }
}
