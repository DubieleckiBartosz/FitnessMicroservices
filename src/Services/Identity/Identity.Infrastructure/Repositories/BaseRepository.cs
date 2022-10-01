namespace Identity.Infrastructure.Repositories
{
    public abstract class BaseRepository<TRepository>
    {
        private readonly ILoggerManager<TRepository> _loggerManager;
        private readonly ITransaction _transaction;
        private readonly ConnectionString _connectionString;
        private readonly AsyncPolicy _retryAsyncPolicyQuery;
        private readonly AsyncPolicy _retryAsyncPolicyConnection;

        protected BaseRepository(IOptions<ConnectionString> connectionString, ILoggerManager<TRepository> loggerManager,
            ITransaction transaction)
        {
            this._transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
            this._loggerManager = loggerManager ?? throw new ArgumentNullException(nameof(loggerManager));
            this._connectionString =
                connectionString?.Value ?? throw new ArgumentNullException(nameof(connectionString));

            var policy = new PolicySetup();
            this._retryAsyncPolicyConnection = policy.PolicyConnectionAsync(this._loggerManager);
            this._retryAsyncPolicyQuery = policy.PolicyQueryAsync(this._loggerManager);
        }

        protected SqlTransaction? CurrentTransaction() => this._transaction.GetTransactionWhenExist() as SqlTransaction;

        private async Task<T> WithConnection<T>(IDbTransaction? transaction, Func<IDbConnection, Task<T>> funcData)
        {
            try
            {
                SqlConnection? connection;
                if (transaction != null)
                {
                    connection = transaction?.Connection as SqlConnection ?? throw new ArgumentNullException(nameof(transaction.Connection));
                    return await this._retryAsyncPolicyQuery.ExecuteAsync(async () => await funcData(connection));
                }

                await using (connection = new SqlConnection(this._connectionString.DefaultConnection))
                {
                    await this._retryAsyncPolicyConnection.ExecuteAsync(async () => await connection.OpenAsync());

                    _loggerManager.LogInformation(null, message: "Connection has been opened");

                    return await this._retryAsyncPolicyQuery.ExecuteAsync(async () => await funcData(connection));
                }
            }
            catch (TimeoutException ex)
            {
                if (this.CurrentTransaction() != null)
                {
                    this._transaction.Rollback();
                }

                throw new Exception($"Timeout sql exception: {ex}");
            }
            catch (SqlException ex)
            {
                if (this.CurrentTransaction() != null)
                {
                    this._transaction.Rollback();
                }

                throw new Exception($"Sql exception: {ex}");
            }
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null,
            IDbTransaction? transaction = null,
            CommandType? commandType = null)
        {
            return await this.WithConnection(transaction,
                async _ => await _.QueryAsync<T>(sql: sql, param: param,
                    transaction: transaction,
                    commandType: commandType));
        }

        protected async Task<int> ExecuteAsync(string sql, object? param = null,
            IDbTransaction? transaction = null,
            CommandType? commandType = null)
        {
            return await this.WithConnection(transaction,
                async _ => await _.ExecuteAsync(sql: sql, param: param,
                    transaction: transaction,
                    commandType: commandType));
        }

        protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string sql, Func<T1, T2, TReturn> map,
            string splitOn = "Id", object? param = null,
            IDbTransaction? transaction = null,
            CommandType? commandType = null)
        {
            return await this.WithConnection(transaction,
                async _ => await _.QueryAsync<T1, T2, TReturn>(sql: sql, map: map, splitOn: splitOn, param: param,
                    transaction: transaction,
                    commandType: commandType));
        }

        protected async Task<IEnumerable<TReturn>> QueryAsync<T1, T2, T3, TReturn>(string sql,
            Func<T1, T2, T3, TReturn> map,
            string splitOn = "Id", object? param = null,
            IDbTransaction? transaction = null,
            CommandType? commandType = null)
        {
            return await this.WithConnection(transaction,
                async _ => await _.QueryAsync<T1, T2, T3, TReturn>(sql: sql, map: map, splitOn: splitOn, param: param,
                    transaction: transaction,
                    commandType: commandType));
        }

        protected async Task<IEnumerable<Tuple<T1, T2, T3>>> QueryMultipleAsync<T1, T2, T3>(string sql,
            string splitOn = "Id", object? param = null,
            IDbTransaction? transaction = null,
            CommandType? commandType = null)
        {
            return await this.WithConnection(transaction,
                async _ => await _.QueryAsync<T1, T2, T3, Tuple<T1, T2, T3>>(sql: sql,
                    (t, t2, t3) => Tuple.Create(t, t2, t3), splitOn: splitOn,
                    param: param,
                    transaction: transaction,
                    commandType: commandType));
        }
    }
}
