namespace Rony.Store.Domain.Entities.Security;
public class GroupRoleRole
{
    public virtual GroupRole GroupRole { get; set; }
    public int GroupRoleId { get; set; }
    public virtual Role Role { get; set; }
    public int RoleId { get; set; }
}
