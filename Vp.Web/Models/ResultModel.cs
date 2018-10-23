using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vp.Web.Models
{
    public class ResultModel
    {
        public int Id { get; set; }
        public double StartCredits { get; set; }
        public double EndCredits { get; set; }
        public int HandsPlayed { get; set; }
        public double CoinIn { get; set; }
        public int TierCreditsEarned { get; set; }
    }
}