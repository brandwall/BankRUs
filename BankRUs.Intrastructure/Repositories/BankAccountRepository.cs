using BankRUs.Application.Repositories;
using BankRUs.Domain.Entities;
using BankRUs.Intrastructure.Persistance;

namespace BankRUs.Intrastructure.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly ApplicationDbContext _db;
    public BankAccountRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Add(BankAccount bankAccount)
    {
        _db.BankAccounts.Add(bankAccount);
        await _db.SaveChangesAsync();
    }
}
