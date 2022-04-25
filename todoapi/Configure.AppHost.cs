using Funq;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Validation;
using todoapi.data;
using todoapi.ServiceInterface;

[assembly: HostingStartup(typeof(todoapi.AppHost))]

namespace todoapi;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services =>
        {
            // Configure ASP.NET Core IOC Dependencies
        });

    public AppHost() : base("todoapi",
        typeof(TodoTasksServices).Assembly
    )
    { }

    public override void Configure(Container container)
    {
        IConfiguration configuration = container.GetService<IConfiguration>();
        if (configuration != default)
        {
            container.Register<IDbConnectionFactory>(c =>
                new OrmLiteConnectionFactory(configuration.GetConnectionString("Default"), PostgreSqlDialect.Provider));

            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                ConfigureDatabase.Configure(db);
            }
        }

        Plugins.Add(new ValidationFeature());

        // Configure ServiceStack only IOC, Config & Plugins
        SetConfig(new HostConfig
        {
            UseSameSiteCookies = true,
        });
    }
}
