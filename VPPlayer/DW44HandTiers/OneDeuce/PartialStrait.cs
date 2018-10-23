using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.OneDeuce
{
    public class PartialStrait : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 1)
                return HandAction.None;

            var nonWilds = cards.Where(c => c.Value != 2).OrderBy(c => c.Value).ToArray();

            var cardList = new[]
            {
                new[] {5,6,7},
                new[] {6,7,8},
                new[] {7,8,9},
                new[] {8,9,10},
                new[] {9,10,11},
                new[] {10,11,12},
            };

            foreach(var list in cardList)
            {
                var foundCards = FindCards(nonWilds, list).ToList();

                if(foundCards.Count==3)
                {
                    return new HandAction
                    {
                        HandTier = 10416,
                        HoldCards = GetIndexes(cards, foundCards)
                    };
                }
            }

            return HandAction.None;
        }

        private IEnumerable<Card> FindCards(Card[] cards, params int[] values)
        {
            for(var i=0;i<values.Length;i++)
            {
                var card = cards.FirstOrDefault(c => c.Value == values[i]);
                if (card != null)
                    yield return card;
            }
        }

        private int[] GetIndexes(Card[] hand, List<Card> holdCards)
        {
            var holds = new List<int>();
            for (var i = 0; i < hand.Length; i++)
                if (hand[i].Value == 2 || holdCards.Any(c => hand[i] == c))
                    holds.Add(i);
            return holds.ToArray();
        }
    }
}
