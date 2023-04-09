using System;

namespace CivilizationsRoad.Models;

public class Asset : IEquatable<Asset>
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

    public bool Equals(Asset other)
    {
        if (other is null)
        {
            return false;
        }
        return this.Key == other.Key;
    }
    public override bool Equals(object obj)
    {
        var asset = obj as Asset;
        if (asset is null)
        {
            return false;
        }

        return this.Key == asset.Key;
    }
    public static bool operator ==(Asset asset1, Asset asset2)
    {
        if (ReferenceEquals(asset1, asset2))
        {
            return true;
        }
        if (asset1 is null || asset2 is null)
        {
            return false;
        }
        return asset1.Equals(asset2);
    }
    public static bool operator !=(Asset asset1, Asset asset2)
    {
        return !(asset1 == asset2);
    }
    
    public override int GetHashCode()
    {
        // using (var sha256Hash = System.Security.Cryptography.SHA256.Create())
        // {
        //     string hash = Scripts.Common.Utils.GetHash (sha256Hash, Key);

        //     return hash;
        // }
        return base.GetHashCode();
    }
    private string _key;
    private string _path;

    
}
