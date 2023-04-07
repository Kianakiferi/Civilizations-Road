using System;

namespace CivilizationsRoad.Scripts.Exceptions.Assets;

public class ResourceNotFoundOrLoadedException : Exception
{
    public ResourceNotFoundOrLoadedException() { }
    public ResourceNotFoundOrLoadedException(string message) : base(message) { }
}
