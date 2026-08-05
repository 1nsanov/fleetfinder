using Bogus;
using fleetfinder.service.main.application.Common.Enums;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.application.Common.Options;
using fleetfinder.service.main.domain.Bases;
using fleetfinder.service.main.domain.Enums.Common;
using fleetfinder.service.main.domain.Enums.Transport;
using fleetfinder.service.main.domain.Enums.Transport.Cargo;
using fleetfinder.service.main.domain.Enums.Transport.Passenger;
using fleetfinder.service.main.domain.Enums.Transport.Special;
using fleetfinder.service.main.domain.Transport.Cargo;
using fleetfinder.service.main.domain.Transport.Passenger;
using fleetfinder.service.main.domain.Transport.Special;
using fleetfinder.service.main.domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace fleetfinder.service.main.application.Common.Seed;

public class DemoDataSeeder : IDemoDataSeeder
{
    public const string HttpClientName = "SeedImages";

    private static readonly string[] DemoLogins = { "demo", "carrier1", "carrier2" };
    private const int TransportsPerType = 12;

    private readonly CommandDbContext _db;
    private readonly IPasswordService _passwordService;
    private readonly IObjectStorageService _objectStorage;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly SeedOptions _options;
    private readonly ILogger<DemoDataSeeder> _logger;

    public DemoDataSeeder(
        CommandDbContext db,
        IPasswordService passwordService,
        IObjectStorageService objectStorage,
        IHttpClientFactory httpClientFactory,
        IOptions<SeedOptions> options,
        ILogger<DemoDataSeeder> logger)
    {
        _db = db;
        _passwordService = passwordService;
        _objectStorage = objectStorage;
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
        _logger = logger;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _db.User.AnyAsync(cancellationToken))
        {
            _logger.LogInformation("Demo seed skipped: database already contains users");
            return;
        }

        Randomizer.Seed = new Random(42);
        var faker = new Faker("ru");
        var encryptedPassword = _passwordService.EncryptPassword(_options.DemoPassword);
        var http = _httpClientFactory.CreateClient(HttpClientName);

        _logger.LogInformation("Demo seed started");

        var users = new List<User>(DemoLogins.Length);
        foreach (var login in DemoLogins)
        {
            var avatarUrl = await DownloadAndUploadAsync(
                http,
                $"https://picsum.photos/seed/{login}-avatar/400/400",
                StorageFolder.UserProfile,
                $"{login}-avatar.jpg",
                "image/jpeg",
                cancellationToken);

            users.Add(new User
            {
                Login = login,
                Password = encryptedPassword,
                Email = faker.Internet.Email(login).ToLowerInvariant(),
                Organization = faker.Company.CompanyName(),
                ImageUrl = avatarUrl,
                FullName = new FullName
                {
                    First = faker.Name.FirstName(),
                    Second = faker.Name.LastName(),
                    Surname = faker.Name.LastName()
                },
                Contact = new Contact
                {
                    PhoneTelegram = faker.Phone.PhoneNumber("77######"),
                    PhoneWhatsapp = faker.Phone.PhoneNumber("77######"),
                    PhoneViber = faker.Phone.PhoneNumber("77######"),
                    WorkingMode = "Пн–Пт 09:00–18:00"
                }
            });
        }

        await _db.User.AddRangeAsync(users, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        var cargoTransports = new List<CargoTransport>();
        var passengerTransports = new List<PassengerTransport>();
        var specialTransports = new List<SpecialTransport>();

        for (var i = 0; i < TransportsPerType; i++)
        {
            var user = users[i % users.Count];

            var cargo = CreateCargoTransport(faker, user.Id);
            cargo.Images = await CreateTransportImagesAsync<CargoTransportImage>(
                http, faker, StorageFolder.CargoTransport, cancellationToken);
            cargoTransports.Add(cargo);

            var passenger = CreatePassengerTransport(faker, user.Id);
            passenger.Images = await CreateTransportImagesAsync<PassengerTransportImage>(
                http, faker, StorageFolder.PassengerTransport, cancellationToken);
            passengerTransports.Add(passenger);

            var special = CreateSpecialTransport(faker, user.Id);
            special.Images = await CreateTransportImagesAsync<SpecialTransportImage>(
                http, faker, StorageFolder.SpecialTransport, cancellationToken);
            specialTransports.Add(special);
        }

        await _db.CargoTransport.AddRangeAsync(cargoTransports, cancellationToken);
        await _db.PassengerTransport.AddRangeAsync(passengerTransports, cancellationToken);
        await _db.SpecialTransport.AddRangeAsync(specialTransports, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Demo seed completed: {Users} users, {Cargo} cargo, {Passenger} passenger, {Special} special",
            users.Count,
            cargoTransports.Count,
            passengerTransports.Count,
            specialTransports.Count);
    }

    private static decimal Decimal2(Faker faker, decimal min, decimal max) =>
        Math.Round(faker.Random.Decimal(min, max), 2);

