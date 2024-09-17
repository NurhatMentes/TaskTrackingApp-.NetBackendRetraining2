using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IaC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection collection);
    }
}