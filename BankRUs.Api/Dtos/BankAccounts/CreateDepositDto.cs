using BankRUs.Domain.ValueObjects;

namespace BankRUs.Api.Dtos.BankAccounts;

public record CreateDepositDto(
    decimal Amount,
    string Reference,
    string Currency
);