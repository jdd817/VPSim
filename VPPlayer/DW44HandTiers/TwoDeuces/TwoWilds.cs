using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.TwoDeuces
{
    public class TwoWilds : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 2)
                return HandAction.None;

            var holdCards = new List<int>();

            for (var i = 0; i < cards.Length; i++)
                if (cards[i].Value == 2)
                    holdCards.Add(i);

            return new HandAction
            {
                HandTier = 10306,
                HoldCards = holdCards.ToArray()
            };
        }
    }
}
