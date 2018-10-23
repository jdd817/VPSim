using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Poker.Hands.DeucesWild
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
                var straitValue = _straitChecker.HandValue(cards.Where(c => c.Suit == flushSuit).Concat(cards.Where(c=>c.Value==2)).Distinct().ToArray());
                if (straitValue > 0)
                {
                    if (straitValue == 4.1413121110m && !cards.Any(c => c.Value == 2))
                        return straitValue + 6;
                    else
                        return straitValue + 4;
                }
            }

            return 0;
        }
    }
}
