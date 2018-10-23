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
                Card.Hand("6S","KS","5S","2s","Qs"),//5
                Card.Hand("5S","KD","5C","KH","5H"),//6
                Card.Hand("2S","2D","2C","2H","QH"),//7
                Card.Hand("4S","8S","5S","7S","6S"),//8
                Card.Hand("JH","AH","TH","KH","QH"),//9
                Card.Hand("2C","AS","4H","5D","3S")//10
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

            value.Should().Be(7.0212m);
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

            value.Should().Be(6.0513m);
        }

        [Test]
        public void FullHouseNotFound()
        {
            var checker = new FullHouseChecker();

            for (var i = 0; i < 9; i++)
                if (i != 6)
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

            value.Should().Be(5.1312060502m);
        }

        [Test]
        public void FlushNotFound()
        {
            var checker = new FlushChecker();

            for(var i=0;i<9;i++)
                if(i!=5 && i!=8 && i!=9)
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

            value.Should().Be(4.0605040302m);
        }

        [Test]
        public void StraitNotFound()
        {
            var checker = new StraitChecker();

            for (var i = 0; i < 9; i++)
                if (i != 4 && i != 8 && i != 9 && i!=10)
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

            value.Should().Be(2.120502m);
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

            value.Should().Be(1.02120605m);
        }

        [Test]
        public void PairNotFound()
        {
            var checker = new PairChecker();

            for (var i = 0; i < 9; i++)
                if (i != 1 && i != 2 && i != 3 && i != 6 && i != 7)
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
