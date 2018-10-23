using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DdbHandTiers
{
    public class Pair : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
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

            int handTier;
            if (pairValue == 14)
                handTier = 8;
            else if (pairValue == 13)
                handTier = 11;
            else if (pairValue == 11 || pairValue == 12)
                handTier = 13;
            else
                handTier = 17;

            return new HandAction
            {
                HandTier = handTier,
                HoldCards = pairIndexes.ToArray()
            };
        }
    }
}
