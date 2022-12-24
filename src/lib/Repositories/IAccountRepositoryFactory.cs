using FastFinance.Models;

namespace FastFinance.Repositories;

public interface IAccountRepositoryFactory
{
    IAccountRepository<TAccount> Create<TAccount>() where TAccount : IAccount;
}
