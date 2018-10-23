using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.NoDeuces
{
    public class Strait : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Any(c => c.Value == 2))
                return HandAction.None;

            var workingCards = cards.OrderBy(c=>c.Value).ToArray();

            var straitCards = new List<List<Card>>();

            foreach (var card in workingCards)
            {
                var searchStart = card.Value - 4;
                if (searchStart < 3)
                    searchStart = 3;
                for (var i = searchStart; i <= card.Value && i <= 10; i++)
                {
                    var hits = workingCards.Where(c => c.Value >= i && c.Value <= i + 4).ToList();
                    if (hits.Count >= 4)
                        straitCards.Add(hits);
                }
            }

            if (straitCards.Count > 0)
            {
                if (straitCards.Any(sc => sc.Count() == 5))
                    return new HandAction
                    {
                        HandTier = 10504,
                        HoldCards = new[] { 0, 1, 2, 3, 4 }
                    };

                var cardIndexes = new List<int>();

                var straitTypes = straitCards.Select(sc =>
                  new
                  {
                      gaps = (sc.Max(c => c.Value) - sc.Min(c => c.Value)) - 2,
                      highCardCount = sc.Where(c => c.Value >= 11 || c.Value == 1).Count(),
                      cards = sc
                  });

                var outside = straitCards.Where(sc =>
                    (sc.Max(c => c.Value) - sc.Min(c => c.Value) < 4)
                    && !(sc.Any(c => c.Value == 14) || sc.Any(c => c.Value == 1)))
                    .OrderBy(sc => sc.Max(c => c.Value))
                    .FirstOrDefault();

                if (outside != null)
                {
                    for (var i = 0; i < cards.Length; i++)
                        if (outside.Any(c => c == cards[i]))
                            cardIndexes.Add(i);
                    return new HandAction
                    {
                        HandTier = 10512,
                        HoldCards = cardIndexes.ToArray()
                    };
                }

                var inside = straitCards.Where(sc =>
                    (sc.Max(c => c.Value) - sc.Min(c => c.Value) >= 4))
                    .OrderBy(sc => sc.Max(c => c.Value))
                    .FirstOrDefault();

                if (inside != null)
                {
                    for (var i = 0; i < cards.Length; i++)
                        if (inside.Any(c => c == cards[i]))
                            cardIndexes.Add(i);
                    return new HandAction
                    {
                        HandTier = 10516,
                        HoldCards = cardIndexes.ToArray()
                    };
                }
            }
            return HandAction.None;
        }
    }
}
