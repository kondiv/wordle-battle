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
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

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
                .HasColumnName("is_email_confirmed");
            
            entity.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("login");
            
            entity.Property(e => e.HashPassword)
                .IsRequired()
                .HasMaxLength(511)
                .HasColumnName("hash_password");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("role");
            
            entity.HasKey(e => e.Id);
            
            entity.HasIndex(e => e.Name).IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("permission");

            entity.HasKey(e => e.Id);
            
            entity.HasIndex(e => e.Name).IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("name");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.ToTable("role_permission");

            entity.HasKey(e => new { e.RoleId, e.PermissionId });
            
            entity.HasIndex(e => new { e.RoleId, e.PermissionId }).IsUnique();
            
            entity.Property(e => e.RoleId)
                .IsRequired()
                .HasColumnName("role_id");
            
            entity.Property(e => e.PermissionId)
                .IsRequired()
                .HasColumnName("permission_id");

            entity.HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            entity.HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("user_role");
            
            entity.HasKey(e => new { e.UserId, e.RoleId });
            
            entity.HasIndex(e => new { e.UserId, e.RoleId }).IsUnique();
            
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasColumnName("user_id");
            
            entity.Property(e => e.RoleId)
                .IsRequired()
                .HasColumnName("role_id");

            entity.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        });
    }
}