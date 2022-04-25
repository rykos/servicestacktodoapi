using Xunit;
using todoapi.ServiceModel;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using todoapi.ServiceModel.Types;

namespace todoapi.Tests.TodoTaskServiceTests;

[Collection("TodoTaskServiceTests")]
public class DeleteTodoTaskServiceTests : TestBase
{
    [Fact]
    public async Task DeleteTodoTask()
    {
        long id = service.Db.Insert(TodoTaskHelper.NewTodoTask, selectIdentity: true);

        var response = await service.Delete(new DeleteTodoTask() { Id = id });

        Assert.Null(service.Db.SingleById<TodoTask>(id));
    }
}