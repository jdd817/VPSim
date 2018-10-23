using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.DW44HandTiers.FourDeuces
{
    public class FourDeuces : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            if (cards.Count(c => c.Value == 2) != 4)
                return HandAction.None;

            var deuceIndicies = new List<int>();

            for (var i = 0; i < cards.Length; i++)
                if (cards[i].Value == 2)
                    deuceIndicies.Add(i);

            return new HandAction
            {
                HandTier = 10101,
                HoldCards = deuceIndicies.ToArray()
            };
        }
    }
}
