using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.NoDeuces
{
    public class Pair : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Any(c => c.Value == 2))
                return HandAction.None;

            var pairs = cards.GroupBy(c => c.Value)
                .Where(p => p.Count() > 1)
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
                HandTier = 10511,
                HoldCards = pairIndexes.ToArray()
            };
        }
    }
}
