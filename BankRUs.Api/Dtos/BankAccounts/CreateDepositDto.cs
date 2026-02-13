using BankRUs.Api.Attributes;
using BankRUs.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace BankRUs.Api.Dtos.BankAccounts;

public record CreateDepositDto(
        [Required]
        [IsPositiveDecimal]
        decimal Amount,
        [MaxLength(140)]
        string Reference,
        [Required]
        [StringLength(3, MinimumLength = 3)]
        string CurrencyCode
    );