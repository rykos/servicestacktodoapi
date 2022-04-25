using ServiceStack;
using todoapi.ServiceModel;
using ServiceStack.OrmLite;
using todoapi.ServiceModel.Types;
using System.Threading.Tasks;
using System.Collections.Generic;
using todoapi.ServiceInterface.Helpers;

namespace todoapi.ServiceInterface;

public class TodoTasksServices : Service
{
    public async Task<CreateTodoTaskResponse> Post(CreateTodoTask request)
    {
        var newTodoTask = new TodoTask()
        {
            Title = request.Title,
            Description = request.Description,
            ExpirationDate = request.ExpirationDate.ToUniversalTime()
        };
        var id = await Db.InsertAsync(newTodoTask, selectIdentity: true);

        return new CreateTodoTaskResponse() { Id = id };
    }

    public async Task<GetAllTodoTasksResponse> Get(GetAllTodoTasks request)
    {
        var todoTasks = await Db.SelectAsync<TodoTask>();

        return new GetAllTodoTasksResponse() { TodoTasks = todoTasks };
    }

    public async Task<GetTodoTaskResponse> Get(GetTodoTask request)
    {
        return new GetTodoTaskResponse()
        {
            TodoTask = await Db.SingleByIdAsync<TodoTask>(request.Id)
        };
    }

    public async Task<GetIncomingTodoTasksResponse> Get(GetIncomingTodoTasks request)
    {
        SqlExpression<TodoTask> query = null;

        query = Db.From<TodoTask>(task => TodoTaskHelper.TodoTasksInTimeFrame(task, request.TimeFrame).And(q => !q.IsComplete));

        List<TodoTask> tasks = await Db.SelectAsync<TodoTask>(query);

        return new GetIncomingTodoTasksResponse()
        {
            TodoTasks = tasks
        };
    }

    public async Task<UpdateTodoTaskResponse> Any(UpdateTodoTask request)
    {
        await Db.UpdateAsync<TodoTask>(
            new
            {
                request.Title,
                request.Description,
                request.ExpirationDate,
                request.CompletePercentage,
                request.IsComplete
            },
            x => x.Id == request.Id
        );

        return new UpdateTodoTaskResponse()
        {
            TodoTask = await Db.SingleByIdAsync<TodoTask>(request.Id)
        };
    }

    public async Task<SetTodoTaskPercentageResponse> Any(SetTodoTaskPercentage request)
    {
        var todoTask = await Db.SingleByIdAsync<TodoTask>(request.Id);

        todoTask.CompletePercentage = request.CompletePercentage;
        if (request.CompletePercentage >= 100)
        {
            todoTask.CompletePercentage = 100;
            todoTask.IsComplete = true;
        }

        return (await Db.UpdateAsync(todoTask) > 0)
        ? new SetTodoTaskPercentageResponse()
        : throw new HttpError(500, "Internal Server Error");
    }

    public async Task<SetTodoTaskAsDoneResponse> Any(SetTodoTaskAsDone request)
    {
        var todoTask = Db.SingleById<TodoTask>(request.Id);
        todoTask.IsComplete = true;
        await Db.UpdateAsync(todoTask);

        return new SetTodoTaskAsDoneResponse();
    }

    public async Task<DeleteTodoTaskResponse> Delete(DeleteTodoTask request)
    {
        return (await Db.DeleteByIdAsync<TodoTask>(request.Id)) > 0
        ? new DeleteTodoTaskResponse()
        : throw new HttpError(404, "Not Found");
    }
}