using Hands;
using Hands.Entities;
using Hands.Poker;
using Hands.Poker.Hands;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPPlayer;

namespace VP
{
    class Program
    {
        static IHandChecker[] JacksOrBetterCheckers = new IHandChecker[]
                {
                    new Hands.Poker.Hands.JacksOrBetter.HighCardChecker(),
                    new Hands.Poker.Hands.JacksOrBetter.PairChecker(),
                    new Hands.Poker.Hands.JacksOrBetter.TwoPairChecker(),
                    new Hands.Poker.Hands.JacksOrBetter.ThreeOfAKindChecker(),
                    new Hands.Poker.Hands.JacksOrBetter.StraitChecker(),
                    new Hands.Poker.Hands.JacksOrBetter.FlushChecker(),
                    new Hands.Poker.Hands.JacksOrBetter.FullHouseChecker(),
                    new Hands.Poker.Hands.JacksOrBetter.FourOfAKindChecker(),
                    new Hands.Poker.Hands.JacksOrBetter.StraitFlushChecker()
                };

        static IHandChecker[] DeucesWildCheckers = new IHandChecker[]
                {
                    new Hands.Poker.Hands.DeucesWild.HighCardChecker(),
                    new Hands.Poker.Hands.DeucesWild.PairChecker(),
                    new Hands.Poker.Hands.DeucesWild.TwoPairChecker(),
                    new Hands.Poker.Hands.DeucesWild.ThreeOfAKindChecker(),
                    new Hands.Poker.Hands.DeucesWild.StraitChecker(),
                    new Hands.Poker.Hands.DeucesWild.FlushChecker(),
                    new Hands.Poker.Hands.DeucesWild.FullHouseChecker(),
                    new Hands.Poker.Hands.DeucesWild.FourOfAKindChecker(),
                    new Hands.Poker.Hands.DeucesWild.StraitFlushChecker()
                };

        static void Main(string[] args)
        {
            var dwVp = new VideoPokerMachine(new VideoPokerController(new VideoPokerPayoutCalculator(new HandAnalyzer(DeucesWildCheckers))));
            var jobVp = new VideoPokerMachine(new VideoPokerController(new VideoPokerPayoutCalculator(new HandAnalyzer(JacksOrBetterCheckers))));

            //vp.GameSelect(StandardPayTables.DW44);

            

            var dollarsPerTier = 10;
            //var dollarsPerCredit = 1m;
            //var hands = 2;
            var start = 2000;
            //var gameName = "Deuces Wild 44";
            //var playerType = "DW";

            var games = new[]
            {/*
                new
                {
                    gameName="Deuces Wild 44",
                    playerType="DW",
                    game=dwVp,
                    payTable=StandardPayTables.DW44,
                    dollarsPerTier = 10,
                    configs=new[]
                    {
                        new
                        {
                            denom=1m,
                            target = 5000,
                            bankroll = 5000,
                            hands=new[] {1,3,5,10,25,50}
                        }
                    }
                },
                new
                {
                    gameName="Jacks or Better 99.54",
                    playerType="Job",
                    game=jobVp,
                    payTable=StandardPayTables.JoB_9954,
                    dollarsPerTier = 25,
                    configs=new[]
                    {
                        new
                        {
                            denom=5m,
                            target = 5000,
                            bankroll = 5000,
                            hands=new[] {1}
                        },
                        new
                        {
                            denom=10m,
                            target = 5000,
                            bankroll = 5000,
                            hands=new[] {1}
                        }
                    }
                },*/
                new
                {
                    gameName="Jacks or Better 98.45",
                    playerType="Job",
                    game=jobVp,
                    payTable=StandardPayTables.JoB_9845,
                    dollarsPerTier = 10,
                    configs=new[]
                    {
                        new
                        {
                            denom=0.25m,
                            target = 5000,
                            bankroll = 5000,
                            hands=new[] {25, 50,100}
                        },
                        new
                        {
                            denom=0.5m,
                            target = 5000,
                            bankroll = 5000,
                            hands=new[] {25, 50,100}
                        },
                        new
                        {
                            denom=1m,
                            target = 5000,
                            bankroll = 5000,
                            hands=new[] {1,3,5,10,25,50,100}
                        },
                        new
                        {
                            denom=2m,
                            target = 5000,
                            bankroll = 5000,
                            hands=new[] {1,3,5,10,25,50, 100}
                        },
                        new
                        {
                            denom=5m,
                            target = 5000,
                            bankroll = 5000,
                            hands=new[] {1,3,5,10}
                        },
                        new
                        {
                            denom=10m,
                            target = 5000,
                            bankroll = 5000,
                            hands = new[] {1,2,3,5}
                        }
                    }
                }
            };

            //PlayManual(vp, dollarsPerCredit, hands, dollarsPerTier, start);

            //return;

            var endTime = new DateTime(2018, 7, 2, 22, 00, 0);
            
            while (DateTime.Now<endTime)
            {
                var startTime = DateTime.Now;

                foreach (var game in games)
                {
                    foreach(var config in game.configs)
                    {
                        foreach (var hands in config.hands)
                        {
                            var vp = game.game;
                            vp.GameSelect(game.payTable);
                            
                            RunSim(vp, game.dollarsPerTier, config.denom, config.bankroll, config.target, game.gameName, game.playerType, hands, 50);
                        }
                    }
                }

                var totalTime = DateTime.Now - startTime;

                if (DateTime.Now + totalTime >= endTime)
                    break;
            }

            Console.WriteLine("Done!");
        }

