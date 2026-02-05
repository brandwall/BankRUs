using BankRUs.Application.Repositories;
using BankRUs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankRUs.Application.UseCases.CreateDeposit;

public class CreateDepositHandler
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDepositHandler(
        IBankAccountRepository bankAccountRepository,
        IUnitOfWork unitOfWork
        
        )
    {
        _bankAccountRepository = bankAccountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateDepositResult> HandleAsync(CreateDepositCommand command)
    {
        // 1. Hämta bankkontot
        BankAccount? bankAccount = await _bankAccountRepository.GetByIdAsync(command.BankAccountId);

        // 2. Validera att det finns
        if (bankAccount == null )
            throw new Exception("Bank account not found.");

        // 3. Skapa depositen
        var deposit = new Deposit
            (
                bankAccountId: bankAccount.Id,
                amount: command.Amount,
                reference: command.Reference,
                currency: command.Currency
            );

        // 4. Lägg till deposit för kontot
        bankAccount.AddDeposit(deposit);

        // 5. Spara ändringarna
        await _unitOfWork.SaveChangesAsync();


        // 6. Skapa retur objekt
        var result = new CreateDepositResult
            (
                TransactionId: deposit.Id,
                TransactionDate: deposit.TransactionDate,
                AccountNumber: bankAccount.AccountNumber,
                Balance: bankAccount.Balance
            );

        return result;
    }
}
