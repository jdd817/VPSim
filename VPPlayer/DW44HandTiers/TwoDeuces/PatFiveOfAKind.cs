using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.TwoDeuces
{
    public class PatFiveOfAKind : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 2)
                return HandAction.None;

            if(cards.Where(c=>c.Value!=2).GroupBy(c=>c.Value).Count()==1)
                return new HandAction
                {
                    HandTier = 10301,
                    HoldCards = new[] { 0, 1, 2, 3, 4 }
                };

            return HandAction.None;
        }
    }
}
