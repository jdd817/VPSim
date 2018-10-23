using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace Hands.Poker.Hands.JacksOrBetter
{
    public class StraitChecker : IHandChecker
    {
        public decimal HandValue(Card[] cards)
        {
            //add in an ace as a value 1 to account for the wheel
            if (cards.Any(c => c.Value == 14))
                cards = new[] { new Card() { Value = 1 } }.Concat(cards).ToArray();
            cards = cards.OrderBy(c => c.Value).ToArray();

            var straitCards = new List<Card>();
            for (var i = 0; i < cards.Length - 1; i++)
            {
                if (cards[i].Value + 1 == cards[i + 1].Value)
                {
                    if (straitCards.Count == 0)
                        straitCards.Add(cards[i]);
                    straitCards.Add(cards[i + 1]);
                }
                else if (cards[i].Value != cards[i + 1].Value)
                {
                    if (straitCards.Count >= 5)
                        break;
                    straitCards.Clear();
                }
            }
            if(straitCards.Count >= 5)
            {
                var value = 4m;

                int i, j;
                for (i = straitCards.Count - 1, j = 1; j <= 5; i--, j++)
                    value += straitCards[i].Value / ((decimal)Math.Pow(100.0, j));

                return value;
            }

            return 0;
        }
    }
}
