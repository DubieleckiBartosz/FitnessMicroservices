namespace Identity.Infrastructure.Repositories
{
    public abstract class BaseRepository<TRepository>
    {
        private readonly ILoggerManager<TRepository> _loggerManager; 
        private readonly ConnectionStrings _connectionStrings;
        private readonly AsyncPolicy _retryAsyncPolicyQuery;
        private readonly AsyncPolicy _retryAsyncPolicyConnection;

        protected BaseRepository(IOptions<ConnectionStrings> connectionString, ILoggerManager<TRepository> loggerManager)
        { 
            this._loggerManager = loggerManager ?? throw new ArgumentNullException(nameof(loggerManager));
            this._connectionStrings =
                connectionString?.Value ?? throw new ArgumentNullException(nameof(connectionString));

            var policy = new PolicySetup();
            this._retryAsyncPolicyConnection = policy.PolicyConnectionAsync(this._loggerManager);
            this._retryAsyncPolicyQuery = policy.PolicyQueryAsync(this._loggerManager);
        }

        private async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> funcData)
        {
            try
            {
                SqlConnection? connection;  
                await using (connection = new SqlConnection(this._connectionStrings.DefaultConnection))
                {
                    await this._retryAsyncPolicyConnection.ExecuteAsync(async () => await connection.OpenAsync());

                    _loggerManager.LogInformation(null, message: "Connection has been opened");

                    return await this._retryAsyncPolicyQuery.ExecuteAsync(async () => await funcData(connection));
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception($"Timeout sql exception: {ex}");
            }
            catch (SqlException ex)
            {
                throw new Exception($"Sql exception: {ex}");
            }
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null,
            CommandType? commandType = null)
        {
            return await this.WithConnection(
                async _ => await _.QueryAsync<T>(sql: sql, param: param, 
                    commandType: commandType));
        }

        protected async Task<int> ExecuteAsync(string sql, object? param = null, 
            CommandType? commandType = null)
        {
            return await this.WithConnection( 
                async _ => await _.ExecuteAsync(sql: sql, param: param, 
                    commandType: commandType));
        }

        protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map,
            string splitOn = "Id", object? param = null, 
            CommandType? commandType = null)
        {
            return await this.WithConnection( 
                async _ => await _.QueryAsync<T1, T2, TReturn>(sql: sql, map: map, splitOn: splitOn, param: param, 
                    commandType: commandType));
        }

        protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(string sql,
            Func<T1, T2, T3, TReturn> map,
            string splitOn = "Id", object? param = null, 
            CommandType? commandType = null)
        {
            return await this.WithConnection( 
                async _ => await _.QueryAsync<T1, T2, T3, TReturn>(sql: sql, map: map, splitOn: splitOn, param: param, 
                    commandType: commandType));
        }

        protected async Task<IEnumerable<Tuple<T1, T2, T3>>> QueryMultipleAsync<T1, T2, T3>(string sql,
            string splitOn = "Id", object? param = null, 
            CommandType? commandType = null)
        {
            return await this.WithConnection( 
                async _ => await _.QueryAsync<T1, T2, T3, Tuple<T1, T2, T3>>(sql: sql,
                    (t, t2, t3) => Tuple.Create(t, t2, t3), splitOn: splitOn,
                    param: param, 
                    commandType: commandType));
        }
    }
}
