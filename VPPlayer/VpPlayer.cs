using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPPlayer
{
    public class VpPlayer
    {
        private IHandTier[] _tiers;

        public VpPlayer(IHandTier[] tiers)
        {
            _tiers = tiers;
        }

        public int[] GetHolds(Card[] hand)
        {
            var bestTier = _tiers.Select(t => t.GetHandTier(hand)).OrderBy(t => t.HandTier).FirstOrDefault();

            if (bestTier == null)
                return HandAction.None.HoldCards;
            else
                return bestTier.HoldCards;
        }
    }
}
