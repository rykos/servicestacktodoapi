using System;
using ServiceStack.OrmLite;
using todoapi.ServiceModel.Types;

namespace todoapi.ServiceInterface.Helpers
{
    public static class TodoTaskHelper
    {
        public static SqlExpression<TodoTask> TodoTasksInTimeFrame(SqlExpression<TodoTask> query, TodoTaskTimeFrame timeFrame)
        {
            switch (timeFrame)
            {
                case TodoTaskTimeFrame.Today:
                    return query.Where(y => y.ExpirationDate > DateTime.UtcNow
                                        && y.ExpirationDate <= DateTime.UtcNow.EndOfDay());
                case TodoTaskTimeFrame.Tommarow:
                    return query.Where(y => y.ExpirationDate > DateTime.UtcNow.AddDays(1).StartOfDay()
                                        && y.ExpirationDate <= DateTime.UtcNow.AddDays(1).EndOfDay());
                case TodoTaskTimeFrame.ThisWeek:
                    return query.Where(y => y.ExpirationDate > DateTime.UtcNow
                                        && y.ExpirationDate <= DateTime.UtcNow.AddDays(7).EndOfDay());
                default:
                    return null;
            }
        }

        public static bool TodoTasksInTimeFrame2(TodoTask y, TodoTaskTimeFrame timeFrame)
        {
            switch (timeFrame)
            {
                case TodoTaskTimeFrame.Today:
                    return y.ExpirationDate > DateTime.UtcNow
                                        && y.ExpirationDate <= DateTime.UtcNow.EndOfDay();
                case TodoTaskTimeFrame.Tommarow:
                    return y.ExpirationDate > DateTime.UtcNow.AddDays(1).StartOfDay()
                                        && y.ExpirationDate <= DateTime.UtcNow.AddDays(1).EndOfDay();
                case TodoTaskTimeFrame.ThisWeek:
                    return y.ExpirationDate > DateTime.UtcNow
                                        && y.ExpirationDate <= DateTime.UtcNow.AddDays(7).EndOfDay();
                default:
                    return false;
            }
        }
    }
}