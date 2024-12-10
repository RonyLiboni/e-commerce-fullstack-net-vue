using System.ComponentModel.DataAnnotations;

namespace Rony.Store.Domain.Entities.BaseCommons;

public abstract class BaseEntityWithName : BaseEntity
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public override bool Equals(object? obj)
    {
        return obj is BaseEntityWithName name &&
               Name == name.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }
}
