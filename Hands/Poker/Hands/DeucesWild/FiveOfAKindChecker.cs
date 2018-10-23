using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Poker.Hands.DeucesWild
{
    public class FiveOfAKindChecker:IHandChecker
    {
        public decimal HandValue(Card[] cards)
        {
            var deuces = cards.Count(c => c.Value == 2);

            var pairs = cards
                .Where(c => c.Value != 2)
                .GroupBy(c => c.Value)
                .Where(p => p.Count() >= 5 - deuces)
                .OrderByDescending(p => p.Key)
                .Select(c => c.ToList())
                .FirstOrDefault();

            if (pairs == null)
                return 0;

            var pairValue = pairs.First().Value;
            return 9 + pairValue / 100m;
        }
    }
}
