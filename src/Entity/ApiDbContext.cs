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

      //Relations 

      entity.HasOne(e => e.LocationIdNavigation)
        .WithMany()
        .HasForeignKey(e => e.LocationId);
      
      
    });

    modelBuilder.Entity<StationEntity>(entity =>
    {
      entity.ToTable("station");
      entity.HasKey(e => e.StationId);

      //Columns

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

      //Relations

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
      
      //Relations

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


            entity.Property(e => e.UbicationId)
                .HasColumnName("ubication_id")
                .HasColumnType("uuid");

            entity.Property(e => e.UbicationId)
                .HasColumnName("ubication_id")
                .HasColumnType("uuid");

            entity.Property(e => e.StationType)
                .HasColumnName("station_type")
                .HasColumnType("varchar(20)");

          // Configuración para estaciones de carga
          entity.Property(e => e.ChargingStationId)
              .HasColumnName("charging_station_id")
              .HasColumnType("varchar(50)");

            
            entity.Property(e => e.BicingStationId)
                .HasColumnName("bicing_station_id")
                .HasColumnType("integer");

            // Configuración para BUS
          entity.Property(e => e.BusStopId)
              .HasColumnName("bus_stop_id")
              .HasColumnType("integer");

            entity.Property(e => e.BusStreetName)
                .HasColumnName("bus_street_name")
                .HasColumnType("varchar(200)");

            entity.Property(e => e.BusDistrictName)
                .HasColumnName("bus_district_name")
                .HasColumnType("varchar(100)");

            entity.Property(e => e.MetroStationId)
                .HasColumnName("metro_station_id")
                .HasColumnType("integer");

            entity.Property(e => e.MetroLineId)
                .HasColumnName("metro_line_id")
                .HasColumnType("integer");

            entity.Property(e => e.MetroLineName)
                .HasColumnName("metro_line_name")
                .HasColumnType("varchar(50)");

            entity.Property(e => e.MetroLineColor)
                .HasColumnName("metro_line_color")
                .HasColumnType("varchar(20)");

            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId);

            entity.HasOne(e => e.ChargingStation)
                .WithMany()
                .HasForeignKey(e => e.ChargingStationId);

            entity.HasOne(e => e.BicingStation)
                .WithMany()
                .HasForeignKey(e => e.BicingStationId);
        });
      modelBuilder.Entity<RouteEntity>(entity=>
        {
          entity.ToTable("route");
          entity.HasKey(e => e.Id);


          entity.Property(e => e.Id)
              .HasColumnName("route_id")
              .HasColumnType("uuid");

          entity.Property(e => e.OriginLat)
              .HasColumnName("originlat")
              .HasColumnType("double precision");

          entity.Property(e => e.OriginLng)
              .HasColumnName("originlng")
              .HasColumnType("double precision");

          entity.Property(e => e.DestinationLat)
              .HasColumnName("destinationlat")
              .HasColumnType("double precision");

          entity.Property(e => e.DestinationLng)
              .HasColumnName("destinationlng")
              .HasColumnType("double precision");

          entity.Property(e => e.Mean)
              .HasColumnName("mean")
              .HasColumnType("varchar(50)");

          entity.Property(e => e.Preference)
              .HasColumnName("preference")
              .HasColumnType("varchar(50)");

          entity.Property(e => e.Distance)
              .HasColumnName("distance")
              .HasColumnType("float");

          entity.Property(e => e.Duration)
              .HasColumnName("duration")
              .HasColumnType("float");

          entity.Property(e => e.GeometryJson)
              .HasColumnName("geometryjson")
              .HasColumnType("jsonb");

          entity.Property(e => e.InstructionsJson)
              .HasColumnName("instructionsjson")
              .HasColumnType("jsonb");

        });

      modelBuilder.Entity<RouteUserEntity>(entity =>
        {
          entity.ToTable("userRoutes");
          entity.HasKey(e => new {e.UsuarioId, e.RutaId});

          entity.Property(e => e.UsuarioId)
              .HasColumnName("UserId")
              .HasColumnType("uuid");

          entity.Property(e => e.RutaId)
              .HasColumnName("RouteId")
              .HasColumnType("uuid");

          entity.HasOne(e => e.Ruta)
              .WithMany()
              .HasForeignKey(e => e.RutaId);

          entity.HasOne(e => e.Usuario) 
              .WithMany()
              .HasForeignKey(e => e.UsuarioId);
        
        });
    });
  }
}
