using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class VpMachine
{
    /*
     * TODOS:
     * 
     * UI to show original hand when a mistake is made
     * Add credits display - done
     * Add winnings display - done
     * Add Bet display - done
     * Impliment multiple hands -done
     *   List count of hits on each payline -done
     * Show paytable
     * 
     * Move held to top of card -done
     * Move held down a bit -done
     * Change mistake display to something different
     * Move UI elements down on screen -done
     * Make main hand a little bigger -done
     * Add box around Hand and Bet indicators
     * Enable swipe holds (low priority)
     * Paylines for individual hands hard to read -done (color coded paylines)
     * 
     * Display error can occur when displaying aces (had a set of ace of spades....) -done
     * 
     * Performance improvements:
     * Change data update mechanism to be event based not running every update cycle -done
     * 
     * Possible enhancements:
     * 
     * Add sound
     * 
     * Training Modes (not mutually exlusive):
     * Show cards that should have been held
     * Tally mistakes -done
     * Disallow draw when mistakes present (2 versions, 1 that shows the correct holds, 1 that doesnt) -done
     *   Add toggle for above -done
     * 
     * */
    public static Hands.Entities.Card[] Hand { get; set; }
    public static bool[] Holds { get; set; }
    public static bool[] CorrectHolds { get; set; }
    public static int Credits { get; set; }
    public static int Bet { get; set; }
    public static int HandsPlayed { get; set; }
    public static int GameState { get; set; }
    public static int AmountWon { get; set; }

    public static Hands.HandResult MainHandResult { get; set; }
    public static Hands.HandResult[] HandResults{ get; set; }

    public static Hands.Entities.PayTable PayTable { get; set; }

    public static bool BlockIncorrectHolds { get; set; }

    private static Hands.VideoPokerController _controller;
    private static VPPlayer.VpPlayer _player;

    public const int MaxHands = 25;  //this will eventually be setable from outside

    public static Statistics statistics { get; set; }
    private static DateTime handDealtAt { get; set; }

    public static void Init()
    {
        PayTable = Hands.StandardPayTables.JoB_9845;
        _controller = new Hands.VideoPokerController(new Hands.Poker.VideoPokerPayoutCalculator(new Hands.Poker.HandAnalyzer(
                System.Reflection.Assembly.GetAssembly(typeof(Hands.Poker.HandAnalyzer)).GetTypes()
                    .Where(t => typeof(Hands.Poker.Hands.IHandChecker).IsAssignableFrom(t) && t != typeof(Hands.Poker.Hands.IHandChecker))
                    .Where(t => t.Namespace.Contains("JacksOrBetter"))
                    .Select(ht => Activator.CreateInstance(ht))
                    .OfType<Hands.Poker.Hands.IHandChecker>()
                    .ToArray()
                )));

        _player = new VPPlayer.VpPlayer(System.Reflection.Assembly.GetAssembly(typeof(VPPlayer.VpPlayer)).GetTypes()
                .Where(t => typeof(VPPlayer.IHandTier).IsAssignableFrom(t) && t != typeof(VPPlayer.IHandTier))
                .Where(t => t.Namespace.Contains("Job"))
                .Select(ht => Activator.CreateInstance(ht))
                .OfType<VPPlayer.IHandTier>()
                .ToArray());

        Holds = new bool[5];
        CorrectHolds = new bool[5];

        GameState = 1;
        HandsPlayed = 1;
        Bet = 1;
        Credits = 0;
        AmountWon = 0;
        HandResults = new Hands.HandResult[0];

        BlockIncorrectHolds = true;  //this needs to be a set via UI
    }

    public static void IncreaseBet()
    {
        if (GameState == 1)
        {
            Bet++;
            if (Bet > 5)
                Bet = 1;
            OnBetChange?.Invoke(Bet);
        }
    }

    public static void MaxBet()
    {
        if (GameState == 1)
        {
            Bet = 5;
            HandsPlayed = MaxHands;
            OnHandsPlayedChange?.Invoke(HandsPlayed);
            OnBetChange?.Invoke(Bet);
            Deal();
        }
    }

    public static void IncreaseHands()
    {
        if(GameState==1)
        {
            HandsPlayed++;
            if (HandsPlayed > MaxHands)
                HandsPlayed = 1;
            OnHandsPlayedChange?.Invoke(HandsPlayed);
        }
    }

    public static void ToggleIncorrectPlays()
    {
        BlockIncorrectHolds = !BlockIncorrectHolds;
    }

    public static bool Deal()
    {
        if (GameState == 1)
            return DealNewHand();
        else if(GameState==2)
            return DrawCards();
        return true;
    }

    private static bool DealNewHand()
    {
        handDealtAt = DateTime.Now;
        Credits -= Bet * HandsPlayed;
        for(var i=0;i<Holds.Length;i++)
        {
            Holds[i] = false;
            CorrectHolds[i] = false;
        }
        GameState = 2;
        statistics.HandsPlayed++;

        Hand = _controller.DealHand();
        MainHandResult = _controller.ResolveHand(Hand, Bet, PayTable);
        
        var correctPlay = _player.GetHolds(Hand);
        foreach (var play in correctPlay)
            CorrectHolds[play] = true;

        OnHandDealt?.Invoke(new DealEventArgs
        {
            Hand = Hand,
            MainHandResult = MainHandResult,
            CorrectHolds = CorrectHolds
        });
        return true;
    }

    private static bool DrawCards()
    {
        var good = true;
        for (var i = 0; i < 5; i++)
            if (Holds[i] != CorrectHolds[i])
                good = false;
        if (!good)
        {
            statistics.MistakesMade++;
            if (BlockIncorrectHolds)
                return false;
        }

        var timeTaken = (DateTime.Now - handDealtAt).TotalSeconds;
        statistics.AvgHandLength = (statistics.AvgHandLength * (statistics.HandsPlayed - 1) + timeTaken) / statistics.HandsPlayed;
        

        var heldCards = new List<Hands.Entities.Card>();
        for (var i = 0; i < Holds.Length; i++)
            if (Holds[i])
                heldCards.Add(Hand[i]);

        var results = new Hands.HandResult[HandsPlayed];

        for (var i = 0; i < HandsPlayed; i++)
            results[i] = _controller.ResolveHand(heldCards.ToArray(), Bet, PayTable);

        //re-order the cards so the hold cards are in the right spot
        foreach(var result in results)
        {
            var reorderedHand = new List<Hands.Entities.Card>();
            var i = 0;
            for (var j = 0; j < 5; j++)
            {
                if (heldCards.Contains(Hand[j]))
                    reorderedHand.Add(Hand[j]);
                else
                {
                    if (Hand[j] != result.Hand[i])
                        reorderedHand.Add(result.Hand[i]);
                    i++;
                }
            }
            result.Hand = reorderedHand.ToArray();
        }

        MainHandResult = results[0];
        Hand = results[0].Hand;
        HandResults = results;

        //tabulate winnings
        AmountWon = results.Sum(r => r.Payout);
        Credits += AmountWon;

        GameState = 1;

        OnCardsDrawn?.Invoke(new DrawEventArgs
        {
            Hand = Hand,
            MainHandResult = MainHandResult,
            CorrectHolds = CorrectHolds,
            AmountWon = AmountWon,
            HandResults = HandResults
        });

        return true;
    }

    public class DealEventArgs
    {
        public Hands.Entities.Card[] Hand { get; set; }
        public Hands.HandResult MainHandResult { get; set; }
        public bool[] CorrectHolds { get; set; }
    }
    public class DrawEventArgs : DealEventArgs
    {
        public int AmountWon { get; set; }
        public Hands.HandResult[] HandResults { get; set; }
    }

    public static event Action<DealEventArgs> OnHandDealt;
    public static event Action<DrawEventArgs> OnCardsDrawn;
    public static event Action<int> OnHandsPlayedChange;
    public static event Action<int> OnBetChange;
}

