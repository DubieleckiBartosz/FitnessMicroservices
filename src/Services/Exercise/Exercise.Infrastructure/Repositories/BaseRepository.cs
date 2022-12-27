using Exercise.Application.Options;
using Fitness.Common.Dapper;
using Fitness.Common.Logging;
using Microsoft.Extensions.Options;

namespace Exercise.Infrastructure.Repositories;

public abstract class BaseRepository<TRepository>
{
    private readonly DapperConnection<TRepository> _connection;

    protected BaseRepository(IOptions<DatabaseConnection> dbConnection, ILoggerManager<TRepository> loggerManager)
    {
        var connectionStrings =
            dbConnection?.Value ?? throw new ArgumentNullException(nameof(dbConnection));

        _connection = new DapperConnection<TRepository>(connectionStrings.DefaultConnection, loggerManager);
    }
}