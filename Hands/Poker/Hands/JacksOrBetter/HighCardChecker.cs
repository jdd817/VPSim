using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace Hands.Poker.Hands.JacksOrBetter
{
    public class HighCardChecker : IHandChecker
    {
        public decimal HandValue(Card[] cards)
        {
            cards = cards.OrderByDescending(c => c.Value).ToArray();
            decimal value = 0;
            for (var i = 1; i <= 5; i++)
                value += cards[i - 1].Value / ((decimal)Math.Pow(100.0, i));
            return value;
        }
    }
}
