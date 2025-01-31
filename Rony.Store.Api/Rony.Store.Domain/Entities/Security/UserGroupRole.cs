namespace Rony.Store.Domain.Entities.Security;
public class UserGroupRole
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int GroupRoleId { get; set; }
    public GroupRole GroupRole { get; set; }
}