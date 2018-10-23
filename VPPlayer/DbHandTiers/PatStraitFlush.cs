using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DbHandTiers
{
    public class PatStraitFlush : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            var highestSuit = cards
                .GroupBy(c => c.Suit)
                .Select(x => x.OrderByDescending(c => c.Value).ToList())
                .OrderByDescending(x => x.Count).First();

            if(highestSuit.Count>=5)
            {
                if (cards.Any(c => c.Value == 14))
                    cards = new[] { new Card() { Value = 1 } }.Concat(cards).ToArray();
                cards = cards.Where(c => c.Suit == highestSuit.First().Suit).OrderBy(c => c.Value).ToArray();

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
                if (straitCards.Count >= 5)
                {
                    return new HandAction
                    {
                        HandTier = 2,
                        HoldCards = new[] { 0, 1, 2, 3, 4 }
                    };
                }
            }

            return HandAction.None;
        }
    }
}
