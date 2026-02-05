using BankRUs.Application.Repositories;
using BankRUs.Domain.Entities;
using BankRUs.Intrastructure.Persistance.Application;
using Microsoft.EntityFrameworkCore;

namespace BankRUs.Intrastructure.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly ApplicationDbContext _db;
    public BankAccountRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(BankAccount bankAccount)
    {
        await _db.BankAccounts.AddAsync(bankAccount);
    }

    public async Task<BankAccount?> GetByIdAsync(Guid bankAccountId)
    {
        return await _db.BankAccounts.FindAsync(bankAccountId);
    }
    public async Task<BankAccount?> GetByUserAsync(Guid userId)
    {
        return await _db.BankAccounts.SingleOrDefaultAsync(b => b.UserId == userId.ToString());
    }
}
