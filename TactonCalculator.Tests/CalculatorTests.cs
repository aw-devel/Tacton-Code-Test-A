using Xunit;

namespace TactonCalculator.Tests;

public class CalculatorTests
{
    [Theory]
    [InlineData("2 + 3", 5)]
    [InlineData("3 * 2 + 1", 7)]
    [InlineData("3 * -2 + 6", 0)]
    public void Calculate_ExerciseExamples_ReturnsCorrectResult(string expression, double expected)
    {
        // Act
        var result = Calculator.Calculate(expression);

        // Assert
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("2 + 3", 5)]
    [InlineData("10 - 4", 6)]
    [InlineData("3 * 4", 12)]
    [InlineData("15 / 3", 5)]
    public void Calculate_BasicOperations_ReturnsCorrectResult(string expression, double expected)
    {
        // Act
        var result = Calculator.Calculate(expression);

        // Assert
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("3 * 2 + 1", 7)]
    [InlineData("1 + 3 * 2", 7)]
    [InlineData("8 / 2 + 3", 7)]
    [InlineData("2 + 8 / 2", 6)]
    [InlineData("10 - 2 * 3", 4)] 
    [InlineData("2 * 3 - 1", 5)]
    public void Calculate_OperatorPrecedence_ReturnsCorrectResult(string expression, double expected)
    {
        // Act
        var result = Calculator.Calculate(expression);

        // Assert
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("3 * -2 + 6", 0)]
    [InlineData("-5 + 3", -2)]
    [InlineData("10 + -4", 6)]
    [InlineData("-8 / -2", 4)]
    [InlineData("-3 * -4", 12)]
    public void Calculate_NegativeNumbers_ReturnsCorrectResult(string expression, double expected)
    {
        // Act
        var result = Calculator.Calculate(expression);

        // Assert
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("1 + 2 + 3", 6)]      
    [InlineData("10 - 3 - 2", 5)]     
    [InlineData("2 * 3 * 4", 24)]     
    [InlineData("24 / 2 / 3", 4)]      
    [InlineData("1 + 2 * 3 + 4", 11)] 
    [InlineData("20 / 4 + 2 * 3", 11)]
    public void Calculate_MultipleOperations_ReturnsCorrectResult(string expression, double expected)
    {
        // Act
        var result = Calculator.Calculate(expression);

        // Assert
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("2 + 3 * 4 - 1", 13)]
    [InlineData("5 * 2 + 3 * 4", 22)]
    [InlineData("15 / 3 + 2 * 4", 13)]
    [InlineData("20 - 4 * 2 + 6", 18)]
    public void Calculate_ComplexExpressions_ReturnsCorrectResult(string expression, double expected)
    {
        // Act
        var result = Calculator.Calculate(expression);

        // Assert
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Calculate_NullOrEmptyExpression_ThrowsArgumentException(string? expression)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Calculator.Calculate(expression!));
    }

    [Theory]
    [InlineData("5 /")]
    [InlineData("+ 5")]
    [InlineData("5 + 3 +")]
    [InlineData("5 3")]
    [InlineData("+ + 5")]
    public void Calculate_InvalidFormat_ThrowsArgumentException(string expression)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Calculator.Calculate(expression));
    }

    [Theory]
    [InlineData("abc + 5")]
    [InlineData("5 + def")]
    [InlineData("5 @ 3")]
    [InlineData("5.5 + 3")]
    public void Calculate_InvalidTokens_ThrowsArgumentException(string expression)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Calculator.Calculate(expression));
    }

    [Theory]
    [InlineData("5 / 0")]
    [InlineData("10 + 3 / 0")]
    [InlineData("0 / 0")]
    public void Calculate_DivisionByZero_ThrowsDivideByZeroException(string expression)
    {
        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => Calculator.Calculate(expression));
    }

    [Fact]
    public void Calculate_SingleNumber_ReturnsNumber()
    {
        // Act
        var result = Calculator.Calculate("42");

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public void Calculate_SingleNegativeNumber_ReturnsNumber()
    {
        // Act
        var result = Calculator.Calculate("-42");

        // Assert
        Assert.Equal(-42, result);
    }

    [Theory]
    [InlineData("100 / 3", 33.333333333333336)]
    [InlineData("7 / 2", 3.5)]
    public void Calculate_DivisionWithDecimals_ReturnsCorrectResult(string expression, double expected)
    {
        // Act
        var result = Calculator.Calculate(expression);

        // Assert
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("2147483647 + 1", 2147483648)]
    [InlineData("-2147483648 - 1", -2147483649)]
    public void Calculate_LargeNumbers_ReturnsCorrectResult(string expression, double expected)
    {
        // Act
        var result = Calculator.Calculate(expression);

        // Assert
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("1 + 2 - 3 + 4", 4)]
    [InlineData("20 / 4 / 2", 2.5)]
    [InlineData("2 * 3 / 2", 3)]
    [InlineData("10 - 5 + 3", 8)]
    public void Calculate_LeftAssociativity_ReturnsCorrectResult(string expression, double expected)
    {
        // Act
        var result = Calculator.Calculate(expression);

        // Assert
        Assert.Equal(expected, result, 10);
    }

    [Theory]
    [InlineData("1 + 2 * 3 / 2 - 1", 3)]
    [InlineData("8 / 2 * 3 + 1", 13)]
    [InlineData("2 + 3 * 4 / 2 - 1", 7)]
    public void Calculate_ComplexPrecedenceAndAssociativity_ReturnsCorrectResult(string expression, double expected)
    {
        // Act
        var result = Calculator.Calculate(expression);

        // Assert
        Assert.Equal(expected, result, 10);
    }
}