using System;
using ServiceStack.DataAnnotations;

namespace todoapi.ServiceModel.Types;

public class TodoTask
{
    [PrimaryKey]
    [AutoIncrement]
    public long Id { get; set; }
    [StringLength(50)]
    public string Title { get; set; }
    [StringLength(500)]
    public string Description { get; set; }
    public DateTime ExpirationDate { get; set; }
    [Range(0, 100)]
    public double? CompletePercentage { get; set; }
    public bool IsComplete { get; set; }
}