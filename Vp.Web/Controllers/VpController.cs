using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vp.Web.Models;
using VpDb;
using VpDb.Entities;
using AutoMapper.QueryableExtensions;

namespace Vp.Web.Controllers
{
    public class VpController : ApiController
    {
        private IRepository _repo;

        public VpController(IRepository repo)
        {
            _repo = repo;
        }

        [Route("Vp/Games")]
        [HttpGet]
        public IEnumerable<GameModel> Games()
        {
            return _repo.Get<Game>()
                .ProjectTo<GameModel>();
        }

        [Route("Vp/Games/{GameId}/Configs")]
        [HttpGet]
        public IEnumerable<ConfigModel> Configs(int GameId)
        {
            return _repo.Get<Config>()
                .Where(c => c.Game.Id == GameId)
                .ProjectTo<ConfigModel>();
        }

        [Route("Vp/Games/{GameId}/Configs/{ConfigId}/Results")]
        [Route("Vp/Configs/{ConfigId}/Results")]
        [HttpGet]
        public IEnumerable<ResultModel> Results(int ConfigId)
        {
            return _repo.Get<Result>()
                .Where(r => r.Config.Id == ConfigId)
                .ProjectTo<ResultModel>();
        }

        [Route("Vp/Games/{GameId}/Configs/{ConfigId}/ResultsSummary")]
        [Route("Vp/Configs/{ConfigId}/ResultsSummary")]
        [Route("Vp/Configs/{ConfigId}/ResultsSummary/Limit/{Limit}")]
        [Route("Vp/Configs/{ConfigId}/ResultsSummary/Grouping/{Grouping}")]
        [Route("Vp/Configs/{ConfigId}/ResultsSummary/Grouping/{Grouping}/Limit/{Limit}")]
        [Route("Vp/Configs/{ConfigId}/ResultsSummary/Limit/{Limit}/Grouping/{Grouping}")]
        [HttpGet]
        public object ResultsSummary(int ConfigId, int? Limit=null,  int? Grouping=null)
        {
            double total = _repo.Get<Result>()
                .Where(r => r.Config.Id == ConfigId)
                .Count();

            var rawNumbers = _repo.Get<Result>()
                .Where(r => r.Config.Id == ConfigId)
                .GroupBy(r => r.EndCredits)
                .Select(rg => new
                {
                    EndCredits = rg.Key,
                    Count = rg.Count(),
                    Percent = rg.Count() / total
                })
                .OrderBy(x => x.EndCredits)
                .ToList();

            if (Limit != null)
                rawNumbers = rawNumbers.Where(rn => rn.EndCredits <= Limit).ToList();

            var cohortGrouping = Grouping ?? 500;

            var cohorts = rawNumbers.
                Select(x =>
                new
                {
                    Cohort = ((int)(x.EndCredits + cohortGrouping - 1) / cohortGrouping) * cohortGrouping,
                    Count = x.Count
                })
                .GroupBy(x => x.Cohort)
                .Select(x => new
                {
                    Tier=x.Key,
                    Count=x.Sum(y=>y.Count),
                    Percent= x.Sum(y => y.Count)/total
                })
                .OrderBy(x=>x.Tier)
                .ToList();

            return new
            {
                rawNumbers = rawNumbers,
                breakdown = cohorts
            };
        }
    }
}
