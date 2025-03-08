using System;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class AnomalieAttribute : Attribute
{
    public string Name { get; }

    public AnomalieAttribute(string name)
    {
        Name = name;
    }
}