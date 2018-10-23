using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Poker.Hands.DeucesWild
{
    public class StraitChecker : IHandChecker
    {
        public decimal HandValue(Card[] cards)
        {
            var deuces = cards.Count(c => c.Value == 2);

            //add in an ace as a value 1 to account for the wheel
            if (cards.Any(c => c.Value == 14))
                cards = new[] { new Card() { Value = 1 } }.Concat(cards).ToArray();
            //remove any deuces
            cards = cards.Where(c => c.Value != 2).OrderBy(c => c.Value).ToArray();

            var straitCards = new List<Card>();
            var currentDeuces = deuces;
            var currentValue = cards[0].Value;
            straitCards.Add(cards[0]);
            for (var i = 1; i < cards.Length; i++)
            {
                if (currentValue + 1 == cards[i].Value)
                {
                    straitCards.Add(cards[i]);
                }
                else if (currentDeuces > 0)
                {
                    straitCards.Add(new Card { Value = currentValue + 1 });
                    currentValue++;
                    currentDeuces--;
                    i--;
                    continue;
                }
                else
                {
                    if (straitCards.Count + currentDeuces >= 5)
                        break;
                    straitCards.Clear();
                    currentDeuces = deuces;
                    straitCards.Add(cards[i]);
                }
                currentValue = cards[i].Value;
            }
            if (straitCards.Count + currentDeuces >= 5)
            {
                var value = 4m;

                var highestCard = straitCards.Max(s => s.Value) + currentDeuces;
                if (highestCard > 14)
                    highestCard = 14;

                int i, j;
                for (i = highestCard, j = 1; j <= 5; i--, j++)
                    value += i / ((decimal)Math.Pow(100.0, j));

                return value;
            }

            return 0;
        }
    }
}
