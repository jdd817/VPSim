using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.TwoDeuces
{
    public class FourOfAKind : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 2)
                return HandAction.None;

            var pairs = cards
                .Where(c => c.Value != 2)
                .GroupBy(c => c.Value)
                .Select(p => p.ToList());

            var singlePair = pairs.Where(p => p.Count == 2).FirstOrDefault();

            if(singlePair!=null)
            {
                var holdCards = new List<int>();
                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Value == 2 || cards[i].Value == singlePair[0].Value)
                        holdCards.Add(i);
                return new HandAction
                {
                    HandTier = 10303,
                    HoldCards = holdCards.ToArray()
                };
            }

            return HandAction.None;
        }
    }
}
