using System;
using Entity.Bicing;
using Entity.Chat;
using Entity.Route;
using Entity.Ubication;
using Entity.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entity;

public class ApiDbContext(DbContextOptions<ApiDbContext> options, IConfiguration configuration) : DbContext(options)
{
  private readonly IConfiguration _configuration = configuration;

  public DbSet<UserEntity> Users { get; set; }
  public DbSet<LocationEntity> Locations { get; set; }
  public DbSet<HostEntity> Hosts { get; set; }
  public DbSet<StationEntity> Stations { get; set; }
  public DbSet<PortEntity> Ports { get; set; }
  public DbSet<BicingStationEntity> BicingStations { get; set; }
  public DbSet<StateBicingEntity> StateBicing { get; set; }
  public DbSet<SavedUbicationEntity> SavedUbications { get; set; }
  public DbSet<RouteEntity> Routes { get; set; }
  public DbSet<PublishedRouteEntity> PublishedRoutes { get; set; }

  public DbSet<ChatEntity> Chats { get; set; }

  public DbSet<MessageEntity> Messages { get; set; }

  public DbSet<UserBlockEntity> UserBlocks { get; set; }



  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (optionsBuilder == null)
    {
      throw new ArgumentNullException(nameof(optionsBuilder));
    }
    if (!optionsBuilder.IsConfigured)
    {
      var connectionString = this._configuration.GetConnectionString("DefaultConnection");
      optionsBuilder.UseNpgsql(connectionString, options =>
      {
        options.UseNetTopologySuite();
        options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorCodesToAdd: []);
      });
    }
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    if (modelBuilder == null)
    {
      throw new ArgumentNullException(nameof(modelBuilder));
    }
    modelBuilder.HasPostgresExtension("postgis");

    modelBuilder.Entity<UserEntity>(entity =>
    {
      entity.ToTable("users");
      entity.HasKey(e => e.UserId);

      entity.Property(e => e.UserId)
        .HasColumnName("id_user")
        .HasColumnType("uuid");

      entity.Property(e => e.Username)
        .HasColumnName("username")
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
        entity.HasKey(e => new { e.UbicationId, e.UserEmail, e.StationType });

        //Columns
        entity.Property(e => e.UbicationId)
              .HasColumnName("ubication_id")
              .HasColumnType("integer");

        entity.Property(e => e.UserEmail)
              .HasColumnName("email")
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

        entity.Property(e => e.Valoration)
              .HasColumnName("valoration")
              .HasColumnType("integer");

        entity.Property(e => e.Comment)
              .HasColumnName("comment")
              .HasColumnType("text");
      });

    modelBuilder.Entity<RouteEntity>(entity =>
      {
        entity.ToTable("route");
        entity.HasKey(e => e.RouteId);

        entity.Property(e => e.RouteId)
          .HasColumnName("route_id")
          .HasColumnType("uuid");

        entity.Property(e => e.OriginLat)
          .HasColumnName("originlat")
          .HasColumnType("real");

        entity.Property(e => e.OriginLng)
          .HasColumnName("originlng")
          .HasColumnType("real");

        entity.Property(e => e.DestinationLat)
          .HasColumnName("destinationlat")
          .HasColumnType("real");

        entity.Property(e => e.DestinationLng)
          .HasColumnName("destinationlng")
          .HasColumnType("real");

        entity.Property(e => e.Mean)
          .HasColumnName("mean")
          .HasColumnType("text");

        entity.Property(e => e.Preference)
          .HasColumnName("preference")
          .HasColumnType("text");

        entity.Property(e => e.Distance)
          .HasColumnName("distance")
          .HasColumnType("real");

        entity.Property(e => e.Duration)
          .HasColumnName("duration")
          .HasColumnType("real");

        entity.Property(e => e.GeometryJson)
          .HasColumnName("geometryjson")
          .HasColumnType("jsonb");

        entity.Property(e => e.InstructionsJson)
          .HasColumnName("instructionsjson")
          .HasColumnType("jsonb");

        entity.Property(e => e.OriginStreetName)
          .HasColumnName("origin_street_name")
          .HasColumnType("text");

        entity.Property(e => e.DestinationStreetName)
          .HasColumnName("destination_street_name")
          .HasColumnType("text");

        entity.Property(e => e.UserId)
            .HasColumnName("id_user")
            .HasColumnType("uuid");

        entity.HasOne(e => e.UserIdNavigation)
          .WithMany()
          .HasForeignKey(e => e.UserId);
      });
    modelBuilder.Entity<PublishedRouteEntity>(entity =>
      {
        entity.ToTable("published_route");
        entity.HasKey(e => e.RouteId);

        entity.Property(e => e.RouteId)
          .HasColumnName("route_id")
          .HasColumnType("uuid");

        entity.Property(e => e.Date)
          .HasColumnName("date")
          .HasColumnType("timestamp with time zone");

        entity.Property(e => e.AvailableSeats)
          .HasColumnName("available_seats")
          .HasColumnType("integer");
        //Relations
        entity.HasOne(e => e.RouteIdNavigation)
          .WithMany()
          .HasForeignKey(e => e.RouteId);
      });

    modelBuilder.Entity<ChatEntity>(entity =>
      {
        entity.ToTable("chat");
        entity.HasKey(e => e.ChatId);

        entity.Property(e => e.ChatId)
          .HasColumnName("chat_id")
          .HasColumnType("uuid");
        entity.Property(e => e.RouteId)
          .HasColumnName("route_id")
          .HasColumnType("uuid");
        entity.Property(e => e.User1Id)
          .HasColumnName("user_id1")
          .HasColumnType("uuid");
        entity.Property(e => e.User2Id)
          .HasColumnName("user_id2")
          .HasColumnType("uuid");
        entity.HasOne(e => e.UserId1Navigation)
          .WithMany()
          .HasForeignKey(e => e.User1Id);

        entity.HasOne(e => e.UserId2Navigation)
          .WithMany()
          .HasForeignKey(e => e.User2Id);
        entity.HasOne(e => e.PublicRouteNavigation)
          .WithMany()
          .HasForeignKey(e => e.RouteId);

      });
    modelBuilder.Entity<MessageEntity>(entity =>
      {
        entity.ToTable("message");
        entity.HasKey(e => e.MessageId);

        entity.Property(e => e.MessageId)
          .HasColumnName("message_id")
          .HasColumnType("uuid");
        entity.Property(e => e.ChatId)
          .HasColumnName("chat_id")
          .HasColumnType("uuid");
        entity.Property(e => e.UserId)
          .HasColumnName("user_id")
          .HasColumnType("uuid");
        entity.Property(e => e.Message)
          .HasColumnName("message_text")
          .HasColumnType("text");
        entity.Property(e => e.CreatedAt)
          .HasColumnName("created_at")
          .HasColumnType("timestamp with time zone");

        entity.HasOne(e => e.ChatIdNavigation)
        .WithMany()
        .HasForeignKey(e => e.ChatId);

        entity.HasOne(e => e.UserIdNavigation)
            .WithMany()
            .HasForeignKey(e => e.UserId);
      });
    modelBuilder.Entity<UserBlockEntity>(entity =>
      {
        entity.ToTable("user_block");
        entity.HasKey(e => new { e.BlockerId, e.BlockedId });

        entity.Property(e => e.BlockerId)
          .HasColumnName("blocker_id")
          .HasColumnType("uuid");

        entity.Property(e => e.BlockedId)
          .HasColumnName("blocked_id")
          .HasColumnType("uuid");

        entity.HasOne(e => e.Blocker)
          .WithMany()
          .HasForeignKey(e => e.BlockerId);

        entity.HasOne(e => e.Blocked)
          .WithMany()
          .HasForeignKey(e => e.BlockedId);
      });


  }
}

