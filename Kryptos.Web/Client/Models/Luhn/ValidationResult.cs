namespace Kryptos.Web.Client.Models.Luhn;

public record ValidationResult (bool IsValid, string? CardNumber = null, string? Issuer = null, string? Industry = null);