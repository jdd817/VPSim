using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace Hands.Poker.Hands.JacksOrBetter
{
    public class FlushChecker : IHandChecker
    {
        public decimal HandValue(Card[] cards)
        {
            var highestSuit = cards
                .GroupBy(c => c.Suit)
                .Select(x => x.OrderByDescending(c => c.Value).ToList())
                .OrderByDescending(x => x.Count).First();

            if(highestSuit.Count>=5)
            {
                decimal value = 5;
                for (var i = 1; i <= 5; i++)
                    value += highestSuit[i - 1].Value / ((decimal)Math.Pow(100.0, i));
                return value;
            }
            return 0;
        }
    }
}
