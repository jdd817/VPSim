using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpDb.Entities
{
    public class Result
    {
        public virtual int Id { get; set; }
        public virtual double StartCredits { get; set; }
        public virtual double EndCredits { get; set; }
        public virtual int HandsPlayed { get; set; }
        public virtual double CoinIn { get; set; }
        public virtual int TierCreditsEarned { get; set; }

        public virtual Config Config { get; set; }
    }
}
