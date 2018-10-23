using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.NoDeuces
{
    public class FullHouse : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Any(c => c.Value == 2))
                return HandAction.None;

            var pairs = cards.GroupBy(c => c.Value)
                .Where(p => p.Count() > 1)
                .OrderByDescending(p => p.Key)
                .Select(c => c.ToList())
                .ToList();

            if (pairs.Count < 2)
                return HandAction.None;

            if (!pairs.Any(p => p.Count >= 3))
                return HandAction.None;

            return new HandAction
            {
                HandTier = 10501,
                HoldCards = new[] { 0, 1, 2, 3, 4 }
            };
        }
    }
}
