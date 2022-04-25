using System;
using System.Collections.Generic;
using ServiceStack;
using todoapi.ServiceModel.Types;

namespace todoapi.ServiceModel;

public class CreateTodoTask : IReturn<CreateTodoTaskResponse>
{
    [ValidateNotEmpty]
    [ValidateMaximumLength(50)]
    public string Title { get; set; }
    [ValidateNotEmpty]
    [ValidateMaximumLength(500)]
    public string Description { get; set; }
    [ValidateNotEmpty]
    public DateTime ExpirationDate { get; set; }
}

public class CreateTodoTaskResponse
{
    public long Id { get; set; }
}


public class GetAllTodoTasks : IReturn<GetAllTodoTasksResponse> { }

public class GetAllTodoTasksResponse
{
    public IEnumerable<TodoTask> TodoTasks { get; set; }
}


public class GetTodoTask : IReturn<GetTodoTaskResponse>
{
    public long Id { get; set; }
}

public class GetTodoTaskResponse
{
    public TodoTask TodoTask { get; set; }
}


public class GetIncomingTodoTasks : IReturn<GetIncomingTodoTasksResponse>
{
    public TodoTaskTimeFrame TimeFrame { get; set; }
}

public class GetIncomingTodoTasksResponse
{
    public IEnumerable<TodoTask> TodoTasks { get; set; }
}


public class DeleteTodoTask : IReturn<DeleteTodoTaskResponse>
{
    public long Id { get; set; }
}

public class DeleteTodoTaskResponse { }


public class SetTodoTaskPercentage : IReturn<SetTodoTaskPercentageResponse>
{
    public long Id { get; set; }
    public double CompletePercentage { get; set; }
}

public class SetTodoTaskPercentageResponse { }


public class SetTodoTaskAsDone : IReturn<SetTodoTaskAsDoneResponse>
{
    public long Id { get; set; }
}

public class SetTodoTaskAsDoneResponse { }


public class UpdateTodoTask : IReturn<UpdateTodoTaskResponse>
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ExpirationDate { get; set; }
    public double? CompletePercentage { get; set; }
    public bool IsComplete { get; set; }
}

public class UpdateTodoTaskResponse
{
    public TodoTask TodoTask { get; set; } 
}