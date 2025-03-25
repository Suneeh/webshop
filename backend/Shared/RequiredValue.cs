using System.ComponentModel.DataAnnotations;

namespace backend.Shared;

public record RequiredValue<T> where T : notnull
{
    [Required] public required T NewValue { get; init; }
}