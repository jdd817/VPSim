using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Poker.Hands.DeucesWild
{
    public class TwoPairChecker : IHandChecker
    {
        public decimal HandValue(Card[] cards)
        {
            var deuces = cards.Count(c => c.Value == 2);

            var pairs = cards.GroupBy(c => c.Value)
                .Where(p => p.Count() > 1)
                .OrderByDescending(p => p.Key)
                .Select(c => c.ToList())
                .ToList();

            if (pairs.Count < 2)
            {
                if (pairs.Count == 1 && deuces >= 1)
                {
                    pairs = pairs.Concat(
                            new[]
                            {
                                cards.Where(c => c.Value != pairs[0].First().Value)
                                    .OrderBy(c => c.Value)
                                    .Take(1)
                                    .ToList()
                            })
                        .ToList();
                }
                else if (pairs.Count == 0 && deuces >= 2)
                {
                    pairs = cards.Where(c => c.Value != 2)
                        .GroupBy(c => c.Value)
                        .OrderBy(c => c.Key)
                        .Select(c => c.ToList())
                        .Take(2)
                        .ToList();
                }
                else
                    return 0;
            }

            var pair1Value = pairs[0].First().Value;
            var pair2Value = pairs[1].First().Value;
            var kicker = cards.Where(c => c.Value != pair1Value && c.Value != pair2Value).Select(c => c.Value).OrderByDescending(v => v).DefaultIfEmpty(0).First();

            return 2 + pair1Value / 100m + pair2Value / 10000m + kicker / 1000000m;
        }
    }
}
