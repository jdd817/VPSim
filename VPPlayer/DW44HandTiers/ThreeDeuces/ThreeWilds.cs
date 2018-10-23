using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.ThreeDeuces
{
    public class ThreeWilds : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 3)
                return HandAction.None;

            var deuces = new List<int>();

            for (var i = 0; i < cards.Length; i++)
                if (cards[i].Value == 2)
                    deuces.Add(i);

            return new HandAction
            {
                HandTier = 10202,
                HoldCards = deuces.ToArray()
            };
        }
    }
}
