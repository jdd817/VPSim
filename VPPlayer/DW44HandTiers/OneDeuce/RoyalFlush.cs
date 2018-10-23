using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.OneDeuce
{
    public class RoyalFlush : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 1)
                return HandAction.None;

            var suitedHighCards = cards.Where(c => c.Value >= 10 && c.Value <= 14)
                .GroupBy(c => c.Suit)
                .Where(cs => cs.Count() > 1)
                .Select(cs => cs.OrderBy(c => c.Value).ToList())
                .OrderByDescending(cs => cs.Count())
                .ToList();

            if (suitedHighCards.Count == 0)
                return HandAction.None;

            if(suitedHighCards.Count==1)
            {
                var royalCards = suitedHighCards.First().OrderBy(c => c.Value).ToList();

                var holdCards = new List<int>();
                for (var i = 0; i < cards.Length; i++)
                    if (cards[i].Value == 2 || royalCards.Any(rc => rc == cards[i]))
                        holdCards.Add(i);

                if (royalCards.Count == 4) //pat royal
                    return new HandAction
                    {
                        HandTier = 10401,
                        HoldCards = new[] { 0, 1, 2, 3, 4 }
                    };

                if (royalCards.Count == 3) // 4 to royal
                    return new HandAction
                    {
                        HandTier = 10403,
                        HoldCards = holdCards.ToArray()
                    };

                return new HandAction
                {
                    HandTier = RankTwoToRoyal(royalCards),
                    HoldCards = holdCards.ToArray()
                };
            }

            if(suitedHighCards.Count==2)
            {
                return suitedHighCards.Select(royalCards =>
                {
                    var holdCards = new List<int>();
                    for (var i = 0; i < cards.Length; i++)
                        if (cards[i].Value == 2 || royalCards.Any(rc => rc == cards[i]))
                            holdCards.Add(i);

                    return new HandAction
                    {
                        HandTier = RankTwoToRoyal(royalCards.ToList()),
                        HoldCards = holdCards.ToArray()
                    };
                })
                .OrderBy(ha => ha.HandTier)
                .First();
            }

            return HandAction.None;
        }

        private decimal RankTwoToRoyal(List<Card> cards)
        {
            if ((cards[0].Value == 10 && (cards[1].Value == 11 || cards[1].Value == 12))
                || (cards[0].Value == 11 && cards[1].Value == 12))
                return 10410;

            if ((cards[1].Value == 13 && (cards[0].Value == 10 || cards[0].Value == 11 || cards[0].Value == 12)))
                return 10412;
            
            return 10415;
        }
    }
}
