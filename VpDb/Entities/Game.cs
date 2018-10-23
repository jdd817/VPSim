using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpDb.Entities
{
    public class Game
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int DollarsPerTierCredit { get; set; }
        public virtual IList<Config> Configs { get; set; }
    }
}
