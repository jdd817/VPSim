using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpDb.Entities
{
    public class Config
    {
        public virtual int Id { get; set; }
        public virtual double DollarsPerCredit { get; set; }
        public virtual int HandsPlayed { get; set; }
        public virtual Game Game { get; set; }
        public virtual IList<Result> Results { get; set; }
    }
}
