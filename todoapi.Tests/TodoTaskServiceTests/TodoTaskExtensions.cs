using todoapi.ServiceModel.Types;

namespace todoapi.Tests;

public static class TodoTaskExtensions
{
    public static object StripId(this TodoTask dto)
    {
        return new
        {
            Title = dto.Title,
            Description = dto.Description,
            ExpirationDate = dto.ExpirationDate,
            IsCompleted = dto.IsComplete
        };
    }
}