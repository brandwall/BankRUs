using BankRUs.Application.Exceptions;
using BankRUs.Application.Repositories;
using BankRUs.Application.UseCases.CreateDeposit;
using BankRUs.Domain.Entities;
using BankRUs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankRUs.Application.UseCases.CreateWithdrawal;

public class CreateWithdrawalHandler
{
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateWithdrawalHandler(IBankAccountRepository bankAccountRepository, IUnitOfWork unitOfWork)
    {
        _bankAccountRepository = bankAccountRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<CreateWithdrawalResult> HandleAsync(CreateWithdrawalCommand command)
    {
        // 0. Validera och skapa rätt currency
        var currency = Currency.FromCode(command.CurrencyCode);

        // 1. Hämta bankkontot
        BankAccount? bankAccount = await _bankAccountRepository.GetByIdAsync(command.BankAccountId);

        // 2. Validera att det finns
        if (bankAccount == null)
            throw new NotFoundException($"Bank account with id: {command.BankAccountId} was not found.");

        // 2.1 Validera så att det är personens konto
        if (bankAccount.UserId != command.UserId)
            throw new NotFoundException($"Bank account with id: {command.BankAccountId} was not found.");

        // 3. Skapa depositen
        var transaction = new Transaction
            (
                bankAccountId: bankAccount.Id,
                amount: command.Amount,
                reference: command.Reference,
                currency: currency,
                transactionType: command.TransactionType
            );

        // 4. Lägg till deposit för kontot
        bankAccount.Withdraw(transaction);
        await _bankAccountRepository.AddAsync(transaction);

        // 5. Spara ändringarna
        await _unitOfWork.SaveChangesAsync();


        // 6. Skapa retur objekt
        var result = new CreateWithdrawalResult
            (
                TransactionId: transaction.Id,
                TransactionDate: transaction.TransactionDate,
                AccountNumber: bankAccount.AccountNumber,
                AccountBalance: bankAccount.Balance,
                DepositAmount: transaction.Amount,
                Currency: transaction.Currency.Code,
                TransactionType: transaction.TransactionType
            );

        return result;
    }
}
