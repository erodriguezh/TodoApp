namespace TodoApp;

using System.Globalization;
using Ardalis.GuardClauses;

public class InputValidator : IInputValidator
{
    public void ValidateString(string input)
    {
        Guard.Against.Null(input, nameof(input));
    }
    public void ValidateInt(string input, string errorMessage = "Format is not accepted")
    {
        Guard.Against.Null(input, nameof(input));

        var isTodoNumber = int.TryParse(input, out var todoNumber);
        Guard.Against.FalseBool(isTodoNumber, nameof(input), errorMessage);
    }

    public bool IsInputEqual(string? input, string value)
    {
        Guard.Against.Null(input, nameof(input));

        var trimmedInput = input.Trim();
        var lowercaseInput = trimmedInput.ToLower(CultureInfo.InvariantCulture);

        return lowercaseInput.Equals(value, StringComparison.OrdinalIgnoreCase);
    }
}
