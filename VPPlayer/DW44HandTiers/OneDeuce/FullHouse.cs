using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.OneDeuce
{
    public class FullHouse : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 1)
                return HandAction.None;

            var pairs = cards
                .Where(c=>c.Value!=2)
                .GroupBy(c => c.Value)
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
                HandTier = 10401,
                HoldCards = new[] { 0, 1, 2, 3, 4 }
            };
        }
    }
}
