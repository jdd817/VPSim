using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Poker.Hands.DeucesWild
{
    public class FourOfAKindChecker : IHandChecker
    {
        public decimal HandValue(Card[] cards)
        {
            var deuces = cards.Count(c => c.Value == 2);

            if (deuces == 4)  //make sure 4 dueces isnt overriden with a diffent quad
                return 7.02m;

            var pairs = cards
                .Where(c => c.Value != 2)
                .GroupBy(c => c.Value)
                .Where(p => p.Count() >= 4 - deuces)
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

