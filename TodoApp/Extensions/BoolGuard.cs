using System.Runtime.CompilerServices;

namespace Ardalis.GuardClauses;

public static class BoolGuard
{
    public static void FalseBool(this IGuardClause guardClause,
        bool input,
        [CallerArgumentExpression("input")] string? parameterName = null,
        string message = "Should not have been false!"
        )
    {
        if (!input)
            throw new ArgumentException(message, parameterName);
    }
}

