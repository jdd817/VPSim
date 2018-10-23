using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Entities;

namespace VPPlayer.JobHandTiers
{
    public class HighCardTenSuited : IHandTier
    {
        public HandAction GetHandTier(Card[] cards)
        {
            var ten = cards.Where(c => c.Value == 10).FirstOrDefault();
            if(ten!=null)
            {
                var cardIndexes = new List<int>();

                var jack = cards.Where(c => c.Value == 11 && c.Suit == ten.Suit).FirstOrDefault();

                if(jack!=null)
                {
                    for (var i = 0; i < cards.Length; i++)
                        if (cards[i] == ten || cards[i] == jack)
                            cardIndexes.Add(i);
                    return new HandAction
                    {
                        HandTier = 18,
                        HoldCards = cardIndexes.ToArray()
                    };
                }

                var queen = cards.Where(c => c.Value == 12 && c.Suit == ten.Suit).FirstOrDefault();

                if (queen != null)
                {
                    for (var i = 0; i < cards.Length; i++)
                        if (cards[i] == ten || cards[i] == queen)
                            cardIndexes.Add(i);
                    return new HandAction
                    {
                        HandTier = 20,
                        HoldCards = cardIndexes.ToArray()
                    };
                }

                var king = cards.Where(c => c.Value == 13 && c.Suit == ten.Suit).FirstOrDefault();

                if (king != null)
                {
                    for (var i = 0; i < cards.Length; i++)
                        if (cards[i] == ten || cards[i] == king)
                            cardIndexes.Add(i);
                    return new HandAction
                    {
                        HandTier = 22,
                        HoldCards = cardIndexes.ToArray()
                    };
                }
            }

            return HandAction.None;
        }
    }
}
