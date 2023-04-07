using System;

namespace CivilizationsRoad.Scripts.Exceptions;
public class ArgumentException : Exception
{
    public ArgumentException() { }
    public ArgumentException(string message) : base(message) { }
    //public ArgumentException(string message, Exception inner) : base(message, inner) { }

    public static void ThrowIfNullOrEmpty(string argument)
    {
        if (string.IsNullOrEmpty(argument))
        {
            var message = Godot.TranslationServer.Translate(Localizations.Strings.Exceptions.Arguments.Key_ArgumentExceptionStringIsNullOrEmpty);
            
            throw new ArgumentException($"{message}: => {nameof(argument)}");
        }
    }
};

