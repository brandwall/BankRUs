using BankRUs.Application.Repositories;
using BankRUs.Intrastructure.Persistance.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankRUs.Intrastructure.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
}
