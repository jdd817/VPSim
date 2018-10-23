using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.ThreeDeuces
{
    public class PatRoyalFlush : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 3)
                return HandAction.None;

            var nonWilds = cards.Where(c => c.Value != 2).ToList();
            if (nonWilds.GroupBy(c => c.Suit).Count() != 1)
                return HandAction.None;

            if (nonWilds.All(c=>c.Value>=10 && c.Value<=14))
                return new HandAction
                {
                    HandTier = 10201,
                    HoldCards = new[] { 0, 1, 2, 3, 4 }
                };

            return HandAction.None;
        }
    }
}
