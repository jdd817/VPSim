using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace Hands.Poker.Hands.JacksOrBetter
{
    public class PairChecker : IHandChecker
    {
        public decimal HandValue(Card[] cards)
        {
            var pairs = cards.GroupBy(c => c.Value)
                .Where(p => p.Count() > 1)
                .OrderByDescending(p => p.Key)
                .Select(c => c.ToList())
                .FirstOrDefault();

            if (pairs == null)
                return 0;

            var pairValue = pairs.First().Value;
            decimal value = 1 + pairValue / 100m;

            var i = 2;
            foreach (var c in cards.Where(c => c.Value != pairValue).OrderByDescending(c=>c.Value))
                if (i <= 4)
                    value += c.Value / ((decimal)Math.Pow(100.0, i++));
            return value;
        }
    }
}
