using System.Data.SqlClient;
using Fitness.Common.EventStore;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Training.API.Database;
using Training.IntegrationTests.Common; 

namespace Training.IntegrationTests.Setup
{
    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        private MockServices? _servicesMock;

        public MockServices FakeServices()
        {
            _servicesMock ??= new MockServices();

            return _servicesMock;
        }

        private const string? ConnectionEventStore = "Server=localhost,1440;Database=FitnessEventStoreTests;User id=sa;Password=a?i0/cEFB@v3dweF7C";
        private SqlConnection? _eventStoreConnection; 
        private const string ConnectionString = "DataSource=:memory:";
        private SqliteConnection? _connection;
        public async Task<SqlConnection?> GetEventStore() => _eventStoreConnection ?? await OpenEventStoreConnection();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {  
            builder.ConfigureServices(services =>
            {
                 
                services.Configure<EventStoreOptions>(opts =>
                {
                    opts.EventStoreConnection = ConnectionEventStore!;
                });

                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<TrainingContext>));

                services.Remove(descriptor!);

                services
                    .AddEntityFrameworkSqlite()
                    .AddDbContext<TrainingContext>(options =>
                    {
                        options.UseSqlite(OpenConnection());
                        options.UseInternalServiceProvider(services.BuildServiceProvider());
                    });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<TrainingContext>())
                {
                    appContext.Database.EnsureCreated();
                }

                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.AddMvc(_ => _.Filters.Add(new FakeUserFilter()));

                MockServices.RegisterMockServices(services: ref services, FakeServices().GetMocks());
            });
        } 

        private SqliteConnection OpenConnection()
        {
            if (_connection != null)
            {
                return _connection;
            }

            _connection = new SqliteConnection(ConnectionString);
            _connection.Open();

            return _connection;
        }

        private async Task<SqlConnection?> OpenEventStoreConnection()
        {
            var connection =  new SqlConnection(ConnectionEventStore);
            await connection.OpenAsync();

            return connection;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_connection != null)
            {
                _connection.Dispose();
                _connection.Close();

                _connection = null;
            }

            if (_eventStoreConnection != null)
            {
                _eventStoreConnection.Dispose();
                _eventStoreConnection.Close();

                _eventStoreConnection = null;
            }
        }
    }
}
