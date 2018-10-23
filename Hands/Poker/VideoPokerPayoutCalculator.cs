using Hands.Entities;
using Hands.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Poker
{
    public class VideoPokerPayoutCalculator
    {
        private HandAnalyzer _handAnalyzer;

        public VideoPokerPayoutCalculator(HandAnalyzer handAnalyzer)
        {
            _handAnalyzer = handAnalyzer;
        }

        public VideoPokerPayout GetPayout(PayTable paytable, Card[] hand, int creditsBet)
        {
            var handValue = _handAnalyzer.GetHandValue(hand);

            var payLine = paytable.PayLines
                .Where(pl => pl.MinValue <= handValue && pl.MaxValue >= handValue)
                .OrderByDescending(pl => pl.Payout)
                .FirstOrDefault();

            if (payLine == null)
                return new VideoPokerPayout { Payout = 0, PayLine = null };


            var result = new VideoPokerPayout { PayLine = payLine };

            if (creditsBet >= 5)
                result.Payout = (int)(creditsBet * payLine.Payout * payLine.MaxBetMultiplier);
            else
                result.Payout = creditsBet * payLine.Payout;

            return result;
        }
    }

    public class VideoPokerPayout
    {
        public int Payout { get; set; }
        public PayLine PayLine { get; set; }
    }
}
