using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DdbHandTiers
{
    public class PatQuad : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            var pairs = cards.GroupBy(c => c.Value)
                .Where(p => p.Count() >= 4)
                .OrderByDescending(p => p.Key)
                .Select(c => c.ToList())
                .FirstOrDefault();

            if (pairs == null)
                return HandAction.None;

            var pairValue = pairs.First().Value;
            var pairIndexes = new List<int>();
            for (var i = 0; i < cards.Length; i++)
                if (cards[i].Value == pairValue)
                    pairIndexes.Add(i);

            if (new[] { 2, 3, 4, 14 }.Contains(pairValue))
            {
                var kicker = cards.Where(c => c.Value != pairValue).Select(c => c.Value).First();

                if (new[] { 2, 3, 4, 14 }.Contains(kicker))
                {
                    return new HandAction
                    {
                        HandTier = 1,
                        HoldCards = new[] { 0, 1, 2, 3, 4 }
                    };
                }
            }

            return new HandAction
            {
                HandTier = 2,
                HoldCards = pairIndexes.ToArray()
            };
        }
    }
}
