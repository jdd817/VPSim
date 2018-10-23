using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace Hands.Poker.Hands.JacksOrBetter
{
    public class StraitFlushChecker : IHandChecker
    {
        private IHandChecker _straitChecker, _flushChecker;

        public StraitFlushChecker()
        {
            _straitChecker = new StraitChecker();
            _flushChecker = new FlushChecker();
        }

        public decimal HandValue(Card[] cards)
        {            
            var flushValue = _flushChecker.HandValue(cards);

            if (flushValue > 0)
            {
                var flushSuit = cards.GroupBy(c => c.Suit).OrderByDescending(c => c.Count()).Select(c => c.Key).First();
                var straitValue = _straitChecker.HandValue(cards.Where(c => c.Suit == flushSuit).ToArray());
                if (straitValue > 0)
                    return straitValue + 4;
            }

            return 0;
        }
    }
}
