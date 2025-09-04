namespace WordleBattle.Auth.Domain.Entities;

public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<RolePermission> RolePermissions { get; set; }
}