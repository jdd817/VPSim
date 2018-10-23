using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VpDb.Entities;
using Vp.Web.Models;


[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Vp.Web.App_Start.AutomapperConfig), "ConfigMaps")]
namespace Vp.Web.App_Start
{
    public class AutomapperConfig
    {
        public static void ConfigMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Game, GameModel>();
                cfg.CreateMap<Config, ConfigModel>();
                cfg.CreateMap<Result, ResultModel>();
            });
        }
    }
}