using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Entities
{
    public class PayTable
    {
        public List<PayLine> PayLines { get; set; }
    }

    public class PayLine
    {
        public string Name { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public int Payout { get; set; }
        public decimal MaxBetMultiplier { get; set; }
    }
}
