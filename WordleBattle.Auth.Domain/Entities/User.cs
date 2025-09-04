namespace WordleBattle.Auth.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public string HashPassword { get; set; }
    public string Login { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}