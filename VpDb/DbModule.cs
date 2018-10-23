using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpDb
{
    public class DbModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<DbFactory>()
                .ToSelf()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", "Data Source=localhost; uid=appuser;pwd=0254E83E3D7A48B38320E9CBFFE44129; Initial Catalog=vp");

            Kernel.Bind<IRepository>().To<Repository>();
        }
    }
}
