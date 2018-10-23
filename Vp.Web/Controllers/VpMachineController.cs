using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Hands;
using VPPlayer;
using Hands.Poker;
using Hands.Poker.Hands;
using Hands.Entities;

namespace Vp.Web.Controllers
{
    public class VpMachineController:ApiController
    {
        private VideoPokerController _vp;
        private VpPlayer _player;

        public VpMachineController()
        {
            _vp = new VideoPokerController(new VideoPokerPayoutCalculator(new HandAnalyzer(
                System.Reflection.Assembly.GetAssembly(typeof(HandAnalyzer)).GetTypes()
                    .Where(t => typeof(IHandChecker).IsAssignableFrom(t) && t != typeof(IHandChecker))
                    .Where(t => t.Namespace.Contains("JacksOrBetter"))
                    .Select(ht => Activator.CreateInstance(ht))
                    .OfType<IHandChecker>()
                    .ToArray()
                )));

            _player = new VpPlayer(System.Reflection.Assembly.GetAssembly(typeof(VpPlayer)).GetTypes()
                .Where(t => typeof(IHandTier).IsAssignableFrom(t) && t != typeof(IHandTier))
                .Where(t => t.Namespace.Contains("Job"))
                .Select(ht => Activator.CreateInstance(ht))
                .OfType<IHandTier>()
                .ToArray());
        }

        [Route("VpMachine/Games")]
        [HttpGet]
        public IEnumerable<object> GetGames()
        {
            return new[]
            {
                /*new
                {
                    Game="Jacks Or Better 99.54",
                    Paytable = StandardPayTables.JoB_9954
                },*/
                new
                {
                    Game="Jacks Or Better 98.45",
                    Paytable = StandardPayTables.JoB_9845
                }
            };
        }

        [Route("VpMachine/DealHand")]
        [HttpGet]
        public IEnumerable<object> DealHand()
        {
            return _vp.DealHand().Select(c => c.ToString());
        }

        [Route("VpMachine/CheckHand")]
        public object CheckHand(ResultQuery query)
        {
            var result = _vp.ResolveHand(Card.Hand(query.Hand.ToArray()), query.Bet, query.PayTable);

            return new TransformedResult
            {
                Hand = result.Hand.Select(c => c.ToString()).ToList(),
                PayLineHit = result.PayLineHit,
                Payout = result.Payout
            };
        }

        [Route("VpMachine/GetResults")]
        [HttpPost]
        public object GetResults(ResultQuery query)
        {
            var results = new List<HandResult>();
            _vp.SetHand(Card.Hand(query.Hand.ToArray()));

            var holdCards = Card.Hand(query.HeldCards.ToArray());

            for (var i = 0; i < query.Hands; i++)
            {
                results.Add(_vp.ResolveHand(holdCards, query.Bet, query.PayTable));
            }

            var transformedResults = results.Select(r =>
              new TransformedResult
              {
                  Hand = r.Hand.Select(c => c.ToString()).ToList(),
                  PayLineHit = r.PayLineHit,
                  Payout = r.Payout
              }).ToList();

            foreach (var result in transformedResults)
            {
                var reorderedHand = new List<string>();
                var i = 0;
                for(var j = 0; j < 5; j++)
                {
                    if (query.HeldCards.Contains(query.Hand[j]))
                        reorderedHand.Add(query.Hand[j]);
                    else
                    {
                        if (query.Hand[j] != result.Hand[i])
                            reorderedHand.Add(result.Hand[i]);
                        i++;
                    }
                }
                result.Hand = reorderedHand;
            }

            var correctPlay = _player.GetHolds(Card.Hand(query.Hand.ToArray()));


            return new
            {
                Hand = transformedResults[0],
                Results = transformedResults.Skip(1),
                CreditsPayed = results.Sum(r => r.Payout),
                CorrectPlay = correctPlay.Select(i => query.Hand[i]),
                CorrectPlayIndexes = correctPlay
            };
        }

        public class TransformedResult
        {
            public List<string> Hand { get; set; }
            public string PayLineHit { get; set; }
            public int Payout { get; set; }
        }

        public class ResultQuery
        {
            public List<string> Hand { get; set; }
            public List<string> HeldCards { get; set; }
            public int Bet { get; set; }
            public int Hands { get; set; }
            public PayTable PayTable { get; set; }
        }
    }
}