using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DbHandTiers
{
    public class FourToRoyal : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
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
                    .Count()>=3)
                {
                    var cardIndexes = new List<int>();
                    for (var i = 0; i < cards.Length; i++)
                        if (cards[i].Suit == suit && cards[i].Value >= 10 && cards[i].Value <= 14)
                            cardIndexes.Add(i);
                    if (cardIndexes.Count == 4)
                        return new HandAction
                        {
                            HandTier = 3,
                            HoldCards = cardIndexes.ToArray()
                        };
                    if (cardIndexes.Count == 3)
                    {
                        if (cards.Any(c => c.Suit == suit && c.Value == 11)
                            && cards.Any(c => c.Suit == suit && c.Value == 12)
                            && cards.Any(c => c.Suit == suit && c.Value == 13))
                            return new HandAction
                            {
                                HandTier = 1,
                                HoldCards = cardIndexes.ToArray()
                            };
                        if (cards.Any(c => c.Suit == suit && c.Value == 10)
                            && cards.Any(c => c.Suit == suit && c.Value == 11)
                            && cards.Any(c => c.Suit == suit && c.Value == 12))
                            return new HandAction
                            {
                                HandTier = 1,
                                HoldCards = cardIndexes.ToArray()
                            };
                        return new HandAction
                        {
                            HandTier = 13,
                            HoldCards = cardIndexes.ToArray()
                        };
                    }
                }
                
            }

            return HandAction.None;
        }
    }
}
