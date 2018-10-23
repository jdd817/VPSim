using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPPlayer.DW44HandTiers.TwoDeuces
{
    public class Royal : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 2)
                return HandAction.None;

            var nonWilds = cards.Where(c => c.Value != 2).ToList();
            if (nonWilds.GroupBy(c=>c.Suit).Count()==1)
                return HandAction.None;

            if (nonWilds.All(c => c.Value >= 10 && c.Value <= 14) && nonWilds.Select(c => c.Value).Distinct().Count() == 3)
                return new HandAction
                {
                    HandTier = 10301,
                    HoldCards = new[] { 0, 1, 2, 3, 4 }
                };

            if(nonWilds.Count(c => c.Value >= 10 && c.Value <= 14)==2)
            {
                var holdCards = new List<int>();
                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Value == 2 || (cards[i].Value >= 10 && cards[i].Value <= 14))
                        holdCards.Add(i);
                return new HandAction
                {
                    HandTier = 10304,
                    HoldCards = holdCards.ToArray()
                };
            }

            return HandAction.None;
        }
    }
}
