using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Poker.Hands
{
    public interface IHandChecker
    {
        /// <summary>
        /// will return a value only if the hand is met.  will return 0 otherwise.  ie, for a full house, 88552 will return 0, 88555 will return 6.0508
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        decimal HandValue(Card[] cards);
    }
}
