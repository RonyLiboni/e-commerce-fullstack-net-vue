using Rony.Store.Domain.Entities.BaseEntities;

namespace Rony.Store.Domain.Entities.Security;
public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<GroupRole> GroupRoles { get; set; }
}
