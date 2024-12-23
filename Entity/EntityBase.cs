using System;
using System.ComponentModel.DataAnnotations;

namespace grpcService.Entity;

public class EntityBase
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
}
