using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace N6.Core.Abp.Client.HttpApi
{
    [DependsOn(
        typeof(AbpHttpClientModule)
    )]
    public abstract class AbpHttpApiClientModule : AbpModule
    {

    }
}
