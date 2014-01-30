
using Ninject.Modules;

namespace Creatives.Repository
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICreativesRepository>().To<CreativesRepository>();
        }
    }
}