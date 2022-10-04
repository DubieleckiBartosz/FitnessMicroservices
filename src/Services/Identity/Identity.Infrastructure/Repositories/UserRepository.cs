namespace Identity.Infrastructure.Repositories;

public class UserRepository : BaseRepository<UserRepository>, IUserRepository
{
    public UserRepository(IOptions<ConnectionStrings> connectionString, ILoggerManager<UserRepository> loggerManager) :
        base(connectionString, loggerManager)
    {
    }

    public async Task AddToRoleAsync(User user)
    {
        var param = new DynamicParameters();

        var roleId = user.Roles.LastOrDefault()?.Id;
        param.Add("@userId", user.Id);
        param.Add("@role", roleId);

        var result =
            await this.ExecuteAsync("user_addToRole_I", param, 
                commandType: CommandType.StoredProcedure);
        if (result <= 0)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.NewRoleForUserFailed,
                ExceptionIdentityTitles.NewUserRole, HttpStatusCode.InternalServerError, null);
        } 
    }

    public async Task<int> CreateAsync(User user)
    {
        var param = new DynamicParameters();

        param.Add("@roleId", user.Roles.FirstOrDefault()?.Id);
        param.Add("@firstName", user.FirstName);
        param.Add("@lastName", user.LastName);
        param.Add("@userName", user.UserName);
        param.Add("@email", user.Email);
        param.Add("@phoneNumber", user.PhoneNumber);
        param.Add("@passwordHash", user.PasswordHash);
        param.Add("@newIdentity", -1, DbType.Int32, ParameterDirection.Output);

        await ExecuteAsync("user_createNewUser_I", param,
            commandType: CommandType.StoredProcedure);
        var result = param.Get<int?>("@newIdentity");

        if (!result.HasValue || result <= 0)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.RegisterFailed,
                ExceptionIdentityTitles.CreatingUser, HttpStatusCode.InternalServerError, null);
        }

        return result.Value;
    }

    public async Task<User> FindUserByTokenAsync(string tokenKey)
    {
        var param = new DynamicParameters();

        param.Add("@tokenKey", tokenKey);
        var dict = new Dictionary<int, UserDao?>();
        var result = (await QueryAsync<UserDao, TokenDao, int, UserDao?>("user_getUserByToken_S",
            (u, rt, r) => MapperUserDatabase.Map(dict, u, rt, r),
            param: param, splitOn: "Id,Id,RoleId", commandType: CommandType.StoredProcedure)).FirstOrDefault();

        if (result == null)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.TokenNotMatch,
                ExceptionIdentityTitles.UserByToken, HttpStatusCode.NotFound, null);
        }

        return result.Map();
    }

    public async Task<User> FindByEmailAsync(string email, bool throwWhenNull = true)
    {
        var param = new DynamicParameters();

        param.Add("@email", email);

        var dict = new Dictionary<int, UserDao?>(); 
        var result = (await QueryAsync<UserDao?, TokenDao?, int, UserDao?>("user_getUserByEmail_S",
            (u, rt, r) => MapperUserDatabase.Map(dict, u, rt, r),
            param: param, splitOn: "Id,Id,RoleId", commandType: CommandType.StoredProcedure)).FirstOrDefault();
        
        if (throwWhenNull && result == null)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.UserNotFound,
                ExceptionIdentityTitles.UserByEmail, HttpStatusCode.NotFound, null);
        } 

        return result?.Map();
    }

    public async Task UpdateAsync(User user)
    {
        var toTableType = user.RefreshTokens.Select(_ => _).ToList();

        var tokenTable = new DataTable();
        tokenTable.Columns.Add(new DataColumn("Token", typeof(string)));
        tokenTable.Columns.Add(new DataColumn("Expires", typeof(DateTime)));
        tokenTable.Columns.Add(new DataColumn("Created", typeof(DateTime)));
        tokenTable.Columns.Add(new DataColumn("ReplacedByToken", typeof(string)));
        tokenTable.Columns.Add(new DataColumn("Revoked", typeof(string)));

        foreach (var token in toTableType)
        {
            tokenTable.Rows.Add(token.Token, token.Expires, token.Expires, token.ReplacedByToken,
                token.TokenActivity?.Revoked);
        }


        var param = new DynamicParameters();

        param.Add("@email", user.Email);
        param.Add("@phoneNumber", user.PhoneNumber);
        param.Add("@userId", user.Id);
        param.Add("@refreshTokens",
            tokenTable.AsTableValuedParameter("UserRefreshTokensTableType"));

        var result =
            await ExecuteAsync("user_updateUserData_U", param,
                CommandType.StoredProcedure);
        if (result <= 0)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.UserUpdateDataFailed,
                ExceptionIdentityTitles.UpdatingUser, HttpStatusCode.InternalServerError, null);
        }
    }

    public async Task<List<string>> GetUserRolesAsync(User user)
    {
        var param = new DynamicParameters();

        param.Add("@userId", user.Id);

        var result =
            await this.QueryAsync<string>("user_getUserRoles_S", param,
                commandType: CommandType.StoredProcedure);
        
        if (result == null)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.UserRolesNotFound,
                ExceptionIdentityTitles.GettingRoles, HttpStatusCode.NotFound, null);
        }

        return result.ToList();
    }


    public async Task ClearTokens()
    {
        await this.ExecuteAsync("user_clearRevokedTokens_D",
            commandType: CommandType.StoredProcedure);
    } 
}

