using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Poker.Hands.DeucesWild
{
    public class FlushChecker : IHandChecker
    {
        public decimal HandValue(Card[] cards)
        {
            var highestSuit = cards
                .Where(c => c.Value != 2)
                .GroupBy(c => c.Suit)
                .Select(x => x.OrderByDescending(c => c.Value).ToList())
                .OrderByDescending(x => x.Count).First();

            var deuces = cards.Count(c => c.Value == 2);

            if (highestSuit.Count + deuces >= 5)
            {
                decimal value = 5;
                for (var i = 1; i <= highestSuit.Count; i++)
                    value += highestSuit[i - 1].Value / ((decimal)Math.Pow(100.0, i));
                return value;
            }
            return 0;
        }
    }
}
