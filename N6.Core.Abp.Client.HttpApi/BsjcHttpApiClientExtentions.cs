using Microsoft.Extensions.DependencyInjection;
using N6.Core.Abp.Client.Contracts;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace N6.Core.Abp.Client.HttpApi
{
    public static class BsjcHttpApiClientExtentions
    {
        public static void AddAbpHttpClientRefitServiceProxy(this IServiceCollection services, BsjcHttpApiClientOptions httpApiClientOptions)
        {
            var configuration = services.GetConfiguration();

            var hostUrl = configuration[httpApiClientOptions.RemoteSectionName];

            var httpClientBuilder = services.AddHttpClient(httpApiClientOptions.RemoteServiceName, x =>
            {
                x.BaseAddress = new Uri(hostUrl);
            });

            if (httpApiClientOptions.DelegatingHandlerFunc != null)
            {
                httpClientBuilder.ConfigurePrimaryHttpMessageHandler(httpApiClientOptions.DelegatingHandlerFunc);
            }

            if (httpApiClientOptions.HttpErrorPolicy != null)
            {
                httpClientBuilder.AddTransientHttpErrorPolicy(httpApiClientOptions.HttpErrorPolicy);
            }

            if (httpApiClientOptions.PolicySelector != null)
            {
                httpClientBuilder.AddPolicyHandler(httpApiClientOptions.PolicySelector);
            }

            var allRefitServiceProxyTypes = new List<Type>(httpApiClientOptions.ContractsAssembly.GetTypes());

            allRefitServiceProxyTypes = allRefitServiceProxyTypes.FindAll(t => t.IsInterface && typeof(IRefitServiceProxy).IsAssignableFrom(t));

            foreach (var type in allRefitServiceProxyTypes)
            {
                services.AddRefitClient(type, httpApiClientOptions.RemoteServiceName);
            }
        }

        // TODO:Add request header Authorization 
        public static IServiceCollection AddRefitClient(this IServiceCollection services, Type refitInterfaceType, string httpclientName, RefitSettings settings = null)
        {
            var builder = RequestBuilder.ForType(refitInterfaceType, settings);
            services.AddSingleton(provider => builder);
            services.AddSingleton(refitInterfaceType, provider =>
            {
                var client = provider.GetService<IHttpClientFactory>()?.CreateClient(httpclientName);

                if (client == null)
                    throw new ArgumentException($"please inject the httpclient named {httpclientName} httpclient!! ", httpclientName);

                return RestService.For(refitInterfaceType, client, builder);
            });
            return services;
        }

        public static IServiceCollection AddRefitClient(this IServiceCollection services, Type refitInterfaceType, HttpClient client, RefitSettings settings = null)
        {
            var builder = RequestBuilder.ForType(refitInterfaceType, settings);
            services.AddSingleton(provider => builder);
            services.AddSingleton(refitInterfaceType, provider =>
            {
                return RestService.For(refitInterfaceType, client, builder);
            });
            return services;
        }
    }
}
