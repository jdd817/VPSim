using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.NoDeuces
{
    public class StraitFlush : IHandTier
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
                        HandTier = 10504,
                        HoldCards = new[] { 0, 1, 2, 3, 4 }
                    };
                }
            }

            if (highestSuit.Count >= 3)
            {
                var workingCards = cards;
                if (workingCards.Any(c => c.Value == 14))
                    workingCards = new[] { new Card() { Value = 1 } }.Concat(workingCards).ToArray();
                workingCards = workingCards.Where(c => c.Suit == highestSuit.First().Suit).OrderBy(c => c.Value).ToArray();

                var straitCards = new List<List<Card>>();

                foreach (var card in workingCards)
                {
                    var searchStart = card.Value - 4;
                    if (searchStart < 1)
                        searchStart = 1;
                    for (var i = searchStart; i <= card.Value && i <= 10; i++)
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
                            HandTier = 10506,
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
                          st.gaps==0
                          && st.cards.All(c => c.Value >= 5)
                          && st.cards.All(c => c.Value <= 11))
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
                            HandTier = 10510,
                            HoldCards = cardIndexes.ToArray()
                        };
                    }

                    var type2 = straitTypes.Where(st =>
                          st.gaps <= 2
                          && !st.cards.Any(c => c.Value == 1))
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
                            HandTier = 10513,
                            HoldCards = cardIndexes.ToArray()
                        };
                    }

                    var type3 = straitTypes.Where(st =>
                          st.gaps <= 2)
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
                            HandTier = 10515,
                            HoldCards = cardIndexes.ToArray()
                        };
                    }
                }
            }

            return HandAction.None;
        }
    }
}
