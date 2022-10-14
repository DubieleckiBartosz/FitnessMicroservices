using Fitness.Common.Dapper;

namespace Identity.Infrastructure.Repositories
{
    public abstract class BaseRepository<TRepository>
    {
        private readonly DapperConnection<TRepository> _connection;

        protected BaseRepository(IOptions<ConnectionStrings> connectionString, ILoggerManager<TRepository> loggerManager)
        {  
            var connectionStrings =
                connectionString?.Value ?? throw new ArgumentNullException(nameof(connectionString));
            
            _connection = new DapperConnection<TRepository>(connectionStrings.DefaultConnection, loggerManager);
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null,
            CommandType? commandType = null)
        {
            return await this._connection.WithConnection(
                async _ => await _.QueryAsync<T>(sql: sql, param: param, 
                    commandType: commandType));
        }

        protected async Task<int> ExecuteAsync(string sql, object? param = null, 
            CommandType? commandType = null)
        {
            return await this._connection.WithConnection( 
                async _ => await _.ExecuteAsync(sql: sql, param: param, 
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

        protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(string sql,
            Func<T1, T2, T3, TReturn> map,
            string splitOn = "Id", object? param = null, 
            CommandType? commandType = null)
        {
            return await this._connection.WithConnection( 
                async _ => await _.QueryAsync<T1, T2, T3, TReturn>(sql: sql, map: map, splitOn: splitOn, param: param, 
                    commandType: commandType));
        }

        protected async Task<IEnumerable<Tuple<T1, T2, T3>>> QueryMultipleAsync<T1, T2, T3>(string sql,
            string splitOn = "Id", object? param = null, 
            CommandType? commandType = null)
        {
            return await this._connection.WithConnection( 
                async _ => await _.QueryAsync<T1, T2, T3, Tuple<T1, T2, T3>>(sql: sql,
                    (t, t2, t3) => Tuple.Create(t, t2, t3), splitOn: splitOn,
                    param: param, 
                    commandType: commandType));
        }
    }
}
