namespace BankRUs.Domain.ValueObjects;

public class Currency
{
    public string Code { get; set; }
    private Currency(string code)
    {
        Code = code;
    }

    public static Currency FromCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Currency code cannot be empty");

        code = code.ToUpperInvariant();

        if (!ValidCurrencies.Contains(code))
            throw new ArgumentException($"Invalid currencty code: {code}");

        return new Currency(code);
    }

    public static readonly HashSet<string> ValidCurrencies = new()
    {
        "SEK", "USD", "EUR", "GBP", "NOK", "DKK", "PLN"
    };

    public override string ToString() => Code;
    public override bool Equals(object? obj)
    {
        return obj is Currency other && Code == other.Code;
    }
    public override int GetHashCode() => Code.GetHashCode();
}
