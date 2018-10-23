using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.OneDeuce
{
    public class FiveOfAKind : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 1)
                return HandAction.None;

            if (cards.GroupBy(c => c.Value).Max(cg => cg.Count()) == 4)
                return new HandAction
                {
                    HandTier = 10401,
                    HoldCards = new[] { 0, 1, 2, 3, 4 }
                };

            return HandAction.None;
        }
    }
}
