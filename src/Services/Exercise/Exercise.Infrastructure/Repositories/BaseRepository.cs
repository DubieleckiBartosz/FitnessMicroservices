using Dapper;
using Exercise.Application.Options;
using Fitness.Common.Dapper;
using Fitness.Common.Logging;
using Microsoft.Extensions.Options;
using System.Data;

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
    protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null,
        CommandType? commandType = null)
    {
        return await this._connection.WithConnection(
            async _ => await _.QueryAsync<T>(sql: sql, param: param,
                commandType: commandType));
    }
    protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map,
        string splitOn = "Id", object? param = null,
        CommandType? commandType = null)
    {
        return await this._connection.WithConnection(
            async _ => await _.QueryAsync<T1, T2, TReturn>(sql: sql, map: map, splitOn: splitOn, param: param,
                commandType: commandType));
    }

    protected async Task<int> ExecuteAsync(string sql, object? param = null,
        CommandType? commandType = null)
    {
        return await this._connection.WithConnection(
            async _ => await _.ExecuteAsync(sql: sql, param: param,
                commandType: commandType));
    }
}