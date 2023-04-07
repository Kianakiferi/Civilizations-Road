using System;

namespace CivilizationsRoad.Scripts.Models;

public class Asset
{
    public string Key
    {
        get { return _key; }
        set
        {
            Scripts.Exceptions.ArgumentException.ThrowIfNullOrEmpty(value);
            _key = value;
        }
    }

    public string Path
    {
        get { return _path; }
        set
        {
            Scripts.Exceptions.ArgumentException.ThrowIfNullOrEmpty(value);
            _path = value;
        }
    }

    private string _key;
    private string _path;
}
