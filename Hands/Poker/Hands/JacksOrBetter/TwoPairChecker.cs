using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace Hands.Poker.Hands.JacksOrBetter
{
    public class TwoPairChecker : IHandChecker
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

            var pair1Value = pairs[0].First().Value;
            var pair2Value = pairs[1].First().Value;
            var kicker = cards.Where(c => c.Value != pair1Value && c.Value != pair2Value).Select(c => c.Value).OrderByDescending(v => v).DefaultIfEmpty(0).First();

            return 2 + pair1Value / 100m + pair2Value / 10000m + kicker / 1000000m;
        }
    }
}
