namespace WordleBattle.Auth.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<RolePermission> Permissions { get; set; }
    public virtual ICollection<User> Users { get; set; }
}