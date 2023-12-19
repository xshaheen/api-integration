using System;

namespace ApiIntegration.Cli.Services;

public sealed class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(string value)
    {
        Console.WriteLine(value);
    }
}