using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DbHandTiers
{
    public class TwoPair : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            var pairs = cards.GroupBy(c => c.Value)
                .Where(p => p.Count() > 1)
                .OrderByDescending(p => p.Key)
                .Select(c => c.ToList())
                .ToList();

            if (pairs.Count < 2)
                return HandAction.None;

            var pair1Value = pairs[0].First().Value;
            var pair2Value = pairs[1].First().Value;

            var cardIndexes = new List<int>();

            for (var i = 0; i < cards.Length; i++)
                if (cards[i].Value == pair1Value || cards[i].Value == pair2Value)
                    cardIndexes.Add(i);

            return new HandAction
            {
                HandTier = 9,
                HoldCards = cardIndexes.ToArray()
            };            
        }
    }
}
