using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Entities
{
    public class Card
    {
        public Card() { }
        public Card(string card)
        {
            card = card.ToUpper();
            var suit = card[1];
            var value = card[0];

            switch (suit)
            {
                case 'D': Suit = 1; break;
                case 'C': Suit = 2; break;
                case 'H': Suit = 3; break;
                case 'S': Suit = 4; break;
                default: Suit = 0; break;
            }

            switch (value)
            {
                case 'A': Value = 14; break;
                case 'K': Value = 13; break;
                case 'Q': Value = 12; break;
                case 'J': Value = 11; break;
                case 'T': Value = 10; break;
                case '9': Value = 9; break;
                case '8': Value = 8; break;
                case '7': Value = 7; break;
                case '6': Value = 6; break;
                case '5': Value = 5; break;
                case '4': Value = 4; break;
                case '3': Value = 3; break;
                case '2': Value = 2; break;
                default: Value = 0; break;
            }
        }
        /// <summary>
        /// D = 1; C = 2; H = 3; S = 4
        /// </summary>
        public int Suit { get; set; }
        public int Value { get; set; }

        public static Card[] Hand(params string[] cards)
        {
            return _hand(cards).ToArray();
        }

        private static IEnumerable<Card> _hand(params string[] cards)
        {
            foreach (var c in cards)
                yield return new Card(c);
        }

        public override string ToString()
        {
            return String.Format("{0}{1}",                
                Value == 10 ? "T" : Value == 11 ? "J" : Value == 12 ? "Q" : Value == 13 ? "K" : Value == 14 ? "A" : Value == 0 ? "O" : Value.ToString(),
                Suit == 1 ? "D" : Suit == 2 ? "C" : Suit == 3 ? "H" : Suit == 4 ? "S" : "J"
                );
        }

        public static bool operator==(Card A, Card B)
        {
            if (((object)A) == null)
                return ((object)B) == null;
            return A.Equals(B);
        }

        public static bool operator !=(Card A, Card B)
        {
            if (((object)A) == null)
                return ((object)B) != null; 
            return !A.Equals(B);
        }

        public override bool Equals(object obj)
        {
            if (obj==null || obj.GetType() != typeof(Card))
                return false;
            Card A = this;
            Card B = (Card)obj;
            return A.Value == B.Value && A.Suit == B.Suit;
        }
    }
}
