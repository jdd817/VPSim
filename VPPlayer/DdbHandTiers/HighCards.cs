using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DdbHandTiers
{
    public class HighCards : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            var cardIndexes = new List<int>();

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
                    HandTier = 21,
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
                    HandTier = 24,
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
                    HandTier = 26,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            if (cards.Any(c => c.Value == 14))
            {
                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Value == 14)
                        cardIndexes.Add(i);
                return new HandAction
                {
                    HandTier = 27,
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
                    HandTier = 29,
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
                    HandTier = 29,
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
                    HandTier = 32,
                    HoldCards = cardIndexes.ToArray()
                };
            }

            return HandAction.None;
        }
    }
}
