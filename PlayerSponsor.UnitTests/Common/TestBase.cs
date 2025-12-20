using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PlayerSponsor.Server.Auth;
using PlayerSponsor.Server.Data.Context;
using PlayerSponsor.Server.Models;

namespace PlayerSponsor.UnitTests.Common
{
    public class TestBase
    {
        public SqliteConnection sqliteConnection;
        public ApplicationDbContext identityDbContext;
        public UserManager<ApplicationUser> userManager;
        public SignInManager<ApplicationUser> signInManager;

        [SetUp]
        public void Setup()
        {
            SetUpTestDB();
        }

        private void SetUpTestDB()
        {
            // Build service colection to create identity UserManager and RoleManager. 
            IServiceCollection serviceCollection = new ServiceCollection();

            // Add ASP.NET Core Identity database in memory.
            sqliteConnection = new SqliteConnection("DataSource=:memory:");
            serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(sqliteConnection).EnableDetailedErrors());

            identityDbContext = serviceCollection.BuildServiceProvider().GetService<ApplicationDbContext>();
            identityDbContext.Database.OpenConnection();
            identityDbContext.Database.EnsureCreated();

            serviceCollection.AddLogging();

            // Add Identity using in memory database to create UserManager and RoleManager.
            serviceCollection.AddApplicationIdentity();

            // Get UserManager and RoleManager.
            userManager = serviceCollection.BuildServiceProvider().GetService<UserManager<ApplicationUser>>();

            signInManager = serviceCollection.BuildServiceProvider().GetService<SignInManager<ApplicationUser>>();
            // Get token validation settings.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            IConfigurationRoot configuration = builder.Build();
        }

        public ApplicationUser CreateTestUser(string userName = "testuser", string userId = "12345")
        {
            var user = new ApplicationUser { Id = userId, UserName = userName };

            identityDbContext.Users.Add(user);

            return user;
        }

        public ClubAdmin CreateTestClubAdmin(ApplicationUser user, Club? club = null)
        {
            club = club ?? CreateTestClub();

            var clubAdmin = new ClubAdmin
            {
                User = user,
                UserId = user.Id,
                ClubId = club.Id,
                Club = club
            };

            identityDbContext.Admins.Add(clubAdmin);

            return clubAdmin;
        }

        public Club CreateTestClub(int? clubId = null, string name = "Test Club", string logo = "Test Logo", string description = "Test Bio", string paymentDetails = "test Payment details", string playerKey = "TestPlayerKey", bool addProducts = false)
        {
            var club = new Club
            {
                Id = clubId ?? new Random().Next(),
                Name = name,
                Description = description,
                Email = "TestEmail@gmail.com",
                Slug = "test-club",
                PrimaryColour = "#FFFFFF",
                SecondaryColour = "#000000",
                HeroImageId = "TestHeroImageId"
            };

            if(addProducts)
            {
                club.Products = [new Product() {
                    Id = 101,
                    Name = "Product 101",
                    Description = "Test Product 101",
                    IconWord = "Icon101",
                    PriceUnit = 1500,
                    Tags = ["Tag1", "Tag2"]
                }];
            }

            identityDbContext.Clubs.Add(club);

            return club;
        }

        public Product CreateTestProduct(int productId = 1, string name = "Test Product", int priceUnit = 1000)
        {
            var product = new Product
            {
                Id = productId,
                Name = name,
                PriceUnit = priceUnit,
                Description = "Test Description",
                IconWord = "TestIcon"
            };

            identityDbContext.Products.Add(product);

            return product;
        }

        [TearDown]
        public void Teardown()
        {
            identityDbContext.Database.EnsureDeleted();
            identityDbContext.Dispose();
            sqliteConnection.Close();
        }
    }
}