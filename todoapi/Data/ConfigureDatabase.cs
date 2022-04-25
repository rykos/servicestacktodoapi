using System.Data;
using ServiceStack.OrmLite;
using todoapi.ServiceModel.Types;

namespace todoapi.data;

public static class ConfigureDatabase
{
    public static void Configure(IDbConnection db)
    {
        db.CreateTableIfNotExists<TodoTask>();
    }
}