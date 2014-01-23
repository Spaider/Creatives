using Creatives.Models;
using Ninject.Modules;

namespace Creatives
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICreativesRepository>().To<CreativesRepository>();
        }
    }
}