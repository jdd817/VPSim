using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPPlayer.DW44HandTiers.OneDeuce
{
    public class StraitFlush:IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 1)
                return HandAction.None;

            var suitedCards = cards.Where(c => c.Value != 2)
                .GroupBy(c => c.Suit)
                .Where(cs => cs.Count() > 1)
                .OrderByDescending(cs => cs.Count())
                .ToList();

            if (suitedCards.Count == 0)
                return HandAction.None;

            if (suitedCards.Count == 1)
            {
                var nonWilds = suitedCards[0].ToList();

                if (nonWilds.Count == 4 &&
                    nonWilds.Max(c => c.Value) - nonWilds.Min(c => c.Value) <= 4)
                    return new HandAction
                    {
                        HandTier = 10401,
                        HoldCards = new[] { 0, 1, 2, 3, 4 }
                    };

                var straitCards = new List<List<Card>>();

                foreach (var card in nonWilds)
                {
                    var searchStart = card.Value - 4;
                    if (searchStart < 1)
                        searchStart = 1;
                    for (var i = searchStart; i <= card.Value && i <= 10; i++)
                    {
                        var hits = nonWilds.Where(c => c.Value >= i && c.Value <= i + 4).ToList();
                        if (hits.Count >= 2)
                            straitCards.Add(hits.OrderBy(c => c.Value).ToList());
                    }
                }

                if (straitCards.Count == 0)
                    return HandAction.None;
                
                if (straitCards.Any(sc=>sc.Count==3))  //4 to a SF
                {
                    return straitCards.Where(sc => sc.Count == 3)
                        .Select(holds =>
                            new HandAction
                            {
                                HandTier = holds.Any(c => c.Value == 14) ? 10409 : 10407,
                                HoldCards = GetIndexes(cards, holds)
                            })
                        .OrderBy(ha => ha.HandTier)
                        .First();
                }

                if(straitCards.Any(sc=>sc.Count==2))
                {
                    var action = straitCards.Where(sc => sc.Count == 2)
                        .Select(holds =>
                            new HandAction
                            {
                                HandTier = RankTwoCards(holds),
                                HoldCards = GetIndexes(cards, holds)
                            })
                        .Where(ha => ha.HandTier < Decimal.MaxValue)
                        .OrderByDescending(ha => ha.HandTier)
                        .FirstOrDefault();

                    if (action != null)
                        return action;
                }
            }

            if (suitedCards.Count == 2)
            {
                var straitCards = new List<List<Card>>();

                foreach (var nonWilds in suitedCards)
                {
                    foreach (var card in nonWilds)
                    {
                        var searchStart = card.Value - 4;
                        if (searchStart < 1)
                            searchStart = 1;
                        for (var i = searchStart; i <= card.Value && i <= 10; i++)
                        {
                            var hits = nonWilds.Where(c => c.Value >= i && c.Value <= i + 4).ToList();
                            if (hits.Count >= 2)
                                straitCards.Add(hits.OrderBy(c => c.Value).ToList());
                        }
                    }
                }

                var action = straitCards.Where(sc => sc.Count == 2)
                        .Select(holds =>
                            new HandAction
                            {
                                HandTier = RankTwoCards(holds),
                                HoldCards = GetIndexes(cards, holds)
                            })
                        .Where(ha => ha.HandTier < Decimal.MaxValue)
                        .OrderByDescending(ha => ha.HandTier)
                        .FirstOrDefault();

                if (action != null)
                    return action;
            }

            return HandAction.None;
        }

        private int[] GetIndexes(Card[] hand, List<Card> holdCards)
        {
            var holds = new List<int>();
            for (var i = 0; i < hand.Length; i++)
                if (hand[i].Value == 2 || holdCards.Any(c => hand[i] == c))
                    holds.Add(i);
            return holds.ToArray();
        }

        private decimal RankTwoCards(List<Card> cards)
        {
            if (cards[0].Value >= 6 && cards[0].Value <= 9)
            {
                if (cards[0].Value + 1 == cards[1].Value)
                    return 10411;
                if (cards[0].Value + 2 == cards[1].Value)
                    return 10413;
            }

            if ((cards[0].Value >= 4 && cards[0].Value <= 5) && (cards[1].Value - cards[0].Value) <= 2)
                return 10414;

            return Decimal.MaxValue;
        }
    }
}
