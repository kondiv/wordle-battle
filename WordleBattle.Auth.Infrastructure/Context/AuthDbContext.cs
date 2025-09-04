using Microsoft.EntityFrameworkCore;
using WordleBattle.Auth.Domain.Entities;

namespace WordleBattle.Auth.Infrastructure.Context;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Login).IsUnique();
            
            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()");
            
            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("email");
            
            entity.Property(e => e.IsEmailConfirmed)
                .HasDefaultValueSql("0")
                .HasColumnName("is_email_confirmed");
            
            entity.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("login");
            
            entity.Property(e => e.HashPassword)
                .IsRequired()
                .HasMaxLength(511)
                .HasColumnName("hash_password");

            entity.HasMany(u => u.Roles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);
        });
    }
}