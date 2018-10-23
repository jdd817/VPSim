using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.NoDeuces
{
    public class SuitedHighCards : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Any(c => c.Value == 2))
                return HandAction.None;

            var suitedHighCards = cards.Where(c => c.Value >= 10 && c.Value <= 13) //ignoring aces
                .GroupBy(c => c.Suit)
                .Where(cs => cs.Count() > 1)
                .Select(cs => cs.OrderBy(c => c.Value).ToList())
                .OrderByDescending(cs => cs.Count())
                .ToList();

            if (suitedHighCards.Count == 0)
                return HandAction.None;

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

        private decimal RankTwoToRoyal(List<Card> cards)
        {
            if (cards.Any(c => c.Value == 13))
                return 10517;

            return 10514;
        }
    }
}
