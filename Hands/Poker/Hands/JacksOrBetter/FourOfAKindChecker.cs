using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace Hands.Poker.Hands.JacksOrBetter
{
    public class FourOfAKindChecker : IHandChecker
    {
        public decimal HandValue(Card[] cards)
        {
            var pairs = cards.GroupBy(c => c.Value)
                .Where(p => p.Count() >= 4)
                .OrderByDescending(p => p.Key)
                .Select(c => c.ToList())
                .FirstOrDefault();

            if (pairs == null)
                return 0;

            var pairValue = pairs.First().Value;
            var kicker = cards.Where(c => c.Value != pairValue).OrderByDescending(c => c.Value).First().Value;
            return 7 + pairValue / 100m + kicker / 10000m;
        }
    }
}
