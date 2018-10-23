using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DdbHandTiers
{
    public class PatThreeOfAKind : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            var pairs = cards.GroupBy(c => c.Value)
                .Where(p => p.Count() >= 3)
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

            if (pairValue == 14)
                return new HandAction
                {
                    HandTier = 4,
                    HoldCards = pairIndexes.ToArray()
                };

            return new HandAction
            {
                HandTier = 6,
                HoldCards = pairIndexes.ToArray()
            };
        }
    }
}
