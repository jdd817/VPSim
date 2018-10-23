using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.JobHandTiers
{
    public class HighCards : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            var cardIndexes = new List<int>();
            if (cards.Any(c => c.Value == 14)
                && cards.Any(c => c.Value == 13)
                && cards.Any(c => c.Value == 12)
                && cards.Any(c => c.Value == 11))
            {
                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Value >= 11)
                        cardIndexes.Add(i);
                return new HandAction
                {
                    HandTier = 12,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            var highCards = cards.Where(c => c.Value >= 11);

            var suitedHighcards = highCards.GroupBy(c => c.Suit).Where(shc => shc.Count() > 1)
                .OrderBy(shc => shc.Max(c => c.Value) - shc.Min(c => c.Value))
                .FirstOrDefault();

            if(suitedHighcards!=null)
            {
                for (var i = 0; i < cards.Length; i++)
                    if (suitedHighcards.Any(c => c == cards[i]))
                        cardIndexes.Add(i);
                return new HandAction
                {
                    HandTier = 13,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            if (cards.Any(c => c.Value == 13)
                && cards.Any(c => c.Value == 12)
                && cards.Any(c => c.Value == 11))
            {
                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Value >= 11 && cards[i].Value < 14)
                        cardIndexes.Add(i);
                return new HandAction
                {
                    HandTier = 16,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            if (cards.Any(c => c.Value == 12)
                && cards.Any(c => c.Value == 11))
            {
                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Value >= 11 && cards[i].Value < 13)
                        cardIndexes.Add(i);
                return new HandAction
                {
                    HandTier = 17,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            if (cards.Any(c => c.Value == 13)
                && cards.Any(c => c.Value == 12))
            {
                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Value >= 12 && cards[i].Value <= 13)
                        cardIndexes.Add(i);
                return new HandAction
                {
                    HandTier = 19,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            if (cards.Any(c => c.Value == 13)
                && cards.Any(c => c.Value == 11))
            {
                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Value == 11 || cards[i].Value == 13)
                        cardIndexes.Add(i);
                return new HandAction
                {
                    HandTier = 19,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            if (cards.Any(c => c.Value == 14)
                && cards.Any(c => c.Value == 13))
            {
                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Value == 14 || cards[i].Value == 13)
                        cardIndexes.Add(i);
                return new HandAction
                {
                    HandTier = 21,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            if (cards.Any(c => c.Value == 14)
                && cards.Any(c => c.Value == 12))
            {
                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Value == 14 || cards[i].Value == 12)
                        cardIndexes.Add(i);
                return new HandAction
                {
                    HandTier = 21,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            if (cards.Any(c => c.Value == 14)
                && cards.Any(c => c.Value == 11))
            {
                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Value == 14 || cards[i].Value == 11)
                        cardIndexes.Add(i);
                return new HandAction
                {
                    HandTier = 21,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            if (cards.Any(c => c.Value >= 11))
            {
                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Value >= 11)
                        cardIndexes.Add(i);
                return new HandAction
                {
                    HandTier = 23,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            return HandAction.None;
        }
    }
}
