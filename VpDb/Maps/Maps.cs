using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VpDb.Entities;

namespace VpDb.Maps
{
    public class GameMaps:ClassMap<Game>
    {
        public GameMaps()
        {
            Table("Game");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.DollarsPerTierCredit);
            HasMany(x => x.Configs).KeyColumn("GameId").Cascade.Delete().Inverse();
        }
    }

    public class ConfigMaps:ClassMap<Config>
    {
        public ConfigMaps()
        {
            Table("Config");
            Id(x => x.Id);
            Map(x => x.DollarsPerCredit);
            Map(x => x.HandsPlayed);
            References(x => x.Game).Column("GameId");
            HasMany(x => x.Results).KeyColumn("ConfigId").Cascade.Delete().Inverse();
        }
    }

    public class ResultMaps:ClassMap<Result>
    {
        public ResultMaps()
        {
            Table("Result");
            Id(x => x.Id);
            Map(x => x.StartCredits);
            Map(x => x.EndCredits);
            Map(x => x.HandsPlayed);
            Map(x => x.CoinIn);
            Map(x => x.TierCreditsEarned);
            References(x => x.Config).Column("ConfigId");
        }
    }
}
