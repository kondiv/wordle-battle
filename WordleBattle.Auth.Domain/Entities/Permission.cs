namespace WordleBattle.Auth.Domain.Entities;

public class Permission
{
    public string Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<RolePermission> Roles { get; set; }
}