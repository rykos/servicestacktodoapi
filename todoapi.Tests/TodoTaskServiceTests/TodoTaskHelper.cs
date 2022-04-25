using System;
using todoapi.ServiceModel.Types;

namespace todoapi.Tests.TodoTaskServiceTests
{
    public static class TodoTaskHelper
    {
        public static TodoTask NewTodoTask = new TodoTask()
        {
            Title = "Test Task",
            Description = "description",
            ExpirationDate = DateTime.UtcNow.AddDays(1)
        };
    }
}