        private static void RunSim(VideoPokerMachine vp, int dollarsPerTier, decimal dollarsPerCredit, int start, int targetPoints, string gameName, string playerType, int hands, int runCount)
        {
            Console.WriteLine("{0} ${1} x {2}", gameName, dollarsPerCredit, hands);
            //var resultsList = new List<PlayResults>();
            using (var conn = new SqlConnection("Data Source=localhost; Initial Catalog=vp; Integrated Security=true"))
            {
                conn.Open();
                var cmd = new SqlCommand("AddResult", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Game", System.Data.SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new SqlParameter("DollarsPerTierCredit", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("DollarsPerCredit", System.Data.SqlDbType.Float));
                cmd.Parameters.Add(new SqlParameter("HandsPlayed", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("StartCredits", System.Data.SqlDbType.Float));
                cmd.Parameters.Add(new SqlParameter("EndCredits", System.Data.SqlDbType.Float));
                cmd.Parameters.Add(new SqlParameter("TotalHandsPlayed", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("CoinIn", System.Data.SqlDbType.Float));
                cmd.Parameters.Add(new SqlParameter("TierCreditsEarned", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("TargetPoints", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("Bankroll", System.Data.SqlDbType.Int));

                cmd.Parameters["Game"].Value = gameName;
                cmd.Parameters["DollarsPerTierCredit"].Value = dollarsPerTier;
                cmd.Parameters["DollarsPerCredit"].Value = dollarsPerCredit;
                cmd.Parameters["StartCredits"].Value = start;
                cmd.Parameters["HandsPlayed"].Value = hands;
                cmd.Parameters["TargetPoints"].Value = targetPoints;
                cmd.Parameters["Bankroll"].Value = start;

                for (var i = 0; i < runCount; i++)
                {
                    var results = PlayAutomated(vp, dollarsPerCredit, hands, dollarsPerTier, start, targetPoints, playerType);

                    //Console.WriteLine("Game Results:");
                    //Console.WriteLine("  Credits: {0} (${2:0.00})\r\n  Tier Credits: {1}", results.CreditsLeft, results.TierCredits, results.CreditsLeft * dollarsPerCredit);

                    //resultsList.Add(results);

                    cmd.Parameters["EndCredits"].Value = results.CreditsLeft * dollarsPerCredit;
                    cmd.Parameters["TotalHandsPlayed"].Value = results.HandsPlayed;
                    cmd.Parameters["CoinIn"].Value = results.CoinIn;
                    cmd.Parameters["TierCreditsEarned"].Value = results.TierCredits;                    

                    cmd.ExecuteNonQuery();
                }
            }
            /*
            using (var writer = new StreamWriter("c:\\vpresults.txt", true))
            {
                writer.WriteLine("$/Tier, $/Credit, Hands, Start, End, Tier Credits");
                foreach (var result in resultsList)
                {
                    writer.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",
                        dollarsPerTier, dollarsPerCredit, hands, start,
                        result.CreditsLeft, result.TierCredits);
                }
            }*/
            /*
            var lineCount = 0;
            using (var writer = new StreamWriter("c:\\vpstats.sql", true))
            {
                foreach (var result in resultsList)
                {
                    if (lineCount % 1000 == 0)
                        writer.WriteLine("Insert VpStats(DpTier, DpCredit, Hands, Start, [End], TierCredits) Values");
                    lineCount++;
                    writer.WriteLine("({0}, {1}, {2}, {3}, {4}, {5}){6}",
                        dollarsPerTier, dollarsPerCredit, hands, start,
                        result.CreditsLeft, result.TierCredits,
                        lineCount % 1000 == 0 ? ";" : ",");
                }
            }*/
            /*
            using (var writer = new StreamWriter("c:\\vpsummary.txt", true))
            {
                writer.WriteLine("$/Tier, $/Credit, Hands, Start, Tier, Hits, Percentage");

                var tiers = new[]
                {
                    resultsList.Where(r=>r.TierCredits<500).Count(),
                    resultsList.Where(r=>r.TierCredits>=500 && r.TierCredits<1000).Count(),
                    resultsList.Where(r=>r.TierCredits>=1000 && r.TierCredits<2500).Count(),
                    resultsList.Where(r=>r.TierCredits>=2500 && r.TierCredits<5000).Count(),
                    resultsList.Where(r=>r.TierCredits>=5000).Count()
                };
                for (var i = 0; i < tiers.Length; i++)
                    writer.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6:0.00}%",
                        dollarsPerTier, dollarsPerCredit, hands, start,
                        i, tiers[i], ((double)tiers[i] * 100) / resultsList.Count);
            }*/
        }

        class PlayResults
        {
            public int CreditsLeft { get; set; }
            public int CoinIn { get; set; }
            public int TierCredits { get; set; }
            public int HandsPlayed { get; set; }
        }

        static PlayResults PlayManual(VideoPokerMachine vp, decimal creditValue, int handsPlayed, int dollarPerTierCredit, int startingBankroll)
        {
            vp.Credits = (int)(startingBankroll / creditValue);
            vp.HandsBet = handsPlayed;

            var coinIn = 0;

            while (true)
            {
                Console.WriteLine("Credits Available: {0}", vp.Credits);
                Console.WriteLine("Tier Credits Earned: {0}", (int)((coinIn * creditValue) / dollarPerTierCredit));

                if ((int)((coinIn * creditValue) / dollarPerTierCredit) >= 5000)
                {
                    Console.WriteLine("************** Made Diamond! *******************");
                    Console.ReadKey();
                    break;
                }

                if (vp.Credits < vp.BetPerHand * vp.HandsBet)
                {
                    Console.WriteLine("!!!!!!!!!!!!!BUSTED!!!!!!!!!!!!!!!!");
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine("Press space to max bet");
                var key = Console.ReadKey().KeyChar;
                if (key != ' ' && key != '0')
                    break;

                vp.MaxBet();
                coinIn += vp.BetPerHand * vp.HandsBet;

                key = 'x';
                while (key != ' ' && key != '0')
                {
                    Console.WriteLine();
                    for (var i = 0; i < vp.Hand.Length; i++)
                    {
                        if (vp.Hand[i].Suit % 2 == 0)
                            Console.ForegroundColor = ConsoleColor.White;
                        else
                            Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("{0} {1}: {2}", vp.Holds[i] ? "X" : " ", i + 1, vp.Hand[i]);
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    var patHand = vp.CheckCurrentHand();
                    if (patHand.Payout > 0)
                    {
                        Console.WriteLine("\r\n-------Dealt a {0}!-------\r\n", patHand.PayLineHit);
                    }
                    Console.WriteLine("Space or 0 for deal, 1-5 to hold.");
                    key = Console.ReadKey().KeyChar;

                    switch (key)
                    {
                        case '1': vp.Hold(0); break;
                        case '2': vp.Hold(1); break;
                        case '3': vp.Hold(2); break;
                        case '4': vp.Hold(3); break;
                        case '5': vp.Hold(4); break;
                    }
                }

                using (var writer = new StreamWriter("c:\\vptests.cs", true))
                {
                    var holds = new List<int>();
                    for (var i = 0; i < vp.Holds.Length; i++)
                        if (vp.Holds[i])
                            holds.Add(i);

                    writer.WriteLine("\r\n[Test]");
                    writer.WriteLine("public void GeneratedTest_{0}()", Guid.NewGuid().ToString().Replace("-", ""));
                    writer.WriteLine("{");
                    writer.WriteLine("var holds = _player.GetHolds(Card.Hand({0}));", vp.Hand.Select(c => String.Format("\"{0}\"", c)).Aggregate((a, b) => a + ", " + b));
                    writer.WriteLine();
                    writer.WriteLine("holds.Should().HaveCount({0});", holds.Count);
                    foreach (var hold in holds)
                        writer.WriteLine("holds.Should().Contain({0});", hold);
                    writer.WriteLine("}\r\n");
                }

                vp.Deal();

                Console.WriteLine("Results:");

                foreach (var result in vp.Results)
                {
                    Console.WriteLine("Hand: {0} Payout {1} for {2}",
                        result.Hand.Select(c => c.ToString()).Aggregate((a, b) => a + " " + b),
                        result.Payout,
                        result.PayLineHit);
                }
                Console.WriteLine();

                if (vp.HandsBet > 10)
                {
                    Console.WriteLine("Payout Summary:");
                    foreach (var payLine in vp.Results.GroupBy(pl => new { pl.PayLineHit, pl.Payout }).OrderBy(pl => pl.Key.Payout))
                        Console.WriteLine("{0}: {1} hits, {2} each for {3} total", payLine.Key.PayLineHit, payLine.Count(), payLine.Key.Payout, payLine.Select(pl => pl.Payout).Sum());
                    Console.WriteLine();
                }

                Console.WriteLine("Total payout {0}", vp.Results.Select(r => r.Payout).Sum());

            }

            return new PlayResults
            {
                CreditsLeft = vp.Credits,
                CoinIn = coinIn,
                TierCredits = (int)((coinIn * creditValue) / dollarPerTierCredit)
            };
        }

        static PlayResults PlayAutomated(VideoPokerMachine vp, decimal creditValue, int handsPlayed, int dollarPerTierCredit, int startingBankroll, int targetPoints, string playerType="Job")
        {
            vp.Credits = (int)(startingBankroll / creditValue);
            vp.HandsBet = handsPlayed;

            var player = new VpPlayer(System.Reflection.Assembly.GetAssembly(typeof(VpPlayer)).GetTypes()                
                .Where(t => typeof(IHandTier).IsAssignableFrom(t) && t != typeof(IHandTier))
                .Where(t => t.Namespace.Contains(playerType))
                .Select(ht => Activator.CreateInstance(ht))
                .OfType<IHandTier>()
                .ToArray());

            var coinIn = 0;
            var hands = 0;
            //var i = 0;
            
            while (true)
            {
                if ((int)((coinIn * creditValue) / dollarPerTierCredit) >= targetPoints)
                {
                    //Console.WriteLine("************** Made Plat! *******************");
                    break;
                }

                if (vp.Credits < vp.BetPerHand * vp.HandsBet)
                {
                    vp.Credits = 0;
                    //Console.WriteLine("!!!!!!!!!!!!!BUSTED!!!!!!!!!!!!!!!!");
                    break;
                }

                vp.MaxBet();
                coinIn += vp.BetPerHand * vp.HandsBet;
                hands++;

                foreach (var hold in player.GetHolds(vp.Hand))
                    vp.Hold(hold);
                
                vp.Deal();
                /*
                i++;
                if(i%100==0)
                {
                    Console.WriteLine("Credits: {0}   Tier Credits:{1}", vp.Credits, (int)((coinIn * creditValue) / dollarPerTierCredit));
                }*/

            }

            return new PlayResults
            {
                CreditsLeft = vp.Credits,
                CoinIn = coinIn,
                TierCredits = (int)((coinIn * creditValue) / dollarPerTierCredit),
                HandsPlayed = hands
            };
        }
    }
}
