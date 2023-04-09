using System;

namespace CivilizationsRoad.Scripts.Exceptions.Assets;

public class ResourceLoadFailException : Exception
{
    public ResourceLoadFailException()
    {
    }

    public ResourceLoadFailException(string message) : base(message)
    {
    }
}
