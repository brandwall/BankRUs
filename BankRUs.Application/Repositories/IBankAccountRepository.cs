using BankRUs.Domain.Entities;

namespace BankRUs.Application.Repositories;

public interface IBankAccountRepository
{
    Task AddAsync(BankAccount bankAccount);
    Task AddAsync(Transaction transaction);

    Task<BankAccount?> GetByIdAsync(Guid bankAccountId);
    Task<BankAccount?> GetByUserAsync(Guid userId);
    //Task<bool> BankAccountExists(Guid bankAccountId);
}
