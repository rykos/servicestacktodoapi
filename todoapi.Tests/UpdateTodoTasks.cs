using System;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.OrmLite;
using todoapi.ServiceModel;
using todoapi.ServiceModel.Types;
using Xunit;

namespace todoapi.Tests.TodoTaskServiceTests;

[Collection("TodoTaskServiceTests")]
public class UpdateTodoTasksTests : TestBase
{
    [Fact]
    public async Task UpdateTodoTask()
    {
        long id = CreateTestTask();

        var updateTask = new UpdateTodoTask()
        {
            Id = id,
            Title = "Updated Task",
            Description = "Updated description",
            CompletePercentage = 50,
            ExpirationDate = DateTime.UtcNow.AddDays(7)
        };

        TodoTask response = (await service.Any(updateTask)).TodoTask;

        Assert.Equal(updateTask.ConvertTo<TodoTask>().StripId(), response.StripId());
    }

    [Fact]
    public async Task SetTodoTaskPercentageAndSetComplete()
    {
        long id = CreateTestTask();

        var setTodoTaskPercentage = new SetTodoTaskPercentage()
        {
            Id = id,
            CompletePercentage = 100
        };

        await service.Any(setTodoTaskPercentage);
        var updatedTask = service.Db.SingleById<TodoTask>(id);

        Assert.Equal(setTodoTaskPercentage.CompletePercentage, updatedTask.CompletePercentage);
        Assert.Equal(true, updatedTask.IsComplete);
    }

    [Fact]
    public async Task SetTodoTaskPercentage()
    {
        long id = CreateTestTask();

        var SetTodoTaskAsDone = new SetTodoTaskAsDone()
        {
            Id = id
        };

        await service.Any(SetTodoTaskAsDone);
        var updatedTask = service.Db.SingleById<TodoTask>(id);

        Assert.Equal(true, updatedTask.IsComplete);
    }

    [Fact]
    public void SetTodoTaskComplete()
    {
        long id = CreateTestTask();

        var setTodoTaskPercentage = new SetTodoTaskPercentage()
        {
            Id = id,
            CompletePercentage = 99
        };

        var response = (service.Any(setTodoTaskPercentage));
        var updatedTask = service.Db.SingleById<TodoTask>(id);

        Assert.Equal(setTodoTaskPercentage.CompletePercentage, updatedTask.CompletePercentage);
        Assert.Equal(false, updatedTask.IsComplete);
    }

    private long CreateTestTask()
    {
        var newTask = TodoTaskHelper.NewTodoTask;
        return service.Db.Insert(newTask, selectIdentity: true);
    }
}