using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DbHandTiers
{
    public class Flush : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            var highestSuit = cards
                .GroupBy(c => c.Suit)
                .Select(x => x.OrderByDescending(c => c.Value).ToList())
                .OrderByDescending(x => x.Count).First();

            if (highestSuit.Count >= 5)
            {
                return new HandAction
                {
                    HandTier = 5,
                    HoldCards = new[] { 0, 1, 2, 3, 4 }
                };
            }

            if (highestSuit.Count >= 4)
            {
                var cardIndexes = new List<int>();
                var suit = highestSuit.First().Suit;

                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Suit == suit)
                        cardIndexes.Add(i);

                return new HandAction
                {
                    HandTier = 10,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            if(highestSuit.Count==3)
            {
                var suit = highestSuit.First().Suit;
                
                var highCardCount = cards.Where(c => c.Suit == suit && c.Value >= 11).Count();
                    var cardIndexes = new List<int>();
                    for (var i = 0; i < cards.Length; i++)
                        if (cards[i].Suit == suit)
                            cardIndexes.Add(i);

                return new HandAction
                {
                    HandTier = highCardCount >= 2 ? 22 : (highCardCount >= 1 ? 31 : 40),
                    HoldCards = cardIndexes.ToArray()
                };
            }

            return HandAction.None;
        }
    }
}
