using FastFinance.FastCrud.EFCore.Repositories;
using FastFinance.Models;
using FastFinance.Repositories;

namespace ExpenseTracker.Repositories;

public class AccountRepositoryFactory : IAccountRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    public AccountRepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IAccountRepository<TAccount> Create<TAccount>() where TAccount : IAccount
    {
        var type = typeof(TAccount);
        if (type == typeof(Account))
            return (_serviceProvider.GetRequiredService<IAccountRepository>() as IAccountRepository<TAccount>)!;
        throw new NotImplementedException();
    }
}
