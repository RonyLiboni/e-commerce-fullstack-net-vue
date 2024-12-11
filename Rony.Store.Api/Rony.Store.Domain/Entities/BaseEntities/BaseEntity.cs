using System.ComponentModel.DataAnnotations;

namespace Rony.Store.Domain.Entities.BaseEntities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

}