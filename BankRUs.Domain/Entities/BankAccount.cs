
using System.ComponentModel.DataAnnotations;

namespace BankRUs.Domain.Entities;

public class BankAccount
{
    public BankAccount(string accountNumber, string name, string userId)
    {
        Id = Guid.NewGuid();
        AccountNumber = accountNumber;
        Name = name;
        UserId = userId;
    }

    public Guid Id { get; protected set; }


    private readonly List<Transaction> _transactions = new();


    [MaxLength(25)]
    public string AccountNumber { get; protected set; }
    
    [MaxLength(25)]
    public string Name { get; protected set; }
    
    public bool IsLocked { get; protected set; }

    public decimal Balance { get; protected set; }
    public string UserId { get; protected set; }

    public void Deposit(Transaction transaction) 
    {
        if (transaction.TransactionType != TransactionType.Deposit)
            throw new ArgumentException("Wrong transaction type");

        Balance += transaction.Amount;
    }

    public void Withdraw(Transaction transaction) 
    {
        if (transaction.TransactionType != TransactionType.Withdrawal)
            throw new ArgumentException("Wrong transaction type");
        if (Balance < transaction.Amount)
            throw new ArgumentException("Insufficient funds");
        
        Balance -= transaction.Amount;
    }
    //public void Withdraw(decimal amount, string reference) { }
}

// Konstruktor
// Object initializer-syntax


