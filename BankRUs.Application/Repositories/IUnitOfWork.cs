using System;
using System.Collections.Generic;
using System.Text;

namespace BankRUs.Application.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
