using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.NoDeuces
{
    public class Flush : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Any(c => c.Value == 2))
                return HandAction.None;

            var highestSuit = cards
                .GroupBy(c => c.Suit)
                .Select(x => x.OrderByDescending(c => c.Value).ToList())
                .OrderByDescending(x => x.Count).First();

            if (highestSuit.Count >= 5)
            {
                return new HandAction
                {
                    HandTier = 10504,
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
                    HandTier = 10508,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            return HandAction.None;
        }
    }
}
