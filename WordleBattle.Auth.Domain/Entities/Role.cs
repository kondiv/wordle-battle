namespace WordleBattle.Auth.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}