    private static CargoTransport CreateCargoTransport(Faker faker, long userId)
    {
        var length = Decimal2(faker, 3, 14);
        var width = Decimal2(faker, 1.5m, 2.5m);
        var height = Decimal2(faker, 1.5m, 3m);
        return new CargoTransport
        {
            UserId = userId,
            Title = faker.Commerce.ProductName(),
            Region = faker.PickRandom<Region>(),
            Brand = faker.Vehicle.Manufacturer(),
            YearIssue = faker.Date.Past(15).Year.ToString(),
            ExperienceWork = faker.PickRandom<ExperienceWork>(),
            PaymentMethod = faker.PickRandom<PaymentMethod>(),
            PaymentOrder = faker.PickRandom<PaymentOrder>(),
            Description = faker.Lorem.Paragraph(),
            Type = faker.PickRandom<CargoType>(),
            TransportationKind = faker.PickRandom<CargoTransportationKind>(),
            Price = new Price
            {
                PerHour = Decimal2(faker, 200, 800),
                PerShift = Decimal2(faker, 1500, 6000),
                PerKm = Decimal2(faker, 10, 50)
            },
            Body = new Body
            {
                LoadCapacity = Decimal2(faker, 1, 20),
                Length = length,
                Width = width,
                Height = height,
                Volume = Math.Round(length * width * height, 2),
                Kind = faker.PickRandom<CargoBodyKind>()
            }
        };
    }

    private static PassengerTransport CreatePassengerTransport(Faker faker, long userId)
    {
        return new PassengerTransport
        {
            UserId = userId,
            Title = faker.Commerce.ProductName(),
            Region = faker.PickRandom<Region>(),
            Brand = faker.Vehicle.Manufacturer(),
            YearIssue = faker.Date.Past(12).Year.ToString(),
            ExperienceWork = faker.PickRandom<ExperienceWork>(),
            PaymentMethod = faker.PickRandom<PaymentMethod>(),
            PaymentOrder = faker.PickRandom<PaymentOrder>(),
            Description = faker.Lorem.Paragraph(),
            Type = faker.PickRandom<PassengerType>(),
            RentalDuration = faker.PickRandom<PassengerRentalDuration>(),
            Facilities = faker.PickRandom<PassengerFacilities>(),
            CountSeats = faker.Random.Int(2, 50),
            Option = faker.PickRandom<PassengerOption>(),
            TransportationKind = faker.PickRandom<PassengerTransportationKind>(),
            Color = faker.Commerce.Color(),
            MinOrderTime = Decimal2(faker, 1, 8),
            Price = new Price
            {
                PerHour = Decimal2(faker, 150, 700),
                PerShift = Decimal2(faker, 1000, 5000),
                PerKm = Decimal2(faker, 5, 30)
            },
            Size = new Size
            {
                Length = Decimal2(faker, 3, 12),
                Width = Decimal2(faker, 1.5m, 2.5m),
                Height = Decimal2(faker, 1.5m, 3.5m)
            }
        };
    }

    private static SpecialTransport CreateSpecialTransport(Faker faker, long userId)
    {
        return new SpecialTransport
        {
            UserId = userId,
            Title = faker.Commerce.ProductName(),
            Region = faker.PickRandom<Region>(),
            Brand = faker.Vehicle.Manufacturer(),
            YearIssue = faker.Date.Past(20).Year.ToString(),
            ExperienceWork = faker.PickRandom<ExperienceWork>(),
            PaymentMethod = faker.PickRandom<PaymentMethod>(),
            PaymentOrder = faker.PickRandom<PaymentOrder>(),
            Description = faker.Lorem.Paragraph(),
            Type = faker.PickRandom<SpecialType>(),
            Price = new Price
            {
                PerHour = Decimal2(faker, 300, 1500),
                PerShift = Decimal2(faker, 2000, 12000),
                PerKm = Decimal2(faker, 15, 80)
            }
        };
    }

    private async Task<List<TImage>> CreateTransportImagesAsync<TImage>(
        HttpClient http,
        Faker faker,
        StorageFolder folder,
        CancellationToken cancellationToken)
        where TImage : ImageBase, new()
    {
        var count = faker.Random.Int(1, 4);
        var images = new List<TImage>(count);
        for (var i = 0; i < count; i++)
        {
            var seed = faker.Random.Guid().ToString("N");
            var url = await DownloadAndUploadAsync(
                http,
                $"https://picsum.photos/seed/{seed}/800/600",
                folder,
                $"{seed}.jpg",
                "image/jpeg",
                cancellationToken);
            images.Add(new TImage { Url = url });
        }

        return images;
    }

    private async Task<string> DownloadAndUploadAsync(
        HttpClient http,
        string sourceUrl,
        StorageFolder folder,
        string fileName,
        string contentType,
        CancellationToken cancellationToken)
    {
        using var response = await http.GetAsync(sourceUrl, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Failed to download seed image from '{sourceUrl}': {(int)response.StatusCode} {response.ReasonPhrase}");
        }

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        await using var buffer = new MemoryStream();
        await stream.CopyToAsync(buffer, cancellationToken);
        buffer.Position = 0;

        var resolvedContentType = response.Content.Headers.ContentType?.MediaType;
        if (string.IsNullOrWhiteSpace(resolvedContentType) ||
            resolvedContentType.Equals("application/octet-stream", StringComparison.OrdinalIgnoreCase))
        {
            resolvedContentType = contentType;
        }

        return await _objectStorage.UploadAsync(
            folder,
            fileName,
            buffer,
            resolvedContentType,
            cancellationToken);
    }
}
