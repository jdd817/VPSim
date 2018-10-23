using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Entities
{
    public class Deck
    {
        private List<Card> _cards { get; set; }
        private Random _rand;

        public Deck()
        {
            _rand = new Random();
            _cards = new List<Card>();

            Reset();
        }

        public void Reset()
        {
            _cards.Clear();

            for (var i = 1; i <= 4; i++)
                for (var j = 2; j <= 14; j++)
                    _cards.Add(new Card { Suit = i, Value = j });

            Shuffle();
        }

        public void Shuffle()
        {
            var passes = 5 + _rand.Next(5);

            for (var i = 0; i < passes; i++)
                for (var j = 0; j < _cards.Count; j++)
                    SwapCards(j, _rand.Next(_cards.Count));
        }

        private void SwapCards(int cardIndexA, int cardIndexB)
        {
            var cardA = _cards[cardIndexA];
            var cardB = _cards[cardIndexB];

            _cards[cardIndexA] = cardB;
            _cards[cardIndexB] = cardA;
        }

        public Card Draw()
        {
            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }

        public Card[] Draw(int n)
        {
            var cards = _cards.Take(n).ToArray();
            foreach (var c in cards)
                _cards.Remove(c);
            return cards;
        }

        public Card Peek()
        {
            return _cards[0];
        }

        public Card[] Peek(int n)
        {
            return _cards.Take(n).ToArray();
        }

        public void Remove(Card card)
        {
            _cards.Remove(_cards.FirstOrDefault(c => c == card));
        }

        public void Remove(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
                Remove(card);
        }
    }
}
