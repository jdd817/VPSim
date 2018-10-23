using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using VPPlayer;
using Hands.Entities;

namespace PokerTests
{
    [TestFixture]
    public class DdbPlayerTests
    {
        private VpPlayer _player;

        public DdbPlayerTests()
        {
            _player = new VpPlayer(System.Reflection.Assembly.GetAssembly(typeof(VpPlayer)).GetTypes()
                .Where(t => typeof(IHandTier).IsAssignableFrom(t) && t != typeof(IHandTier))
                .Where(t => t.Namespace.Contains("Ddb"))
                .Select(ht => Activator.CreateInstance(ht))
                .OfType<IHandTier>()
                .ToArray());
        }

        [Test]
        public void LowPair()
        {
            var holds = _player.GetHolds(Card.Hand("5H", "KD", "QS", "5D", "2C"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(0);
            holds.Should().Contain(3);
        }

        [Test]
        public void HighPair()
        {
            var holds = _player.GetHolds(Card.Hand("5H", "KD", "QS", "KC", "2C"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
        }

        [Test]
        public void FlushWithLowPair()
        {
            var holds = _player.GetHolds(Card.Hand("5D", "KD", "QD", "5S", "2D"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }

        [Test]
        public void FlushWithHighPair()
        {
            var holds = _player.GetHolds(Card.Hand("JD", "KD", "QD", "JS", "2D"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
        }

        [Test]
        public void Strait()
        {
            var holds = _player.GetHolds(Card.Hand("4D", "7D", "5S", "3S", "6D"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ThreeOfAKind()
        {
            var holds = _player.GetHolds(Card.Hand("4D", "KD", "4S", "JS", "4C"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }

        [Test]
        public void FourOfAKind4sJackKicker()
        {
            var holds = _player.GetHolds(Card.Hand("4D", "4H", "4S", "JS", "4C"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }

        [Test]
        public void FourOfAKind4s2Kicker()
        {
            var holds = _player.GetHolds(Card.Hand("4D", "4H", "4S", "2S", "4C"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void FullHouse()
        {
            var holds = _player.GetHolds(Card.Hand("4D", "JD", "4S", "JS", "4C"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void TwoPair()
        {
            var holds = _player.GetHolds(Card.Hand("4D", "KD", "4S", "JS", "JC"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void InsideStraitNoHighCards()
        {
            var holds = _player.GetHolds(Card.Hand("4D", "2D", "3S", "6S", "9C"));

            holds.Should().HaveCount(0);
        }

        [Test]
        public void SingleHighCard()
        {
            var holds = _player.GetHolds(Card.Hand("4D", "2D", "AS", "6S", "9C"));

            holds.Should().HaveCount(1);
            holds.Should().Contain(2);
        }

        [Test]
        public void ThreeHighCards()
        {
            var holds = _player.GetHolds(Card.Hand("4D", "2D", "AS", "6S", "9C"));

            holds.Should().HaveCount(1);
            holds.Should().Contain(2);
        }

        [Test]
        public void OutsideStrait()
        {
            var holds = _player.GetHolds(Card.Hand("4S", "6C", "5H", "9D", "3S"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }

        [Test]
        public void JTSuitedWithOffQ()
        {
            var holds = _player.GetHolds(Card.Hand("4S", "JS", "6D", "QH", "TS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
        }

        [Test]
        public void JTSuitedWithOffK()
        {
            var holds = _player.GetHolds(Card.Hand("4S", "JS", "6D", "KH", "TS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(4);
        }

        [Test]
        public void QTSuitedWithOffK()
        {
            var holds = _player.GetHolds(Card.Hand("4S", "KH", "6D", "QS", "TS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
        }

        [Test]
        public void FiveNeededForWheel()
        {
            var holds = _player.GetHolds(Card.Hand("4S", "AH", "3D", "2S", "TS"));

            holds.Should().HaveCount(1);
            holds.Should().Contain(1);
        }

        [Test]
        public void FourToARoyal()
        {
            var holds = _player.GetHolds(Card.Hand("QS", "TS", "5D", "KS", "JS"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ThreeToARoyal()
        {
            var holds = _player.GetHolds(Card.Hand("QS", "8S", "5D", "KS", "JS"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        #region conflict hands
        #region confict hand definitions (from WoO)
        /*
        9 T J Q K — Pat straight flush or 4 to a royal: Keep the straight flush.
2 T J Q A — Pat flush or 4 to a royal: Go for the royal.
T J Q K A — Pat straight or 4 to a royal: Go for the royal.
T J Q Q K — High pair or 4 to a royal: Go for the royal.
J Q Q Q K — 3 of a kind or 3 to a royal: Keep the 3 of a kind.
2 6 J Q K — Pat flush or 3 to a royal: Keep the flush
3 4 6 7 Q Pat flush or 4 to a straight flush: Keep the flush
T J Q K A — Pat straight or 3 to a royal: Keep the straight
4 5 6 7 8 — Pat straight or 4 to a straight flush: Keep the straight
T T Q A A — 2 pair or 3 to a royal: Keep the 2 pair.
7 8 9 J J — 4 to a straight flush vs. any pair: Go for straight flush
2 5 7 J J — High pair vs. 4 to a flush: Keep high pair
8 9 T J J — High pair vs. 4 to a straight: Keep high pair
3 T J Q Q — High pair vs. 3 to a royal flush: Keep high pair
3 9 J Q Q — High pair vs. 3 to a straight flush: Keep high pair
4 T T Q A — 3 to a royal vs. Low pair: Go for the royal.
2 5 T Q A — 3 to a royal vs. 4 to a flush: Go for the royal
2 T J Q K — 3 to a royal vs. 4 to a straight: Go for the royal
3 3 5 7 9 — 4 to a flush vs low pair: Go for the flush
4 5 6 7 Q — 4 to a flush vs. 4 to a straight: Go for the flush
2 6 7 8 K — 4 to a flush vs. 3 to a straight flush: Go for the flush
2 7 8 Q K — 4 to a flush vs. 2 to a royal flush: Go for the flush
5 6 7 7 8 — Low pair vs. 4 to a straight: Keep the low pair
7 8 8 9 K — Low pair vs. 3 to a straight flush: Keep the low pair
3 6 6 J Q — Low pair vs. 2 to a royal: — Keep the low pair
2789T — 4 to an outside straight vs. 3 to a straight flush: Go for the straight
29TJQ — 4 to an outside straight vs. 2 to a royal flush: Go for the straight
4 5 6 J Q — 3 to a straight flush (type 1) vs. 2 to a royal flush: Go for the straight flush
2 9 J Q K — 3 to a straight flush (type 1) vs. 4 to an inside straight with 3-4 high cards: Go for the straight flush
2 T J Q A — 2 to a royal flush (both high) vs. 4 to an inside straight with 3-4 high cards: Go for the royal
2 4 J Q K — 2 to a royal flush (both high) vs. any 3 unsuited high cards: Go for the royal
5 6 8 K A — 2 to a royal flush (both high) vs. 3 to a straight flush (type 2): Go for the royal
7 T J K A — 4 to an inside straight with 3-4 high cards vs. 3 to a straight flush (type 2): Go for the straight
7T J K A — 4 to an inside straight with 3-4 high cards vs. 2 to a royal (ten low): Go for the straight
2 4 7 T J — 3 to a straight flush (type 2) vs. 2 to a royal flush (10 low): Go for the straight flush
7 8 T J A — 3 to a straight flush (type 2) vs. 1-3 high cards: Go for the straight flush
2 5 J Q K — KQJ vs. QJ: Play KQJ
4 6 T J Q — QJ vs 2 to a royal flush (ten low): Keep QJ
2 4 6 J Q — QJ vs 3 to a straight flush (type 3): Keep QJ
3 5 J Q A — QJ vs 3 high cards A high: Keep QJ
2 3 T J A — JT suited vs 2-3 unsuited high cards K or A high: Keep JT
2 4 6 T J — JT suited vs 3 to a straight flush (type 3): Keep JT
2 4 T J K — KJ vs JT suited: Keep JT suited
2 4 6 J K — KQ or KJ vs. 3 to a straight flush (type 3): Keep KQ or KJ
3 4 5 T K — KT suited vs. K: Play KT suited
246TK — KT suited vs. 3 to a straight flush (type 3): Keep KT suited
3 5 7 8 J — Single high card vs. 3 to a straight flush (type 3): Keep high card only
        */
        #endregion

        [Test]
        public void ConflictHand_01()
        {
            var holds = _player.GetHolds(Card.Hand("9H", "TH", "JH", "QH", "KH"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_02()
        {
            var holds = _player.GetHolds(Card.Hand("2C", "TC", "JC", "QC", "AC"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_03()
        {
            var holds = _player.GetHolds(Card.Hand("TS", "JS", "QH", "KS", "AS"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_04()
        {
            var holds = _player.GetHolds(Card.Hand("TC", "JC", "QC", "QH", "KC"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_05()
        {
            var holds = _player.GetHolds(Card.Hand("JC", "QH", "QC", "QS", "KC"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }

        [Test]
        public void ConflictHand_06()
        {
            var holds = _player.GetHolds(Card.Hand("9H", "TH", "JH", "QH", "KH"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_07()
        {
            var holds = _player.GetHolds(Card.Hand("3H", "4H", "6H", "7H", "QH"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_08()
        {
            var holds = _player.GetHolds(Card.Hand("TH", "JC", "QH", "KS", "AH"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_09()
        {
            var holds = _player.GetHolds(Card.Hand("4H","5H","6H","7H","8S"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_10()
        {
            var holds = _player.GetHolds(Card.Hand("TC","TH","QC","AH","AC"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_11()
        {
            var holds = _player.GetHolds(Card.Hand("7S","8S","9S","JS","JH"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }

        [Test]
        public void ConflictHand_12()
        {
            var holds = _player.GetHolds(Card.Hand("2H","5H","7H","JC","JH"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_13()
        {
            var holds = _player.GetHolds(Card.Hand("8C","9H","TS","JH","JC"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_14()
        {
            var holds = _player.GetHolds(Card.Hand("3H","TS","JS","QH","QS"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_15()
        {
            var holds = _player.GetHolds(Card.Hand("3H","9S","JS","QH","QS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_16()
        {
            var holds = _player.GetHolds(Card.Hand("4C","TH","TS","QH","AH"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_17()
        {
            var holds = _player.GetHolds(Card.Hand("2H","5C","TH","QH","AH"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_18()
        {
            var holds = _player.GetHolds(Card.Hand("2H","TC","JH","QC","KC"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_19()
        {
            var holds = _player.GetHolds(Card.Hand("3S","3H","5S","7S","9S"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_20()
        {
            var holds = _player.GetHolds(Card.Hand("4S","5S","6H","7S","QS"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_21()
        {
            var holds = _player.GetHolds(Card.Hand("2C","6H","7H","8H","KH"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_22()
        {
            var holds = _player.GetHolds(Card.Hand("2C","7C","8H","QC","KC"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_23()
        {
            var holds = _player.GetHolds(Card.Hand("5S","6H","7C","7H","8S"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }

        [Test]
        public void ConflictHand_24()
        {
            var holds = _player.GetHolds(Card.Hand("7S","8S","8H","9S","KH"));
            
            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
        }

        [Test]
        public void ConflictHand_25()
        {
            var holds = _player.GetHolds(Card.Hand("3S","6C","6H","JS","QS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
        }

        [Test]
        public void ConflictHand_26()
        {
            var holds = _player.GetHolds(Card.Hand("2C","7H","8H","9H","TS"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_27()
        {
            var holds = _player.GetHolds(Card.Hand("2C","9C","TS","JH","QH"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_28()
        {
            var holds = _player.GetHolds(Card.Hand("4C", "5C", "6C", "JH", "QH"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
        }

        [Test]
        public void ConflictHand_29()
        {
            var holds = _player.GetHolds(Card.Hand("2S","9H","JH","QC","KH"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_30()
        {
            var holds = _player.GetHolds(Card.Hand("2H","TC","JH","QS","AH"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_31()
        {
            var holds = _player.GetHolds(Card.Hand("2S","4H","JC","QH","KC"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_32()
        {
            var holds = _player.GetHolds(Card.Hand("5H","6H","8H","KS","AS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_33()
        {
            var holds = _player.GetHolds(Card.Hand("7H","TH","JH","KC","AS"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_34()
        {
            var holds = _player.GetHolds(Card.Hand("7D","TH","JC","KS","AH"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_35()
        {
            var holds = _player.GetHolds(Card.Hand("2S","4H","7C","TC","JC"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_36()
        {
            var holds = _player.GetHolds(Card.Hand("7H","8H","TH","JC","AS"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
        }

        [Test]
        public void ConflictHand_37()
        {
            var holds = _player.GetHolds(Card.Hand("2H","5S","JC","QH","KS"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_38()
        {
            var holds = _player.GetHolds(Card.Hand("4S","6H","TH","JH","QC"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_39()
        {
            var holds = _player.GetHolds(Card.Hand("2H","4H","6H","JS","QC"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_40()
        {
            var holds = _player.GetHolds(Card.Hand("3H","5S","JC","QH","AS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }

        [Test]
        public void ConflictHand_41()
        {
            var holds = _player.GetHolds(Card.Hand("2C","3H","TH","JH","AC"));

            holds.Should().HaveCount(1);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_42()
        {
            var holds = _player.GetHolds(Card.Hand("2H","4H","6H","TS","JS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_43()
        {
            var holds = _player.GetHolds(Card.Hand("2H","4S","TH","JH","KC"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }

        [Test]
        public void ConflictHand_44()
        {
            var holds = _player.GetHolds(Card.Hand("2H","4H","6H","JS","KC"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_45()
        {
            var holds = _player.GetHolds(Card.Hand("3H","4C","5H","TS","KS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_46()
        {
            var holds = _player.GetHolds(Card.Hand("2H","4H","6H","TS","KS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void ConflictHand_47()
        {
            var holds = _player.GetHolds(Card.Hand("3C","5C","7C","8H","JH"));

            holds.Should().HaveCount(1);
            holds.Should().Contain(4);
        }

        #endregion
    }
}
