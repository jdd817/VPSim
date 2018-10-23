using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.TwoDeuces
{
    public class StraitFlush : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 2)
                return HandAction.None;

            var nonWilds = cards.Where(c => c.Value != 2).ToList();

            if (nonWilds.GroupBy(c => c.Suit).Count() > 2)
                return HandAction.None;

            nonWilds = nonWilds.GroupBy(c => c.Suit).OrderByDescending(s => s.Count()).Select(s => s.ToList()).First();

            if (nonWilds.Count == 3 && nonWilds.Max(c => c.Value) - nonWilds.Min(c => c.Value) <= 4)
                return new HandAction
                {
                    HandTier = 10302,
                    HoldCards = new[] { 0, 1, 2, 3, 4 }
                };

            for (var i = 0; i < nonWilds.Count; i++)
                for (var j = 0; j < nonWilds.Count; j++)
                    if (nonWilds[i].Value != nonWilds[j].Value
                        && Math.Abs(nonWilds[i].Value - nonWilds[j].Value) <= 4)
                    {
                        var holdCards = new List<int>();
                        for (var k = 0; k < cards.Length; k++)
                            if (cards[k].Value == 2 || cards[k] == nonWilds[i] || cards[k] == nonWilds[j])
                                holdCards.Add(k);
                        return new HandAction
                        {
                            HandTier = 10305,
                            HoldCards = holdCards.ToArray()
                        };
                    }

            return HandAction.None;
        }
    }
}
