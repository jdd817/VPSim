using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPPlayer
{
    public interface IHandTier
    {
        HandAction GetHandTier(Card[] cards);
    }

    public class HandAction
    {
        public decimal HandTier { get; set; }
        public int[] HoldCards { get; set; }

        public static HandAction None =
                new HandAction
                {
                    HandTier = Decimal.MaxValue,
                    HoldCards = new int[0]
                };
    }
}
