using System.Data;

namespace Identity.Application.Contracts;

public interface ITransaction
{
    IDbTransaction GetTransactionWhenExist();
    Task<IDbTransaction> GetOpenOrCreateTransaction();
    bool Commit();
    void Rollback();
}