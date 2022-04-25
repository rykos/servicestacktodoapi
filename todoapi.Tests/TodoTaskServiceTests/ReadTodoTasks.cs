using Xunit;
using todoapi.ServiceModel;
using System.Threading.Tasks;
using System.Linq;
using ServiceStack.OrmLite;
using todoapi.ServiceModel.Types;
using System;
using todoapi.ServiceInterface.Helpers;

namespace todoapi.Tests.TodoTaskServiceTests;

[Collection("TodoTaskServiceTests")]
public class GetAllTests : TestBase
{
    [Fact]
    public async Task GetAllTodoTasks()
    {
        GetAllTodoTasksResponse response = await service.Get(new GetAllTodoTasks());

        Assert.Equal(response.TodoTasks.Count(), 0);
    }

    [Fact]
    public async Task GetTodoTaskById()
    {
        TodoTask newTodoTask = new TodoTask()
        {
            Title = "Test Task",
            Description = "description",
            ExpirationDate = DateTime.UtcNow.AddDays(1)
        };

        long id = service.Db.Insert(newTodoTask, selectIdentity: true);

        GetTodoTaskResponse response = await service.Get(new GetTodoTask() { Id = id });

        Assert.Equal(response.TodoTask.StripId(), newTodoTask.StripId());
    }

    [Fact]
    public async void GetIncomingTodoTasks()
    {
        TodoTask[] newTodoTask = new TodoTask[]{
            new TodoTask()
            {
                Title = "Test Task 1",
                Description = "description",
                ExpirationDate = DateTime.Today.EndOfDay()
            },
            new TodoTask()
            {
                Title = "Test Task 2",
                Description = "description",
                ExpirationDate = DateTime.Today.EndOfDay().AddDays(1)
            },
            new TodoTask()
            {
                Title = "Test Task 3",
                Description = "description",
                ExpirationDate = DateTime.Today.EndOfDay().AddDays(5)
            },
            new TodoTask()
            {
                Title = "Test Task 3",
                Description = "description",
                ExpirationDate = DateTime.Today.EndOfDay().AddDays(8)
            }
        };

        service.Db.InsertAll<TodoTask>(newTodoTask);

        GetIncomingTodoTasksResponse responseToday = await service.Get(new GetIncomingTodoTasks() { TimeFrame = TodoTaskTimeFrame.Today });
        GetIncomingTodoTasksResponse responseTommorow = await service.Get(new GetIncomingTodoTasks() { TimeFrame = TodoTaskTimeFrame.Tommarow });
        GetIncomingTodoTasksResponse responseWeek = await service.Get(new GetIncomingTodoTasks() { TimeFrame = TodoTaskTimeFrame.ThisWeek });

        Assert.Equal(1, responseToday.TodoTasks.Count());
        Assert.Equal(1, responseTommorow.TodoTasks.Count());
        Assert.Equal(newTodoTask.Count() - 1, responseWeek.TodoTasks.Count());
    }
}