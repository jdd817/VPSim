using Hands.Poker;
using Hands.Poker.Hands;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Hands.Entities;
using Hands.Poker.Hands.JacksOrBetter;

namespace PokerTests.JacksOrBetter
{
    [TestFixture]
    public class HandAnalyzerTests
    {
        private HandAnalyzer _analyzer;

        public HandAnalyzerTests()
        {
            _analyzer = new HandAnalyzer(new IHandChecker[]
            {
                new HighCardChecker(),
                new PairChecker(),
                new TwoPairChecker(),
                new ThreeOfAKindChecker(),
                new StraitChecker(),
                new FlushChecker(),
                new FullHouseChecker(),
                new FourOfAKindChecker(),
                new StraitFlushChecker()
            });
        }

        [Test]
        public void NotEnoughCards()
        {
            Action action = () => _analyzer.GetHandValue(Card.Hand("AS", "KS", "QS", "JS"));

            action.ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void HighCardValue()
        {
            var value = _analyzer.GetHandValue(Card.Hand("JS", "TD", "7S", "8D", "2H"));

            value.Should().Be(0.1110080702m);
        }

        [Test]
        public void PairValue()
        {
            var value = _analyzer.GetHandValue(Card.Hand("AS","KH","QD","2C","JS","3D","JC"));

            value.Should().Be(1.11141312m);
        }

        [Test]
        public void TwoPairValue()
        {
            var value = _analyzer.GetHandValue(Card.Hand("TS", "JH", "AD", "4H", "JS", "TC", "4D"));

            value.Should().Be(2.111014m);
        }

        [Test]
        public void TripsValue()
        {
            var value = _analyzer.GetHandValue(Card.Hand("KH", "AS", "8D", "2C", "8C", "4D", "8S"));

            value.Should().Be(3.081413m);
        }

        [Test]
        public void StraitValue()
        {
            var value = _analyzer.GetHandValue(Card.Hand("8S", "5C", "KD", "7S", "4C", "AD", "6H"));

            value.Should().Be(4.0807060504m);
        }

        [Test]
        public void FlushValue()
        {
            var value=_analyzer.GetHandValue(Card.Hand("6C", "9C", "AS", "KC", "4C", "JC", "TC"));

            value.Should().Be(5.1311100906m);
        }

        [Test]
        public void BoatValue()
        {
            var value = _analyzer.GetHandValue(Card.Hand("8H", "7S", "5D", "5C", "7C", "5S", "7D"));

            value.Should().Be(6.0705m);
        }

        [Test]
        public void QuadsValue()
        {
            var value = _analyzer.GetHandValue(Card.Hand("8D", "AS", "8H", "8S", "AH", "AC", "8C"));

            value.Should().Be(7.0814m);
        }

        [Test]
        public void StraitFlushValue()
        {
            var value = _analyzer.GetHandValue(Card.Hand("9D", "TD", "JS", "JD", "QH", "8D", "9S", "7D"));

            value.Should().Be(8.1110090807m);
        }
    }
}
