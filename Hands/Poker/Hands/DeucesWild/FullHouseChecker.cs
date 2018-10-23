using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Poker.Hands.DeucesWild
{
    public class FullHouseChecker : IHandChecker
    {
        public decimal HandValue(Card[] cards)
        {
            var deuces = cards.Count(c => c.Value == 2);

            var cardGroups = cards
                .Where(c => c.Value != 2)
                .GroupBy(c => c.Value)
                .OrderByDescending(p => p.Key)
                .Select(c => c.ToList())
                .ToList();

            var trips = cardGroups
                .Where(p => p.Count >= 3 - deuces)
                .OrderByDescending(p => p.Count)
                .ThenByDescending(p => p.First().Value);

            if (!trips.Any())
                return 0;

            var tripValue=trips
                .First().First().Value;
            var deucesLeft = deuces - (3 - Math.Min(3, cards.Count(c => c.Value == tripValue)));
            var pairs = cardGroups
                .Where(p =>
                    p.First().Value != tripValue
                    && p.Count >= 2 - deucesLeft);

            if (!pairs.Any())
                return 0;

            var pairValue = pairs
                .First().First().Value;

            return 6 + tripValue / 100m + pairValue / 10000m;
        }
    }
}
