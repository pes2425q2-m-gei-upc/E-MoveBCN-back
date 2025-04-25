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
    public DbSet<LocationEntity> Locations { get; set; }
    public DbSet<HostEntity> Hosts { get; set; }
    public DbSet<StationEntity> Stations { get; set; }
    public DbSet<PortEntity> Ports { get; set; }
    public DbSet<BicingStationEntity> BicingStations { get; set; }
    public DbSet<StateBicingEntity> StateBicing { get; set; }
    public DbSet<SavedUbicationEntity> SavedUbications { get; set; }
    public DbSet<RouteEntity> Routes {get; set;}
    public DbSet<RouteUserEntity> RoutesUser { get; set; }



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
      entity.ToTable("users");
      entity.HasKey(e => e.UserId);
      
      entity.Property(e => e.UserId)
        .HasColumnName("id_user")
        .HasColumnType("uuid");

      entity.Property(e => e.Name)
        .HasColumnName("name")
        .HasColumnType("text");

      entity.HasAlternateKey(e => e.Name);

      entity.Property(e => e.Email)
        .HasColumnName("email")
        .HasColumnType("text");

      entity.Property(e => e.PasswordHash)
        .HasColumnName("password_hash")
        .HasColumnType("text");
    });

    modelBuilder.Entity<LocationEntity>(entity =>
    {
      entity.ToTable("location");
      entity.HasKey(e => e.LocationId);

      entity.Property(e => e.LocationId)
        .HasColumnName("location_id")
        .HasColumnType("text");

      entity.Property(e => e.NetworkBrandName)
        .HasColumnName("network_brand_name")
        .HasColumnType("text");

      entity.Property(e => e.OperatorPhone)
        .HasColumnName("operator_phone")
        .HasColumnType("text");

      entity.Property(e => e.OperatorWebsite)
        .HasColumnName("operator_website")
        .HasColumnType("text");

      entity.Property(e => e.Latitude)
        .HasColumnName("latitude")
        .HasColumnType("real");

      entity.Property(e => e.Longitude)
        .HasColumnName("longitude")
        .HasColumnType("real");

      entity.Property(e => e.AddressString)
        .HasColumnName("address_string")
        .HasColumnType("text");

      entity.Property(e => e.Locality)
        .HasColumnName("locality")
        .HasColumnType("text");

      entity.Property(e => e.PostalCode)
        .HasColumnName("postal_code")
        .HasColumnType("text");
        
    });

    modelBuilder.Entity<HostEntity>(entity =>
    {
      entity.ToTable("host");
      entity.HasKey(e => e.HostId);

      entity.Property(e => e.HostId)
        .HasColumnName("host_id")
        .HasColumnType("uuid");

      entity.Property(e => e.HostName)
        .HasColumnName("host_name")
        .HasColumnType("text");

      entity.Property(e => e.HostAddress)
        .HasColumnName("host_address")
        .HasColumnType("text");

      entity.Property(e => e.HostLocality)
        .HasColumnName("host_locality")
        .HasColumnType("text");

      entity.Property(e => e.HostPostalCode)
        .HasColumnName("host_postal_code")
        .HasColumnType("text");

      entity.Property(e => e.OperatorPhone)
        .HasColumnName("operator_phone")
        .HasColumnType("text");

      entity.Property(e => e.OperatorWebsite)
        .HasColumnName("operator_website")
        .HasColumnType("text");

      entity.Property(e => e.LocationId)
        .HasColumnName("location_id")
        .HasColumnType("text");

      entity.HasOne(e => e.LocationIdNavigation)
        .WithMany()
        .HasForeignKey(e => e.LocationId);
      
    });

    modelBuilder.Entity<StationEntity>(entity =>
    {
      entity.ToTable("station");
      entity.HasKey(e => e.StationId);

      entity.Property(e => e.StationId)
        .HasColumnName("station_id")
        .HasColumnType("text");

      entity.Property(e => e.StationLabel)
        .HasColumnName("station_label")
        .HasColumnType("text");

      entity.Property(e => e.StationLatitude)
        .HasColumnName("station_latitude")
        .HasColumnType("float");

      entity.Property(e => e.StationLongitude)
        .HasColumnName("station_longitude")
        .HasColumnType("float");

      entity.Property(e => e.LocationId)
        .HasColumnName("location_id")
        .HasColumnType("text");

      entity.HasOne(e => e.LocationIdNavigation)
        .WithMany()
        .HasForeignKey(e => e.LocationId);
      
    });

    modelBuilder.Entity<PortEntity>(entity =>
    {
      entity.ToTable("port");
      entity.HasKey(e => new { e.StationId, e.PortId });

      entity.Property(e => e.PortId)
        .HasColumnName("port_id")
        .HasColumnType("text");
        
      entity.Property(e => e.ConnectorType)
        .HasColumnName("connector_type")
        .HasColumnType("text");

      entity.Property(e => e.PowerKw)
        .HasColumnName("power_kw")
        .HasColumnType("float");

      entity.Property(e => e.ChargingMechanism)
        .HasColumnName("charging_mechanism")
        .HasColumnType("text");

      entity.Property(e => e.Status)
        .HasColumnName("status")
        .HasColumnType("text");

      entity.Property(e => e.Reservable)
        .HasColumnName("reservable")
        .HasColumnType("boolean");

      entity.Property(e => e.LastUpdated)
        .HasColumnName("last_updated")
        .HasColumnType("timestamp");

      entity.Property(e => e.StationId)
        .HasColumnName("station_id")
        .HasColumnType("text");

      entity.HasOne(e => e.StationIdNavigation)
        .WithMany()
        .HasForeignKey(e => e.StationId);

    });

    modelBuilder.Entity<BicingStationEntity>(entity =>
    {
      entity.ToTable("bicing_station");
      entity.HasKey(e => e.BicingId);

      entity.Property(e => e.BicingId)
        .HasColumnName("bicing_id")
        .HasColumnType("integer");

      entity.Property(e => e.BicingName)
        .HasColumnName("bicing_name")
        .HasColumnType("char(200)");

      entity.Property(e => e.Latitude)
        .HasColumnName("latitude")
        .HasColumnType("real");

      entity.Property(e => e.Longitude)
        .HasColumnName("longitude")
        .HasColumnType("real");

      entity.Property(e => e.Altitude)
        .HasColumnName("altitude")
        .HasColumnType("real");

      entity.Property(e => e.Address)
        .HasColumnName("address")
        .HasColumnType("char(200)");

      entity.Property(e => e.CrossStreet)
        .HasColumnName("cross_street")
        .HasColumnType("char(200)");

      entity.Property(e => e.PostCode)
        .HasColumnName("post_code")
        .HasColumnType("char(10)");

      entity.Property(e => e.Capacity)
        .HasColumnName("capacity")
        .HasColumnType("integer");

      entity.Property(e => e.IsChargingStation)
        .HasColumnName("is_charging_station")
        .HasColumnType("boolean");

    });

    modelBuilder.Entity<StateBicingEntity>(entity =>
    {
      entity.ToTable("state_bicing");
      entity.HasKey(e => e.BicingId);

      entity.Property(e => e.BicingId)
        .HasColumnName("bicing_id")
        .HasColumnType("integer");

      entity.Property(e => e.NumBikesAvailable)
        .HasColumnName("num_bikes_available")
        .HasColumnType("integer");

      entity.Property(e => e.NumBikesAvailableMechanical)
        .HasColumnName("num_bikes_available_mechanical")
        .HasColumnType("integer");

      entity.Property(e => e.NumBikesAvailableEbike)
        .HasColumnName("num_bikes_available_ebike")
        .HasColumnType("integer");

      entity.Property(e => e.NumDocksAvailable)
        .HasColumnName("num_docks_available")
        .HasColumnType("integer");

      entity.Property(e => e.LastReported)
        .HasColumnName("last_reported")
        .HasColumnType("date");

      entity.Property(e => e.Status)
        .HasColumnName("status")
        .HasColumnType("char(50)");
    
      entity.HasOne(e => e.BicingStationIdNavigation)
        .WithMany()
        .HasForeignKey(e => e.BicingId);
    });

      modelBuilder.Entity<SavedUbicationEntity>(entity =>
        {
            entity.ToTable("saved_ubications");
            //Primary Key
            entity.HasKey(e => new {e.UbicationId, e.Username, e.StationType});

            //Columns
            entity.Property(e => e.UbicationId)
                .HasColumnName("ubication_id")
                .HasColumnType("integer");

            entity.Property(e => e.Username)
                .HasColumnName("username")
                .HasColumnType("text");

            entity.Property(e => e.StationType)
                .HasColumnName("station_type")
                .HasColumnType("text");

            entity.Property(e => e.Latitude)
                .HasColumnName("latitude")
                .HasColumnType("real");

            entity.Property(e => e.Longitude)
              .HasColumnName("longitude")
              .HasColumnType("real");
        });
    });
  }
}

