using System.Globalization;

namespace TactonCalculator;

/// <summary>
/// A calculator that evaluates mathematical expressions using the Shunting Yard algorithm
/// to convert infix notation to Reverse Polish Notation (RPN) and then evaluates the result.
/// Supports addition (+), subtraction (-), multiplication (*), and division (/) operations.
/// Handles both positive and negative integers with proper operator precedence.
/// </summary>
public static class Calculator
{
    /// <summary>
    /// Calculates and returns the value of an integer expression given as a space separated string.
    /// Uses the Shunting Yard algorithm for proper operator precedence handling.
    /// </summary>
    /// <param name="expression">The mathematical expression as a space-separated string (e.g., "2 + 3", "3 * -2 + 6")</param>
    /// <returns>The calculated result as a double</returns>
    /// <exception cref="ArgumentException">Thrown when the expression is null, empty, or invalid</exception>
    /// <exception cref="DivideByZeroException">Thrown when division by zero is attempted</exception>
    public static double Calculate(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentException("Expression cannot be null or empty", nameof(expression));

        var tokens = ParseTokens(expression);
        ValidateTokens(tokens);
        
        var rpnTokens = ConvertToRpn(tokens);
        return EvaluateRpn(rpnTokens);
    }

    private static List<string> ParseTokens(string expression)
    {
        return expression.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    private static void ValidateTokens(List<string> tokens)
    {
        if (tokens.Count == 0)
            throw new ArgumentException("Expression cannot be empty");

        if (tokens.Count % 2 == 0)
            throw new ArgumentException("Invalid expression format - must have odd number of tokens");
        
        for (int i = 0; i < tokens.Count; i++)
        {
            if (i % 2 == 0)
            {
                if (!IsValidNumber(tokens[i]))
                    throw new ArgumentException($"Invalid number format: '{tokens[i]}'");
            }
            else
            {
                if (!IsValidOperator(tokens[i]))
                    throw new ArgumentException($"Invalid operator: '{tokens[i]}'");
            }
        }
    }

    private static bool IsValidNumber(string token)
    {
        return double.TryParse(token, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
    }

    private static bool IsValidOperator(string token)
    {
        return token is "+" or "-" or "*" or "/";
    }

    /// <summary>
    /// Converts infix notation to Reverse Polish Notation using the Shunting Yard algorithm.
    /// This algorithm properly handles operator precedence and associativity.
    /// </summary>
    /// <param name="tokens">List of tokens in infix notation</param>
    /// <returns>List of tokens in RPN (postfix) notation</returns>
    private static List<string> ConvertToRpn(List<string> tokens)
    {
        var output = new List<string>();
        var operatorStack = new Stack<string>();

        foreach (var token in tokens)
        {
            if (IsValidNumber(token))
            {
                output.Add(token);
            }
            else if (IsValidOperator(token))
            {
                while (operatorStack.Count > 0 && 
                       IsValidOperator(operatorStack.Peek()) &&
                       GetPrecedence(operatorStack.Peek()) >= GetPrecedence(token))
                {
                    output.Add(operatorStack.Pop());
                }
                operatorStack.Push(token);
            }
        }
        
        while (operatorStack.Count > 0)
        {
            var op = operatorStack.Pop();
            if (IsValidOperator(op))
            {
                output.Add(op);
            }
        }

        return output;
    }

    /// <summary>
    /// Returns the precedence level of an operator.
    /// Higher numbers indicate higher precedence.
    /// </summary>
    /// <param name="op">The operator</param>
    /// <returns>Precedence level (1 for +/-, 2 for */)</returns>
    private static int GetPrecedence(string op)
    {
        return op switch
        {
            "+" or "-" => 1,
            "*" or "/" => 2,
            _ => 0
        };
    }

    /// <summary>
    /// Evaluates a Reverse Polish Notation expression.
    /// RPN is naturally unambiguous and doesn't require parentheses or precedence rules.
    /// </summary>
    /// <param name="rpnTokens">List of tokens in RPN notation</param>
    /// <returns>The calculated result</returns>
    private static double EvaluateRpn(List<string> rpnTokens)
    {
        var stack = new Stack<double>();

        foreach (var token in rpnTokens)
        {
            if (IsValidNumber(token))
            {
                // Push numbers onto the stack
                stack.Push(double.Parse(token, CultureInfo.InvariantCulture));
            }
            else if (IsValidOperator(token))
            {
                if (stack.Count < 2)
                    throw new ArgumentException("Invalid expression - insufficient operands");

                var right = stack.Pop();
                var left = stack.Pop();
                
                var result = token switch
                {
                    "+" => left + right,
                    "-" => left - right,
                    "*" => left * right,
                    "/" => right == 0 
                        ? throw new DivideByZeroException("Division by zero is not allowed") 
                        : left / right,
                    _ => throw new InvalidOperationException($"Unexpected operator: {token}")
                };

                stack.Push(result);
            }
        }

        if (stack.Count != 1)
            throw new ArgumentException("Invalid expression - malformed RPN");

        return stack.Pop();
    }
}