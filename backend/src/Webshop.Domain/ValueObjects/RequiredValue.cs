using System.ComponentModel.DataAnnotations;

namespace Webshop.Domain.ValueObjects;

public record RequiredValue<T> where T : notnull
{
    [Required] public required T NewValue { get; init; }
}