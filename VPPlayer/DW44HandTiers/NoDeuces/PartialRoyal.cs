using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.NoDeuces
{
    public class PartialRoyal : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Any(c => c.Value == 2))
                return HandAction.None;

            var highestSuit = cards
                .GroupBy(c => c.Suit)
                .Select(x => x.OrderByDescending(c => c.Value).ToList())
                .OrderByDescending(x => x.Count).First();

            if (highestSuit.Count >= 3)
            {
                var suit = highestSuit.First().Suit;

                if (cards
                    .Where(c => c.Suit == suit)
                    .Where(c => c.Value >= 10 && c.Value <= 14)
                    .Count() >= 3)
                {
                    var cardIndexes = new List<int>();
                    for (var i = 0; i < cards.Length; i++)
                        if (cards[i].Suit == suit && cards[i].Value >= 10 && cards[i].Value <= 14)
                            cardIndexes.Add(i);
                    return new HandAction
                    {
                        HandTier = cardIndexes.Count == 4 ? 10502 : 10507,
                        HoldCards = cardIndexes.ToArray()
                    };
                }

            }

            return HandAction.None;
        }
    }
}
