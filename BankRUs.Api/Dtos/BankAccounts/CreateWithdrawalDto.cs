using BankRUs.Api.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BankRUs.Api.Dtos.BankAccounts;

public record CreateWithdrawalDto(
        [Required]
        [IsPositiveDecimal]
        decimal Amount,
        [MaxLength(140)]
        string Reference,
        [Required]
        [StringLength(3, MinimumLength = 3)]
        string CurrencyCode
    );
