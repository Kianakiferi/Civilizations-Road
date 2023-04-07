using System;

namespace CivilizationsRoad.Scripts.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ConfigurationAttribute : Attribute
{
    public string Key { get; set; }
}
