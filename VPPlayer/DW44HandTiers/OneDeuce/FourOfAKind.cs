using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPPlayer.DW44HandTiers.OneDeuce
{
    public class FourOfAKind : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 1)
                return HandAction.None;

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

            return new HandAction
            {
                HandTier = 10402,
                HoldCards = pairIndexes.ToArray()
            };
        }
    }
}
