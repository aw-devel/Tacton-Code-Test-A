# Tacton Engineering - Code Test A

[![Unit Tests](https://github.com/aw-devel/Tacton-Code-Test-A/actions/workflows/ci.yml/badge.svg)](https://github.com/aw-devel/Tacton-Code-Test-A/actions/workflows/ci.yml)

A C# calculator that evaluates mathematical expressions using Reverse Polish Notation (RPN) and the Shunting Yard algorithm.

## Features

- Basic operations: +, -, *, /
- Operator precedence handling
- Negative numbers support
- Proper error handling
- 100% test coverage

## Algorithm

Shunting Yard → RPN Evaluation
```
Input:  "3 + 4 * 2"

RPN:    "3 4 2 * +"

Result: 11
```
## Usage

```
Calculator.Calculate("2 + 3");        // Returns: 5

Calculator.Calculate("3 * 2 + 1");    // Returns: 7

Calculator.Calculate("3 * -2 + 6");   // Returns: 0
```

## Build & Test

Build:
```dotnet build```

Run tests:
``dotnet test``

## Project Structure
```
TactonCalculator/           - Core library
TactonCalculator.Tests/     - Unit tests (xUnit)
```

## References

https://en.wikipedia.org/wiki/Polish_notation

https://en.wikipedia.org/wiki/Shunting_yard_algorithm