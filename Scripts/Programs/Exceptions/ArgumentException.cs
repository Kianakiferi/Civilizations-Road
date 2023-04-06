using System;

namespace CivilizationsRoad.Scripts.Program.Exceptions;
public class ArgumentException : Exception
{
    public ArgumentException() { }
    public ArgumentException(string message) : base(message) { }
    //public ArgumentException(string message, Exception inner) : base(message, inner) { }

    public static void ThrowIfNullOrEmpty(string argument)
    {
        if (string.IsNullOrEmpty(argument))
        {
            throw new System.ArgumentException($"{Localizations.Strings.Program.ErrorMessages.ArgumentExceptionValueNotNull}: => {nameof(argument)}");
        }
    }
};

