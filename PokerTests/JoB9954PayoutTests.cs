using Hands;
using Hands.Entities;
using Hands.Poker;
using Hands.Poker.Hands;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Hands.Poker.Hands.JacksOrBetter;

namespace PokerTests
{
    [TestFixture]
    public class JoB9954PayoutTests
    {
        private PayTable _payTable;
        private VideoPokerPayoutCalculator _payoutCalc;

        public JoB9954PayoutTests()
        {
            var handAnalyzer = new HandAnalyzer(new IHandChecker[]
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

            _payoutCalc = new VideoPokerPayoutCalculator(handAnalyzer);

            _payTable = StandardPayTables.JoB_9954;
        }

        [Test]
        public void HighCardPayout()
        {
            var hand = Card.Hand("7S", "6D", "KH", "2S", "5C");
            var value = _payoutCalc.GetPayout(_payTable, hand, 5);

            value.Payout.Should().Be(0);
        }

        [Test]
        public void LowPairPayout()
        {
            var hand = Card.Hand("7S", "6D", "KH", "6S", "5C");
            var value = _payoutCalc.GetPayout(_payTable, hand, 5);

            value.Payout.Should().Be(0);
        }

        [Test]
        public void HighPairPayout()
        {
            var hand = Card.Hand("7S", "6D", "KH", "2S", "KC");
            var value = _payoutCalc.GetPayout(_payTable, hand, 5);

            value.Payout.Should().Be(5);
        }

        [Test]
        public void TwoPairPayout()
        {
            var hand = Card.Hand("7S", "6D", "6H", "2S", "7C");
            var value = _payoutCalc.GetPayout(_payTable, hand, 5);

            value.Payout.Should().Be(10);
        }

        [Test]
        public void ThreeOfAKindPayout()
        {
            var hand = Card.Hand("2S", "6D", "KH", "2S", "2C");
            var value = _payoutCalc.GetPayout(_payTable, hand, 5);

            value.Payout.Should().Be(15);
        }

        [Test]
        public void StraitPayout()
        {
            var hand = Card.Hand("7S", "6D", "4H", "8S", "5C");
            var value = _payoutCalc.GetPayout(_payTable, hand, 3);

            value.Payout.Should().Be(12);
        }

        [Test]
        public void FlushPayout()
        {
            var hand = Card.Hand("7S", "6S", "Ks", "2S", "5S");
            var value = _payoutCalc.GetPayout(_payTable, hand, 1);

            value.Payout.Should().Be(6);
        }

        [Test]
        public void FullHousePayout()
        {
            var hand = Card.Hand("7S", "5D", "7H", "5S", "5C");
            var value = _payoutCalc.GetPayout(_payTable, hand, 4);

            value.Payout.Should().Be(36);
        }

        [Test]
        public void FourOfAKindPayout()
        {
            var hand = Card.Hand("7S", "7D", "7H", "2S", "7C");
            var value = _payoutCalc.GetPayout(_payTable, hand, 5);

            value.Payout.Should().Be(125);
        }

        [Test]
        public void StraitFlushPayout()
        {
            var hand = Card.Hand("4S", "6S", "3S", "2S", "5S");
            var value = _payoutCalc.GetPayout(_payTable, hand, 5);

            value.Payout.Should().Be(250);
        }

        [Test]
        public void RoyalFlushPayoutNotMaxBet()
        {
            var hand = Card.Hand("QS", "JS", "KS", "TS", "AS");
            var value = _payoutCalc.GetPayout(_payTable, hand, 4);

            value.Payout.Should().Be(1600);
        }

        [Test]
        public void RoyalFlushPayoutMaxBet()
        {
            var hand = Card.Hand("QS", "JS", "KS", "TS", "AS");
            var value = _payoutCalc.GetPayout(_payTable, hand, 5);

            value.Payout.Should().Be(4000);
        }
    }
}
