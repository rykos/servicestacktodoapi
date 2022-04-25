using System;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using todoapi.data;
using todoapi.ServiceInterface;

namespace todoapi.Tests;

public abstract class TestBase : IDisposable
{
    protected ServiceStackHost appHost;
    protected TodoTasksServices service;

    public TestBase()
    {
        appHost = new AppHost().Init();
        RegisterDatabase();
        service = appHost.Container.Resolve<TodoTasksServices>();
    }

    private void RegisterDatabase()
    {
        appHost.Container.Register<IDbConnectionFactory>(
                    new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));

        using (var db = appHost.GetDbConnection())
        {
            ConfigureDatabase.Configure(db);
        }
    }

    public void Dispose()
    {
        appHost.Dispose();
    }
}