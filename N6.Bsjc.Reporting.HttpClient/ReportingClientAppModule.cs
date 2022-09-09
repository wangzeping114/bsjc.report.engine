using Microsoft.Extensions.DependencyInjection;
using N6.Core.Abp.Client.HttpApi;
using Polly;
using System;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace N6.Bsjc.Reporting.HttpApi.Client
{
    [DependsOn(
     typeof(AbpHttpClientModule)
    )]
    public class ReportingClientAppModule: AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpHttpClientBuilderOptions>(options =>
            {
                options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
                {
                    clientBuilder.AddTransientHttpErrorPolicy(policyBuilder =>
                        policyBuilder.WaitAndRetryAsync(
                            3,
                            i => TimeSpan.FromSeconds(Math.Pow(2, i))
                        )
                    );
                });
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpHttpClientRefitServiceProxy(new ReportHttpApiClientOptions());
        }
    }
}
