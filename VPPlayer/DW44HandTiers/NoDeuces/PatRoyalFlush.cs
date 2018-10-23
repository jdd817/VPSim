using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.NoDeuces
{
    public class PatRoyalFlush : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Any(c => c.Value == 2))
                return HandAction.None;

            if (cards.GroupBy(c => c.Suit).Max(s => s.Count()) == 5
                && cards.All(c => c.Value >= 10))
                return new HandAction
                {
                    HandTier = 10501,
                    HoldCards = new[] { 0, 1, 2, 3, 4 }
                };

            return HandAction.None;
        }
    }
}
