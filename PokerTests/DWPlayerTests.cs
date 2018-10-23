using FluentAssertions;
using Hands.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPPlayer;

namespace PokerTests
{
    [TestFixture]
    public class DWPlayerTests
    {
        private VpPlayer _player;

        public DWPlayerTests()
        {
            _player = new VpPlayer(System.Reflection.Assembly.GetAssembly(typeof(VpPlayer)).GetTypes()
                .Where(t => typeof(IHandTier).IsAssignableFrom(t) && t != typeof(IHandTier))
                .Where(t => t.Namespace.Contains("DW"))
                .Select(ht => Activator.CreateInstance(ht))
                .OfType<IHandTier>()
                .ToArray());
        }

        [Test]
        public void PairNoDeuce()
        {
            var holds = _player.GetHolds(Card.Hand("5H", "KD", "QS", "5D", "7C"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(0);
            holds.Should().Contain(3);
        }

        [Test]
        public void OneDeuce()
        {
            var holds = _player.GetHolds(Card.Hand("JH", "KD", "3S", "2D", "7C"));

            holds.Should().HaveCount(1);
            holds.Should().Contain(3);
        }


        [Test]
        public void TwoDeuces()
        {
            var holds = _player.GetHolds(Card.Hand("2H", "KD", "3S", "2D", "7C"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(0);
            holds.Should().Contain(3);
        }

        [Test]
        public void ThreeDeuces()
        {
            var holds = _player.GetHolds(Card.Hand("2H", "2C", "3S", "2D", "7C"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
        }

        [Test]
        public void FourDeuces()
        {
            var holds = _player.GetHolds(Card.Hand("2H", "2C", "2S", "2D", "QC"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }

        [Test]
        public void ThreeDeucesWildRoyal()
        {
            var holds = _player.GetHolds(Card.Hand("2H", "2D", "KS", "2D", "TS"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void TwoDeucesQuads()
        {
            var holds = _player.GetHolds(Card.Hand("2H", "2D", "KS", "KD", "TS"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }

        [Test]
        public void TwoDeucesQuints()
        {
            var holds = _player.GetHolds(Card.Hand("2H", "2D", "KS", "KD", "KC"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void TwoDeucesWildRoyal()
        {
            var holds = _player.GetHolds(Card.Hand("2H", "2D", "KS", "AS", "TS"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void TwoDeucesPatStraitFlush()
        {
            var holds = _player.GetHolds(Card.Hand("5S", "2D", "7S", "2H", "3S"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        [Test]
        public void TwoDeuces4ToStraitFlush()
        {
            var holds = _player.GetHolds(Card.Hand("5S", "2D", "9D", "2S", "3S"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }

        #region Generated Tests


        [Test]
        public void GeneratedTest_65726ea843334cc3a69cc8bf5ef4ea41()
        {
            var holds = _player.GetHolds(Card.Hand("8C", "AD", "4C", "9H", "9S"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_c9758b8df5e3451a9325c5dc6cb617a7()
        {
            var holds = _player.GetHolds(Card.Hand("3D", "JS", "AC", "QD", "KD"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_7f54d6fa66c9422ea4d942dc27bb331e()
        {
            var holds = _player.GetHolds(Card.Hand("4S", "JS", "5H", "QH", "TS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_38810da2031143d18a715a17efa51f20()
        {
            var holds = _player.GetHolds(Card.Hand("AS", "TS", "6S", "8H", "9H"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_c243681d38c143f3826d6405bbead298()
        {
            var holds = _player.GetHolds(Card.Hand("AH", "9D", "2S", "7C", "9C"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_3e5425c37d5a47e987610a0740ba9084()
        {
            var holds = _player.GetHolds(Card.Hand("6D", "8C", "7S", "6S", "AS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(0);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_c85c8bec3e414dfaa773be877822d337()
        {
            var holds = _player.GetHolds(Card.Hand("JS", "KD", "9H", "JC", "3H"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(0);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_20edc488b24542f891635b6a10501eac()
        {
            var holds = _player.GetHolds(Card.Hand("8D", "8H", "9C", "JH", "2S"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_0dbd1576d0f447948e282a4109e0199a()
        {
            var holds = _player.GetHolds(Card.Hand("AS", "KC", "6S", "8C", "QD"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_569f44026c6a47f69c2101d18cc2db21()
        {
            var holds = _player.GetHolds(Card.Hand("2H", "JD", "3S", "8C", "3C"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_2b8084d3cbac423f98901deda931d7f1()
        {
            var holds = _player.GetHolds(Card.Hand("QS", "6H", "7C", "9H", "KC"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_9517ac8ac2964a13b83c6941e7113853()
        {
            var holds = _player.GetHolds(Card.Hand("TD", "6C", "8C", "AD", "JD"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_081a5b228497465ca94dbb7a8f4f8826()
        {
            var holds = _player.GetHolds(Card.Hand("4H", "7D", "2D", "8D", "AH"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_a74f01fc59c845f1bcf308b08278e8a4()
        {
            var holds = _player.GetHolds(Card.Hand("TS", "2D", "3S", "KH", "8C"));

            holds.Should().HaveCount(1);
            holds.Should().Contain(1);
        }


        [Test]
        public void GeneratedTest_9c3297d8d5a34cabae7e22a1269dfeb6()
        {
            var holds = _player.GetHolds(Card.Hand("4H", "JH", "4S", "3C", "9S"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(0);
            holds.Should().Contain(2);
        }


        [Test]
        public void GeneratedTest_37295abb55cf4faa8c0d5f44de57a8fa()
        {
            var holds = _player.GetHolds(Card.Hand("AC", "6D", "QS", "3S", "KC"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_640f7b3ba29843eaad22e340da1fb721()
        {
            var holds = _player.GetHolds(Card.Hand("KC", "2C", "3C", "KD", "QH"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_b6dd6f498fbc4af68a374b2e7b13c404()
        {
            var holds = _player.GetHolds(Card.Hand("6C", "2H", "2D", "5C"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_7cce0f21742d4da2b7a717ac78d02760()
        {
            var holds = _player.GetHolds(Card.Hand("9S", "7D", "6D", "5C", "TH"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_b773c6f810054e75a62f41c8fa558a48()
        {
            var holds = _player.GetHolds(Card.Hand("6C", "KD", "KH", "7D", "3H"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
        }


        [Test]
        public void GeneratedTest_07d554b60d2f491eafc214ab72efc264()
        {
            var holds = _player.GetHolds(Card.Hand("9S", "AH", "7C", "8S", "5S"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_e0b0cfd718284164876c1c2f061340d2()
        {
            var holds = _player.GetHolds(Card.Hand("5S", "TC", "3S", "KH", "4C"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_f5603be3ffca4be6a86cd30b308bbf03()
        {
            var holds = _player.GetHolds(Card.Hand("AD", "QH", "8S", "4H", "4D"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_37978e693b5f4650b552006a0258083c()
        {
            var holds = _player.GetHolds(Card.Hand("KS", "JS", "JH", "3S", "6S"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_0643fa3d157e46998f5ab581b9753870()
        {
            var holds = _player.GetHolds(Card.Hand("2S", "9C", "AD", "2H", "7H"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(0);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_3cb707f96f904f6e9eb9087f90442074()
        {
            var holds = _player.GetHolds(Card.Hand("KC", "9S", "KH", "8D", "KS"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_fde19d32a7b744f68916b208f09faf53()
        {
            var holds = _player.GetHolds(Card.Hand("TD", "7D", "TS", "QH", "TH"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_33c1831894764b64beaa716ba23e4316()
        {
            var holds = _player.GetHolds(Card.Hand("7H", "KS", "KH", "6C", "9S"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
        }


        [Test]
        public void GeneratedTest_85c411f4bf4e4338968467230f7fc86d()
        {
            var holds = _player.GetHolds(Card.Hand("KD", "4H", "8S", "5H", "7D"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_28caa6570f254f7293ac5465ee3dff04()
        {
            var holds = _player.GetHolds(Card.Hand("5S", "JD", "2C", "6D", "JH"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_e602f014db8540ee9580f0b439216866()
        {
            var holds = _player.GetHolds(Card.Hand("QD", "KH", "8D", "9S", "KC"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_a34decd7d3af46de9a44793dd97f7be1()
        {
            var holds = _player.GetHolds(Card.Hand("5H", "KC", "8H", "3H", "KD"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_c0ffdbd01285412fa45d5ccd87358312()
        {
            var holds = _player.GetHolds(Card.Hand("8H", "TH", "9H", "AS", "8S"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
        }


        [Test]
        public void GeneratedTest_a17db49700ff4862bbe03a9b665c4fdd()
        {
            var holds = _player.GetHolds(Card.Hand("KD", "AD", "8H", "JD", "6S"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_58be352e496a422a9c129091a2472507()
        {
            var holds = _player.GetHolds(Card.Hand("8S", "KS", "4S", "3C", "JH"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_f6760dac72974c9cbdd1e885a532308f()
        {
            var holds = _player.GetHolds(Card.Hand("QS", "7H", "AS", "AH", "5C"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_0e423fd46c0b4ed683523a54383db827()
        {
            var holds = _player.GetHolds(Card.Hand("7D", "5S", "6S", "2H", "AC"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_78257ae5d44b41e09fcff06d79033453()
        {
            var holds = _player.GetHolds(Card.Hand("9D", "AD", "TS", "7S", "5S"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_6aaa03dfb13c4124afb5cb6b820d47bb()
        {
            var holds = _player.GetHolds(Card.Hand("8C", "9C", "7S", "KS", "8H"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(0);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_13edcfaba7054e05a3ba385f7833b136()
        {
            var holds = _player.GetHolds(Card.Hand("7S", "8C", "8S", "2S", "3D"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_b4af8d562bf44c688c2c585c6807b5a6()
        {
            var holds = _player.GetHolds(Card.Hand("5C", "JD", "KD", "JH", "4C"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_2d27311ca1c84dc8ab68dcea2305812f()
        {
            var holds = _player.GetHolds(Card.Hand("9C", "3C", "8H", "JD", "QC"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_009aa94b100141609bce55a72192bb0f()
        {
            var holds = _player.GetHolds(Card.Hand("6D", "TD", "JD", "8H", "3S"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
        }


        [Test]
        public void GeneratedTest_401ce4773d9745959517db38b2841137()
        {
            var holds = _player.GetHolds(Card.Hand("6C", "JS", "AD", "8C", "3C"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_39da204c34a04ddf91f41522ceaa8734()
        {
            var holds = _player.GetHolds(Card.Hand("JH", "3C", "6C", "3H", "JD"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_6e067abfcc5544b294ca50fd203abf73()
        {
            var holds = _player.GetHolds(Card.Hand("5D", "4S", "2D", "2C", "QS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_43988565ecb44947975d2d2d05a01b05()
        {
            var holds = _player.GetHolds(Card.Hand("KC", "AD", "JD", "5H", "AS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_34a6640c28974691b35d39a1aa93fe6c()
        {
            var holds = _player.GetHolds(Card.Hand("KD", "TH", "6H", "AD", "AH"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_7deb842b21f54a87835cbc6e7ac60a2e()
        {
            var holds = _player.GetHolds(Card.Hand("9H", "6S", "5C", "7S", "TD"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_977b9b72b0a74d7f889b1b1353578419()
        {
            var holds = _player.GetHolds(Card.Hand("JD", "KH", "6C", "AC", "QD"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(0);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_89bcdaa9528f403e94c92ee8a71ec4c7()
        {
            var holds = _player.GetHolds(Card.Hand("6H", "5S", "KH", "7D", "6D"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(0);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_c4ca9b68577c462c995d2ea195d5ac49()
        {
            var holds = _player.GetHolds(Card.Hand("8S", "QD", "JH", "JS", "AS"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_bd6442e4fe33423b9fbd83d4f0bf7085()
        {
            var holds = _player.GetHolds(Card.Hand("4H", "6S", "7S", "3D", "5C"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_cd616f68f0ee4483a7657cbb31051597()
        {
            var holds = _player.GetHolds(Card.Hand("5H", "QH", "AC", "5C", "6S"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(0);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_ba78469bb6a04790b23401d73ba02166()
        {
            var holds = _player.GetHolds(Card.Hand("KH", "KC", "4H", "2C", "TS"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_fe503d5f36414df68d1d772e6aceffa6()
        {
            var holds = _player.GetHolds(Card.Hand("6H", "9S", "AS", "AD", "9C"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_ed1c9abda47c4b3dbee96302c6387742()
        {
            var holds = _player.GetHolds(Card.Hand("AD", "5C", "7D", "TC", "4S"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_69575e2f058c4aaf80d159a2c1338db5()
        {
            var holds = _player.GetHolds(Card.Hand("KH", "7D", "TS", "9C", "AH"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_4729b3596f8d4bebbeae98a1483ffdf0()
        {
            var holds = _player.GetHolds(Card.Hand("9D", "8C", "7H", "QH", "AS"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_b9ef722e69a04b0abb2a5c4b45c3d30e()
        {
            var holds = _player.GetHolds(Card.Hand("8S", "2S", "6S", "7H", "KD"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
        }


        [Test]
        public void GeneratedTest_65f171e9a0174aa48359e2d3e0928e53()
        {
            var holds = _player.GetHolds(Card.Hand("4S", "QH", "6C", "KD", "3C"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_622c3fff69234c31953e6f544b99ad4b()
        {
            var holds = _player.GetHolds(Card.Hand("QC", "JS", "3S", "TC", "KS"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_c353d810b51a4fda8ec4dad2e370861b()
        {
            var holds = _player.GetHolds(Card.Hand("QD", "8D", "3C", "5D", "7C"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_1b13bb9ab89b4c8e98228a85250072d5()
        {
            var holds = _player.GetHolds(Card.Hand("AH", "4H", "QS", "4S", "9C"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_3d5f8945b0c646efadc43f89586524b0()
        {
            var holds = _player.GetHolds(Card.Hand("TH", "9D", "4S", "6D", "QH"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(0);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_02ec8664133f4214a7adea88bd63c570()
        {
            var holds = _player.GetHolds(Card.Hand("5H", "6C", "8H", "2S", "TD"));

            holds.Should().HaveCount(1);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_a2878f0b738f4359adedcfcfc4aee20b()
        {
            var holds = _player.GetHolds(Card.Hand("5S", "3C", "2D", "5H", "9S"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_dc020af0d5994f2796cc152d99f0a727()
        {
            var holds = _player.GetHolds(Card.Hand("7H", "TD", "AH", "5H", "8C"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_893bef1347e642f9b304a3fe0614281f()
        {
            var holds = _player.GetHolds(Card.Hand("2D", "8D", "8H", "7C", "QS"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
        }


        [Test]
        public void GeneratedTest_3e823dfdd4224fb78326b5ed5e5f780f()
        {
            var holds = _player.GetHolds(Card.Hand("JD", "TH", "AC", "KH", "4H"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_9cf3980adb3b4f54b838ef71e83466ec()
        {
            var holds = _player.GetHolds(Card.Hand("6C", "5S", "KS", "3D", "4H"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_2de7a3f64a884cc38af4fae4f216e0d8()
        {
            var holds = _player.GetHolds(Card.Hand("8H", "3H", "3C", "8S", "3D"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_f5c7857ffee54e03b458a06e54fdb840()
        {
            var holds = _player.GetHolds(Card.Hand("3C", "AH", "9S", "9D", "7C"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
        }


        [Test]
        public void GeneratedTest_6bfb653bb9ad48d6ab147914398d56c2()
        {
            var holds = _player.GetHolds(Card.Hand("KD", "9D", "9C", "KC", "2D"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_4990cac5ce104c9482cd37cc9af51343()
        {
            var holds = _player.GetHolds(Card.Hand("2S", "TC", "5H", "TS", "5D"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_7e7c2adbc93c41e9b815d60d2cad74c6()
        {
            var holds = _player.GetHolds(Card.Hand("8D", "TH", "3S", "5C", "9H"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_467e8a9ef2784ddb9ff80d3653434850()
        {
            var holds = _player.GetHolds(Card.Hand("KH", "JS", "9D", "4H", "6H"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_2ec3b6c353354192a211da7d3a4810b0()
        {
            var holds = _player.GetHolds(Card.Hand("JC", "KC", "8C", "6H", "KH"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(1);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_2011bae1e744454192f211618c01f257()
        {
            var holds = _player.GetHolds(Card.Hand("QS", "9D", "7D", "JH", "7S"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_f6b8ffee14da45ff96901d550adfaf96()
        {
            var holds = _player.GetHolds(Card.Hand("TD", "KH", "8S", "4S", "AC"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_7293dd4fa3bc4dbaa549ed1033540a50()
        {
            var holds = _player.GetHolds(Card.Hand("QH", "KH", "4D", "6D", "TC"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
        }


        [Test]
        public void GeneratedTest_ca6102bf909649a8bfcb2896a8799bfe()
        {
            var holds = _player.GetHolds(Card.Hand("3H", "9C", "2S", "TH", "5C"));

            holds.Should().HaveCount(1);
            holds.Should().Contain(2);
        }


        [Test]
        public void GeneratedTest_4bacbb8eb44e497d9a56d1dd0ff6fe67()
        {
            var holds = _player.GetHolds(Card.Hand("2D", "TS", "TC", "5D", "5C"));

            holds.Should().HaveCount(5);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_1247ca70d1b14abc98ed3c8602301b64()
        {
            var holds = _player.GetHolds(Card.Hand("7C", "AD", "QC", "8C", "5D"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_69b926424df143f2a4f96e8af4dc4fe5()
        {
            var holds = _player.GetHolds(Card.Hand("7H", "TD", "4C", "6H", "3H"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_a757600321604277940bd3b7de6f3f06()
        {
            var holds = _player.GetHolds(Card.Hand("8S", "6C", "9D", "4C", "9H"));

            holds.Should().HaveCount(2);
            holds.Should().Contain(2);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_b6542f2de9504cbeb0d625d48f3436e7()
        {
            var holds = _player.GetHolds(Card.Hand("TH", "5C", "9D", "6S", "QS"));

            holds.Should().HaveCount(0);
        }


        [Test]
        public void GeneratedTest_47be44d7a60c4ed1b8462996a363da79()
        {
            var holds = _player.GetHolds(Card.Hand("7D", "TD", "KS", "JC", "QC"));

            holds.Should().HaveCount(4);
            holds.Should().Contain(1);
            holds.Should().Contain(2);
            holds.Should().Contain(3);
            holds.Should().Contain(4);
        }


        [Test]
        public void GeneratedTest_7d92c0aa5cc3490fb049c09e5dcc5229()
        {
            var holds = _player.GetHolds(Card.Hand("9H", "9D", "QC", "3S", "2H"));

            holds.Should().HaveCount(3);
            holds.Should().Contain(0);
            holds.Should().Contain(1);
            holds.Should().Contain(4);
        }



        #endregion
    }
}
