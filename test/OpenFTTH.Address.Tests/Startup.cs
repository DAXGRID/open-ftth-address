using Microsoft.Extensions.DependencyInjection;
using OpenFTTH.CQRS;
using OpenFTTH.EventSourcing;
using OpenFTTH.EventSourcing.InMem;
using System.Reflection;

namespace OpenFTTH.Address.Tests;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IEventStore, InMemEventStore>();
        {
            var businessAssemblies = new Assembly[] {
                AppDomain.CurrentDomain.Load("OpenFTTH.Address.Business"),
            };

            services.AddProjections(businessAssemblies);
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddCQRS(businessAssemblies);
        }
    }
}
