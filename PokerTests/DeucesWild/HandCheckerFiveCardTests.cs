using FluentAssertions;
using Hands.Entities;
using Hands.Poker.Hands.DeucesWild;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTests.DeucesWild
{
    [TestFixture]
    public class HandCheckerFiveCardTests
    {
        private List<Card[]> _hands;

        /*
            High Card:    0
            Pair:         1
            Two Pair:     2
            3 of a Kind:  3
            Strait:       4
            Flush:        5
            Full House:   6
            4 of a Kind:  7
            Strait Flush: 8
        */
        public HandCheckerFiveCardTests()
        {
            _hands = new List<Card[]>()
            {
                Card.Hand("6S","4D","5C","2H","QH"),//0
                Card.Hand("6S","2D","5C","2H","QH"),//1
                Card.Hand("QD","2D","5C","5S","QH"),//2
                Card.Hand("6S","AD","5C","AH","AH"),//3
                Card.Hand("6S","2D","5C","3H","4H"),//4
                Card.Hand("6S","KS","5S","4S","QS"),//5
                Card.Hand("5S","KD","5C","KH","5H"),//6
                Card.Hand("2S","2D","2C","2H","7H"),//7
                Card.Hand("4S","8S","5S","7S","6S"),//8
                Card.Hand("JH","AH","TH","KH","QH"),//9
                Card.Hand("2C","AS","4H","5D","3S"),//10
                Card.Hand("JH","AH","2S","KH","QH"),//11
                Card.Hand("4S","3S","5S","2D","6S"),//12
                Card.Hand("2S","2D","2C","3H","7H"),//13
                Card.Hand("2S","2D","4C","4H","7H"),//14
                Card.Hand("4S","4D","4C","4H","7H"),//15
                Card.Hand("2S","8D","2C","2H","8H"),//16
            };
        }

        [Test]
        public void NaturalRoyalFlushFound()
        {
            var checker = new StraitFlushChecker();
            var value = checker.HandValue(_hands[9]);

            value.Should().Be(10.1413121110m);
        }

        [Test]
        public void WildRoyalFlushFound()
        {
            var checker = new StraitFlushChecker();
            var value = checker.HandValue(_hands[11]);

            value.Should().Be(8.1413121110m);
        }

        [Test]
        public void NaturalStraitFlushFound()
        {
            var checker = new StraitFlushChecker();
            var value = checker.HandValue(_hands[8]);

            value.Should().Be(8.0807060504m);
        }

        [Test]
        public void WildStraitFlushFound()
        {
            var checker = new StraitFlushChecker();
            var value = checker.HandValue(_hands[12]);

            value.Should().Be(8.0706050403m);
        }

        [Test]
        public void MultipleWildStraitFlushFound()
        {
            var checker = new StraitFlushChecker();
            var value = checker.HandValue(_hands[7]);

            value.Should().Be(8.1110090807m);
        }

        [Test]
        public void StraitFlushNotFound()
        {
            var checker = new StraitFlushChecker();

            for (var i = 0; i < _hands.Count; i++)
                if (!new[] { 7, 8, 9, 11, 12, 13 }.Contains(i))
                {
                    var value = checker.HandValue(_hands[i]);
                    value.Should().Be(0, _hands[i].Select(c => c.ToString()).Aggregate((a, b) => a + " " + b));
                }
        }

        [Test]
        public void FiveOfAKind4DeucesFound()
        {
            var checker = new FiveOfAKindChecker();
            var value = checker.HandValue(_hands[7]);

            value.Should().Be(9.07m);
        }

        [Test]
        public void FiveOfAKind3DeucesFound()
        {
            var checker = new FiveOfAKindChecker();
            var value = checker.HandValue(_hands[16]);

            value.Should().Be(9.08m);
        }

        [Test]
        public void FiveOfAKindNotFound()
        {
            var checker = new FiveOfAKindChecker();

            for (var i = 0; i < _hands.Count; i++)
                if (!new[] { 7, 16 }.Contains(i))
                {
                    var value = checker.HandValue(_hands[i]);
                    value.Should().Be(0, _hands[i].Select(c => c.ToString()).Aggregate((a, b) => a + " " + b));
                }
        }

        [Test]
        public void FourDeucesFound()
        {
            var checker = new FourOfAKindChecker();
            var value = checker.HandValue(_hands[7]);

            value.Should().Be(7.02m);
        }

        [Test]
        public void FourOfAKind2WildFound()
        {
            var checker = new FourOfAKindChecker();
            var value = checker.HandValue(_hands[14]);

            value.Should().Be(7.0407m);
        }

        [Test]
        public void FourOfAKind3WildFound()
        {
            var checker = new FourOfAKindChecker();
            var value = checker.HandValue(_hands[13]);

            value.Should().Be(7.0703m);
        }

        [Test]
        public void FourOfAKindNoWildFound()
        {
            var checker = new FourOfAKindChecker();
            var value = checker.HandValue(_hands[15]);

            value.Should().Be(7.0407m);
        }

        [Test]
        public void FourOfAKindNotFound()
        {
            var checker = new FourOfAKindChecker();

            for (var i = 0; i < _hands.Count; i++)
                if (!new[] { 7, 13, 14, 15, 16 }.Contains(i))
                {
                    var value = checker.HandValue(_hands[i]);
                    value.Should().Be(0, _hands[i].Select(c => c.ToString()).Aggregate((a, b) => a + " " + b));
                }
        }

        [Test]
        public void FullHouseFound()
        {
            var checker = new FullHouseChecker();
            var value = checker.HandValue(_hands[6]);

            value.Should().Be(6.0513m);
        }

        [Test]
        public void WildFullHouseFound()
        {
            var checker = new FullHouseChecker();
            var value = checker.HandValue(_hands[2]);

            value.Should().Be(6.1205m);
        }

        [Test]
        public void WildFullHouseFound2()
        {
            var checker = new FullHouseChecker();
            var value = checker.HandValue(_hands[13]);

            value.Should().Be(6.0703m);
        }

        [Test]
        public void FullHouseNotFound()
        {
            var checker = new FullHouseChecker();

            for (var i = 0; i < _hands.Count; i++)
                if (!new[] { 2, 6, 13, 14, 16 }.Contains(i))
                {
                    var value = checker.HandValue(_hands[i]);
                    value.Should().Be(0, _hands[i].Select(c => c.ToString()).Aggregate((a, b) => a + " " + b));
                }
        }

        [Test]
        public void FlushFound()
        {
            var checker = new FlushChecker();
            var value = checker.HandValue(_hands[5]);

            value.Should().Be(5.1312060504m);
        }

        [Test]
        public void WildFlushFound()
        {
            var checker = new FlushChecker();
            var value = checker.HandValue(_hands[7]);

            value.Should().Be(5.07m);
        }

        [Test]
        public void FlushNotFound()
        {
            var checker = new FlushChecker();

            for (var i = 0; i < _hands.Count; i++)
                if (!new[] { 5, 7, 8, 9, 11, 12, 13 }.Contains(i))
                {
                    var value = checker.HandValue(_hands[i]);
                    value.Should().Be(0, _hands[i].Select(c => c.ToString()).Aggregate((a, b) => a + " " + b));
                }
        }

        [Test]
        public void WheelFound()
        {
            var checker = new StraitChecker();
            var value = checker.HandValue(_hands[10]);

            value.Should().Be(4.0504030201m);
        }

        [Test]
        public void StraitFound()
        {
            var checker = new StraitChecker();
            var value = checker.HandValue(_hands[4]);

            value.Should().Be(4.0706050403m);
        }

        [Test]
        public void StraitFound3Wilds()
        {
            var checker = new StraitChecker();
            var value = checker.HandValue(_hands[13]);

            value.Should().Be(4.0706050403m);
        }

        [Test]
        public void StraitNotFound()
        {
            var checker = new StraitChecker();

            for (var i = 0; i < 9; i++)
                if (!new[] { 4, 7, 8, 9, 10, 11, 12, 13 }.Contains(i))
                {
                    var value = checker.HandValue(_hands[i]);
                    value.Should().Be(0, _hands[i].Select(c => c.ToString()).Aggregate((a, b) => a + " " + b));
                }
        }

        [Test]
        public void ThreeOfAKindFound()
        {
            var checker = new ThreeOfAKindChecker();
            var value = checker.HandValue(_hands[3]);

            value.Should().Be(3.140605m);
        }

        [Test]
        public void ThreeOfAKindOneDeuceFound()
        {
            var checker = new ThreeOfAKindChecker();
            var value = checker.HandValue(_hands[2]);

            value.Should().Be(3.120505m);
        }

        [Test]
        public void ThreeOfAKindTwoDeucesFound()
        {
            var checker = new ThreeOfAKindChecker();
            var value = checker.HandValue(_hands[1]);

            value.Should().Be(3.120605m);
        }

        [Test]
        public void ThreeOfAKindNotFound()
        {
            var checker = new ThreeOfAKindChecker();

            for (var i = 0; i < _hands.Count; i++)
                if (!new[] { 1, 2, 3, 6, 7, 13, 14, 15, 16 }.Contains(i))
                {
                    var value = checker.HandValue(_hands[i]);
                    value.Should().Be(0, _hands[i].Select(c => c.ToString()).Aggregate((a, b) => a + " " + b));
                }
        }

        [Test]
        public void TwoPairFound()
        {
            var checker = new TwoPairChecker();
            var value = checker.HandValue(_hands[2]);

            value.Should().Be(2.120502m);
        }

        [Test]
        public void TwoPairNotFound()
        {
            var checker = new TwoPairChecker();

            for (var i = 0; i < 9; i++)
                if (!new[] {1, 2, 6, 7, 13, 14, 15, 16 }.Contains(i))
                {
                    var value = checker.HandValue(_hands[i]);
                    value.Should().Be(0, _hands[i].Select(c => c.ToString()).Aggregate((a, b) => a + " " + b));
                }
        }

        [Test]
        public void PairFound()
        {
            var checker = new PairChecker();
            var value = checker.HandValue(_hands[1]);

            value.Should().Be(1.12060502m);
        }

        [Test]
        public void PairNotFound()
        {
            var checker = new PairChecker();

            for (var i = 0; i < 9; i++)
                if (!new[] { 0, 1, 2, 3, 4, 6, 7, 10, 11, 12, 13, 14, 15 }.Contains(i))
                {
                    var value = checker.HandValue(_hands[i]);
                    value.Should().Be(0, _hands[i].Select(c => c.ToString()).Aggregate((a, b) => a + " " + b));
                }
        }

        [Test]
        public void HighCardFound()
        {
            var checker = new HighCardChecker();
            var value = checker.HandValue(_hands[0]);

            value.Should().Be(0.1206050402m);
        }

    }
}
