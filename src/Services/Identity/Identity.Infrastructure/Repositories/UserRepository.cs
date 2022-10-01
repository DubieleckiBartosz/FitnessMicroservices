namespace Identity.Infrastructure.Repositories;

public class UserRepository : BaseRepository<UserRepository>, IUserRepository
{
    public UserRepository(IOptions<ConnectionString> connectionString, ILoggerManager<UserRepository> loggerManager,
        ITransaction transaction) : base(connectionString, loggerManager, transaction)
    {
    }

}