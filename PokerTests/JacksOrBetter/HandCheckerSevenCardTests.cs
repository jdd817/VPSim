using Hands.Entities;
using Hands.Poker.Hands;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Hands.Poker.Hands.JacksOrBetter;

namespace PokerTests.JacksOrBetter
{
    class HandCheckerSevenCardTests
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
        public HandCheckerSevenCardTests()
        {
            _hands = new List<Card[]>()
            {
                Card.Hand("6S","4D","5C","2H","QH","TS","8C"),//0
                Card.Hand("6S","2D","5C","2H","QH","TD","8C"),//1
                Card.Hand("QD","TD","5C","5S","QH","TH","3D"),//2
                Card.Hand("6S","AD","5C","AH","AH","7D","2S"),//3
                Card.Hand("6S","7D","5C","4H","4H","9H","8H"),//4
                Card.Hand("6S","KS","5S","2s","Qs","4S","AS"),//5
                Card.Hand("5S","4D","5C","4H","5H","7H","7S"),//6
                Card.Hand("2S","2D","QC","2H","QH","Qs","QD"),//7
                Card.Hand("4S","8S","5H","7S","6S","9C","5S"),//8
                Card.Hand("JH","AH","TH","KH","QH","9H","8H"),//9
                Card.Hand("2C","AS","4H","5D","3S","KS","QH"),//10
                Card.Hand("2C","AS","6H","5D","4S","8S","7H"),//11
                Card.Hand("2C","AS","6H","5D","3S","8S","7H"),//12
            };
        }

        [Test]
        public void RoyalFlushFound()
        {
            var checker = new StraitFlushChecker();
            var value = checker.HandValue(_hands[9]);

            value.Should().Be(8.1413121110m);
        }

        [Test]
        public void StraitFlushFound()
        {
            var checker = new StraitFlushChecker();
            var value = checker.HandValue(_hands[8]);

            value.Should().Be(8.0807060504m);
        }

        [Test]
        public void StraitFlushNotFound()
        {
            var checker = new StraitFlushChecker();

            for (var i = 0; i < 9; i++)
                if (i != 8 && i != 9)
                {
                    var value = checker.HandValue(_hands[i]);
                    value.Should().Be(0, _hands[i].Select(c => c.ToString()).Aggregate((a, b) => a + " " + b));
                }
        }

        [Test]
        public void FourOfAKindFound()
        {
            var checker = new FourOfAKindChecker();
            var value = checker.HandValue(_hands[7]);

            value.Should().Be(7.1202m);
        }

        [Test]
        public void FourOfAKindNotFound()
        {
            var checker = new FourOfAKindChecker();

            for (var i = 0; i < 9; i++)
                if (i != 7)
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

            value.Should().Be(6.0507m);
        }

        [Test]
        public void FullHouseNotFound()
        {
            var checker = new FullHouseChecker();

            for (var i = 0; i < 9; i++)
                if (i != 6 && i != 7)
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

            value.Should().Be(5.1413120605m);
        }

        [Test]
        public void FlushNotFound()
        {
            var checker = new FlushChecker();

            for (var i = 0; i < 9; i++)
                if (i != 5 && i != 8 && i != 9)
                {
                    var value = checker.HandValue(_hands[i]);
                    value.Should().Be(0, _hands[i].Select(c => c.ToString()).Aggregate((a, b) => a + " " + b));
                }
        }

        [Test]
        public void DetachedStraitFound()
        {
            var checker = new StraitChecker();
            var value = checker.HandValue(_hands[11]);

            value.Should().Be(4.0807060504m);
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

            value.Should().Be(4.0908070605m);
        }

        [Test]
        public void StraitNotFound()
        {
            var checker = new StraitChecker();

            for (var i = 0; i < 9; i++)
                if (i != 4 && i != 8 && i != 9 && i != 10 && i!=11)
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

            value.Should().Be(3.140706m);
        }

        [Test]
        public void ThreeOfAKindNotFound()
        {
            var checker = new ThreeOfAKindChecker();

            for (var i = 0; i < 9; i++)
                if (i != 3 && i != 6 && i != 7)
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

            value.Should().Be(2.121005m);
        }

        [Test]
        public void TwoPairNotFound()
        {
            var checker = new TwoPairChecker();

            for (var i = 0; i < 9; i++)
                if (i != 2 && i != 6 && i != 7)
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

            value.Should().Be(1.02121008m);
        }

        [Test]
        public void PairNotFound()
        {
            var checker = new PairChecker();

            for (var i = 0; i < 9; i++)
                if (i != 1 && i != 2 && i != 3 && i != 6 && i != 7 && i!=4 && i!=8)
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

            value.Should().Be(0.1210080605m);
        }
    }
}
