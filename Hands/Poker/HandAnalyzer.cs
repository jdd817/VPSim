using Hands.Entities;
using Hands.Poker.Hands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Poker
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Hands are valued as a decimal.  main hand is the whole number, with relative strength being the fractional part.  
    ///  For all hands, simply comparing the numeric value is sufficient to determine relative hand strength.
    ///  Royal flush is not explicitly called out. Its value is 8.1413121110
    /// Whole number hand values:
    /// High Card:    0
    /// Pair:         1
    /// Two Pair:     2
    /// 3 of a Kind:  3
    /// Strait:       4
    /// Flush:        5
    /// Full House:   6
    /// 4 of a Kind:  7
    /// Strait Flush: 8
    /// 
    /// For the fractional part, all card values should be represented as two digits..  ie a pair of 2s, Q 10 8 should be shown as 1.02121008
    /// </remarks>
    public class HandAnalyzer
    {
        private IHandChecker[] _handCheckers;

        public HandAnalyzer(IHandChecker[] handCheckers)
        {
            _handCheckers = handCheckers;
        }

        public decimal GetHandValue(Card[] cards)
        {
            if (cards.Length < 5)
                throw new InvalidOperationException("Not enough cards");
            return _handCheckers
                .Select(c => c.HandValue(cards))
                .Max();
        }
    }
}
