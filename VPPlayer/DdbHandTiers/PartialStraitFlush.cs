using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DdbHandTiers
{
    public class PartialStraitFlush : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            var highestSuit = cards
                .GroupBy(c => c.Suit)
                .Select(x => x.OrderByDescending(c => c.Value).ToList())
                .OrderByDescending(x => x.Count).First();

            if (highestSuit.Count >= 3)
            {
                var workingCards=cards;
                if (workingCards.Any(c => c.Value == 14))
                    workingCards = new[] { new Card() { Value = 1 } }.Concat(workingCards).ToArray();
                workingCards = workingCards.Where(c => c.Suit == highestSuit.First().Suit).OrderBy(c => c.Value).ToArray();

                var straitCards = new List<List<Card>>();

                foreach(var card in workingCards)
                {
                    var searchStart = card.Value - 4;
                    if (searchStart < 1)
                        searchStart = 1;
                    for(var i= searchStart;i<=card.Value && i<=10;i++)
                    {
                        var hits = workingCards.Where(c => c.Value >= i && c.Value <= i + 4).ToList();
                        if (hits.Count >= 3)
                            straitCards.Add(hits);
                    }
                }

                if (straitCards.Count > 0)
                {
                    var cardIndexes = new List<int>();
                    var fourTo = straitCards.Where(sc => sc.Count >= 4).OrderByDescending(sc => sc.Max(c => c.Value)).FirstOrDefault();
                    if (fourTo != null)
                    {
                        for (var i = 0; i < cards.Length; i++)
                            if (fourTo.Any(c => c == cards[i]))
                                cardIndexes.Add(i);

                        return new HandAction
                        {
                            HandTier = 7,
                            HoldCards = cardIndexes.ToArray()
                        };
                    }

                    var straitTypes = straitCards.Select(sc =>
                      new
                      {
                          gaps = (sc.Max(c => c.Value) - sc.Min(c => c.Value)) - 2,
                          highCardCount = sc.Where(c => c.Value >= 11 || c.Value == 1).Count(),
                          cards = sc
                      });

                    var type1 = straitTypes.Where(st =>
                          st.highCardCount >= st.gaps
                          && !st.cards.Any(c => c.Value == 1)
                          && !(st.cards.Any(c => c.Value == 2) && st.cards.Any(c => c.Value == 3) && st.cards.Any(c => c.Value == 4)))
                          .OrderBy(st => st.cards.Max(c => c.Value))
                          .Select(st => st.cards)
                          .FirstOrDefault();

                    if (type1 != null)
                    {
                        for (var i = 0; i < cards.Length; i++)
                            if (type1.Any(c => c == cards[i]))
                                cardIndexes.Add(i);
                        return new HandAction
                        {
                            HandTier = 19,
                            HoldCards = cardIndexes.ToArray()
                        };
                    }

                    var type2 = straitTypes.Where(st =>
                          st.highCardCount >= st.gaps - 1
                          || st.cards.Any(c => c.Value == 1)
                          || (st.cards.Any(c => c.Value == 2) && st.cards.Any(c => c.Value == 3) && st.cards.Any(c => c.Value == 4)))
                          .OrderBy(st => st.cards.Max(c => c.Value))
                          .Select(st => st.cards)
                          .FirstOrDefault();

                    if (type2 != null)
                    {
                        for (var i = 0; i < cards.Length; i++)
                            if (type2.Any(c => c == cards[i]))
                                cardIndexes.Add(i);
                        return new HandAction
                        {
                            HandTier = 23,
                            HoldCards = cardIndexes.ToArray()
                        };
                    }

                    var type3 = straitTypes.Where(st =>
                          st.highCardCount >= st.gaps - 2)
                        .OrderBy(st => st.cards.Max(c => c.Value))
                          .Select(st => st.cards)
                          .FirstOrDefault();

                    if (type3 != null)
                    {
                        for (var i = 0; i < cards.Length; i++)
                            if (type3.Any(c => c == cards[i]))
                                cardIndexes.Add(i);
                        return new HandAction
                        {
                            HandTier = 33,
                            HoldCards = cardIndexes.ToArray()
                        };
                    }
                }
            }

            return HandAction.None;
        }
    }
}
