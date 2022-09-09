namespace N6.Core.Abp.Client.Contracts
{
    public interface IRefitServiceProxyFactory
    {
        TRefitServiceProxy GetRefitServiceProxy<TRefitServiceProxy>() where TRefitServiceProxy : IRefitServiceProxy;
    }
}
