using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace N6.Core.Abp.Client.HttpApi
{
    public abstract class BsjcHttpApiClientOptions
    {
        public abstract Type HttpApiClientContractsModuleType { get; set; }
        public abstract string RemoteServiceName { get; set; }
        public string RemoteSectionName
        {
            get
            {
                return $"RemoteServices:{RemoteServiceName}:BaseUrl";
            }
        }

        public Func<DelegatingHandler> DelegatingHandlerFunc { get; set; }

        public Func<PolicyBuilder<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>> HttpErrorPolicy { get; set; }

        public Func<IServiceProvider, HttpRequestMessage, IAsyncPolicy<HttpResponseMessage>> PolicySelector { get; set; }

        public Assembly ContractsAssembly { get; set; }

        public BsjcHttpApiClientOptions()
        {
            ContractsAssembly = this.HttpApiClientContractsModuleType.Assembly;
        }
    }



}
