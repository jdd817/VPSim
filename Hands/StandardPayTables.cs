using Hands.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands
{
    public class StandardPayTables
    {
        public static PayTable JoB_9954
        {
            get
            {
                return new PayTable()
                {
                    PayLines = new List<PayLine>
                    {
                        new PayLine {Name="Jacks or Better", MinValue=1.11m, MaxValue=1.15m, Payout=1, MaxBetMultiplier=1 },
                        new PayLine {Name="Two Pair", MinValue=2.0m, MaxValue=2.99m, Payout=2, MaxBetMultiplier=1 },
                        new PayLine {Name="Three of a Kind", MinValue=3.0m, MaxValue=3.99m, Payout=3, MaxBetMultiplier=1 },
                        new PayLine {Name="Strait", MinValue=4.0m, MaxValue=4.99m, Payout=4, MaxBetMultiplier=1 },
                        new PayLine {Name="Flush", MinValue=5.0m, MaxValue=5.99m, Payout=6, MaxBetMultiplier=1 },
                        new PayLine {Name="Full House", MinValue=6.0m, MaxValue=6.99m, Payout=9, MaxBetMultiplier=1 },
                        new PayLine {Name="Four of a Kind", MinValue=7.0m, MaxValue=7.99m, Payout=25, MaxBetMultiplier=1 },
                        new PayLine {Name="Strait Flush", MinValue=8.0m, MaxValue=8.1312111009m, Payout=50, MaxBetMultiplier=1 },
                        new PayLine {Name="RoyalFlush", MinValue=8.1413121110m, MaxValue=8.1413121110m, Payout=400, MaxBetMultiplier=2 },
                    }
                };
            }
        }

        public static PayTable JoB_9845
        {
            get
            {
                return new PayTable()
                {
                    PayLines = new List<PayLine>
                    {
                        new PayLine {Name="Jacks or Better", MinValue=1.11m, MaxValue=1.15m, Payout=1, MaxBetMultiplier=1 },
                        new PayLine {Name="Two Pair", MinValue=2.0m, MaxValue=2.99m, Payout=2, MaxBetMultiplier=1 },
                        new PayLine {Name="Three of a Kind", MinValue=3.0m, MaxValue=3.99m, Payout=3, MaxBetMultiplier=1 },
                        new PayLine {Name="Strait", MinValue=4.0m, MaxValue=4.99m, Payout=4, MaxBetMultiplier=1 },
                        new PayLine {Name="Flush", MinValue=5.0m, MaxValue=5.99m, Payout=5, MaxBetMultiplier=1 },
                        new PayLine {Name="Full House", MinValue=6.0m, MaxValue=6.99m, Payout=9, MaxBetMultiplier=1 },
                        new PayLine {Name="Four of a Kind", MinValue=7.0m, MaxValue=7.99m, Payout=25, MaxBetMultiplier=1 },
                        new PayLine {Name="Strait Flush", MinValue=8.0m, MaxValue=8.1312111009m, Payout=50, MaxBetMultiplier=1 },
                        new PayLine {Name="RoyalFlush", MinValue=8.1413121110m, MaxValue=8.1413121110m, Payout=400, MaxBetMultiplier=2 },
                    }
                };
            }
        }

        public static PayTable DDB_9898
        {
            get
            {
                return new PayTable()
                {
                    PayLines = new List<PayLine>
                    {
                        new PayLine {Name="Jacks or Better", MinValue=1.11m, MaxValue=1.15m, Payout=1, MaxBetMultiplier=1 },
                        new PayLine {Name="Two Pair", MinValue=2.0m, MaxValue=2.99m, Payout=1, MaxBetMultiplier=1 },
                        new PayLine {Name="Three of a Kind", MinValue=3.0m, MaxValue=3.99m, Payout=3, MaxBetMultiplier=1 },
                        new PayLine {Name="Strait", MinValue=4.0m, MaxValue=4.99m, Payout=4, MaxBetMultiplier=1 },
                        new PayLine {Name="Flush", MinValue=5.0m, MaxValue=5.99m, Payout=6, MaxBetMultiplier=1 },
                        new PayLine {Name="Full House", MinValue=6.0m, MaxValue=6.99m, Payout=9, MaxBetMultiplier=1 },
                        new PayLine {Name="Four 5s through Ks", MinValue=7.05m, MaxValue=7.1399m, Payout=50, MaxBetMultiplier=1 },
                        new PayLine {Name="Four 2s,3s,4s", MinValue=7.02m, MaxValue=7.0499m, Payout=80, MaxBetMultiplier=1 },
                        new PayLine {Name="Four As", MinValue=7.14m, MaxValue=7.1499m, Payout=160, MaxBetMultiplier=1 },
                        new PayLine {Name="Four 2s with A", MinValue=7.0214m, MaxValue=7.0214m, Payout=160, MaxBetMultiplier=1 },
                        new PayLine {Name="Four 2s with any 2,3,4", MinValue=7.0202m, MaxValue=7.0204m, Payout=160, MaxBetMultiplier=1 },
                        new PayLine {Name="Four 3s with A", MinValue=7.0314m, MaxValue=7.0314m, Payout=160, MaxBetMultiplier=1 },
                        new PayLine {Name="Four 3s with any 2,3,4", MinValue=7.0302m, MaxValue=7.0304m, Payout=160, MaxBetMultiplier=1 },
                        new PayLine {Name="Four 4s with A", MinValue=7.0414m, MaxValue=7.0414m, Payout=160, MaxBetMultiplier=1 },
                        new PayLine {Name="Four 4s with any 2,3,4", MinValue=7.0402m, MaxValue=7.0404m, Payout=160, MaxBetMultiplier=1 },
                        new PayLine {Name="Four Aces with any 2,3,4", MinValue=7.1402m, MaxValue=7.1404m, Payout=400, MaxBetMultiplier=1 },
                        new PayLine {Name="Strait Flush", MinValue=8.0m, MaxValue=8.1312111009m, Payout=50, MaxBetMultiplier=1 },
                        new PayLine {Name="RoyalFlush", MinValue=8.1413121110m, MaxValue=8.1413121110m, Payout=400, MaxBetMultiplier=2 },
                    }
                };
            }
        }

        public static PayTable DB_9911
        {
            get
            {
                return new PayTable()
                {
                    PayLines = new List<PayLine>
                    {
                        new PayLine {Name="Jacks or Better", MinValue=1.11m, MaxValue=1.15m, Payout=1, MaxBetMultiplier=1 },
                        new PayLine {Name="Two Pair", MinValue=2.0m, MaxValue=2.99m, Payout=1, MaxBetMultiplier=1 },
                        new PayLine {Name="Three of a Kind", MinValue=3.0m, MaxValue=3.99m, Payout=3, MaxBetMultiplier=1 },
                        new PayLine {Name="Strait", MinValue=4.0m, MaxValue=4.99m, Payout=5, MaxBetMultiplier=1 },
                        new PayLine {Name="Flush", MinValue=5.0m, MaxValue=5.99m, Payout=7, MaxBetMultiplier=1 },
                        new PayLine {Name="Full House", MinValue=6.0m, MaxValue=6.99m, Payout=9, MaxBetMultiplier=1 },
                        new PayLine {Name="Four 5s through Ks", MinValue=7.05m, MaxValue=7.1399m, Payout=50, MaxBetMultiplier=1 },
                        new PayLine {Name="Four 2s,3s,4s", MinValue=7.02m, MaxValue=7.0499m, Payout=80, MaxBetMultiplier=1 },
                        new PayLine {Name="Four As", MinValue=7.14m, MaxValue=7.1499m, Payout=160, MaxBetMultiplier=1 },
                        new PayLine {Name="Strait Flush", MinValue=8.0m, MaxValue=8.1312111009m, Payout=50, MaxBetMultiplier=1 },
                        new PayLine {Name="RoyalFlush", MinValue=8.1413121110m, MaxValue=8.1413121110m, Payout=400, MaxBetMultiplier=2 },
                    }
                };
            }
        }

        public static PayTable DW44
        {
            get
            {
                return new PayTable()
                {
                    PayLines = new List<PayLine>
                    {
                        new PayLine {Name="Three of a Kind", MinValue=3.0m, MaxValue=3.99m, Payout=1, MaxBetMultiplier=1 },
                        new PayLine {Name="Strait", MinValue=4.0m, MaxValue=4.99m, Payout=2, MaxBetMultiplier=1 },
                        new PayLine {Name="Flush", MinValue=5.0m, MaxValue=5.99m, Payout=3, MaxBetMultiplier=1 },
                        new PayLine {Name="Full House", MinValue=6.0m, MaxValue=6.99m, Payout=4, MaxBetMultiplier=1 },
                        new PayLine {Name="Four of a Kind", MinValue=7.03m, MaxValue=7.99m, Payout=4, MaxBetMultiplier=1 },
                        new PayLine {Name="Strait Flush", MinValue=8.0m, MaxValue=8.1312111009m, Payout=10, MaxBetMultiplier=1 },
                        new PayLine {Name="Five Of A Kind", MinValue=9.0m, MaxValue=9.99m, Payout=12, MaxBetMultiplier=1 },
                        new PayLine {Name="Wild Royal Flush", MinValue=8.1413121110m, MaxValue=8.1413121110m, Payout=20, MaxBetMultiplier=2 },
                        new PayLine {Name="Four Deuces", MinValue=7.02m, MaxValue=7.029m, Payout=200, MaxBetMultiplier=2 },
                        new PayLine {Name="Natural Royal Flush", MinValue=10.1413121110m, MaxValue=10.1413121110m, Payout=1120, MaxBetMultiplier=2 },
                    }
                };
            }
        }
    }
}
