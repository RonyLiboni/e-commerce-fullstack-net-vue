using System.ComponentModel.DataAnnotations;

namespace Rony.Store.Domain.Entities.BaseCommons;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

}