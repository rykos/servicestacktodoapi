using Xunit;
using todoapi.ServiceModel;
using System.Threading.Tasks;
using System;

namespace todoapi.Tests.TodoTaskServiceTests;

[Collection("TodoTaskServiceTests")]
public class CreateTodoTaskServiceTests : TestBase
{
    [Fact]
    public async Task CreateTodoTask()
    {        
        CreateTodoTaskResponse response = await service.Post(new CreateTodoTask()
        {
            Title = "Test Task",
            Description = "description",
            ExpirationDate = DateTime.UtcNow.AddDays(1)
        });

        Assert.NotEqual(response.Id, 0);
    }
}