using Rony.Store.Domain.Entities.BaseEntities;

namespace Rony.Store.Domain.Entities.Security;
public class Role : BaseEntityWithName
{
    public string Description { get; set; }
}
