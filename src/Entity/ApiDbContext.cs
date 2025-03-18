using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entity;

public class ApiDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApiDbContext(DbContextOptions<ApiDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString, options =>
            {
                options.UseNetTopologySuite();
                options.EnableRetryOnFailure(
                  maxRetryCount: 5,
                  maxRetryDelay: TimeSpan.FromSeconds(5),
                  errorCodesToAdd: new List<string>());
            });
        }
    }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresExtension("postgis");

    modelBuilder.Entity<UserEntity>(entity =>
    {
      //Table
      entity.ToTable("users");
      //Primary Key
      entity.HasKey(e => e.UserId);
      //Columns
      entity.Property(e => e.UserId)
        .HasColumnName("id_user")
        .HasColumnType("uuid");

      entity.Property(e => e.Name)
        .HasColumnName("name")
        .HasColumnType("text");

      entity.Property(e => e.Email)
        .HasColumnName("email")
        .HasColumnType("text");

      entity.Property(e => e.PasswordHash)
        .HasColumnName("password_hash")
        .HasColumnType("text");

      entity.Property(e => e.Idioma)
        .HasColumnName("idioma")
        .HasColumnType("text");
    });
  }
}
