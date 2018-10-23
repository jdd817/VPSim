using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.OneDeuce
{
    public class Flush : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 1)
                return HandAction.None;

            var highestSuit = cards
                .Where(c => c.Value != 2)
                .GroupBy(c => c.Suit)
                .Select(x => x.OrderByDescending(c => c.Value).ToList())
                .OrderByDescending(x => x.Count).First();

            if (highestSuit.Count >= 4)
            {
                return new HandAction
                {
                    HandTier = 10404,
                    HoldCards = new[] { 0, 1, 2, 3, 4 }
                };
            }

            return HandAction.None;
        }
    }
}
