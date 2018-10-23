using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace Hands.Poker.Hands.JacksOrBetter
{
    public class FullHouseChecker : IHandChecker
    {
        public decimal HandValue(Card[] cards)
        {
            var pairs = cards.GroupBy(c => c.Value)
                .Where(p => p.Count() > 1)
                .OrderByDescending(p => p.Key)
                .Select(c => c.ToList())
                .ToList();

            if (pairs.Count < 2)
                return 0;

            if (!pairs.Any(p => p.Count >= 3))
                return 0;

            var tripValue = pairs.Where(p=>p.Count >= 3).First().First().Value;
            var pairValue = pairs.Where(p => p.First().Value != tripValue).First().First().Value;

            return 6 + tripValue / 100m + pairValue / 10000m;
        }
    }
}
