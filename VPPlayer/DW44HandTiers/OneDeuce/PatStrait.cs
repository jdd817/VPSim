using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.OneDeuce
{
    public class PatStrait : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 1)
                return HandAction.None;

            var nonWilds = cards.Where(c => c.Value != 2).ToArray();

            if (nonWilds.Any(c => c.Value == 14))
                nonWilds = new[] { new Card() { Value = 1 } }.Concat(nonWilds).ToArray();
            nonWilds = nonWilds.OrderBy(c => c.Value).ToArray();

            var straitCards = new List<Card>();
            for (var i = 0; i < nonWilds.Length - 1; i++)
            {
                if (nonWilds[i].Value + 1 == nonWilds[i + 1].Value)
                {
                    if (straitCards.Count == 0)
                        straitCards.Add(nonWilds[i]);
                    straitCards.Add(nonWilds[i + 1]);
                }
                else if (nonWilds[i].Value != nonWilds[i + 1].Value)
                {
                    if (straitCards.Count >= 4)
                        break;
                    straitCards.Clear();
                }
            }
            if (straitCards.Count >= 4)
            {
                return new HandAction
                {
                    HandTier = 10406,
                    HoldCards = new[] { 0, 1, 2, 3, 4 }
                };
            }

            return HandAction.None;
        }
    }
}
