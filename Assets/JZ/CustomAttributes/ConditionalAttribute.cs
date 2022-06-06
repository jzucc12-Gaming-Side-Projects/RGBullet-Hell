using UnityEngine;

/// <summary>
/// Changes attribute depending upon specified conditions
/// </summary>
public abstract class ConditionalAttribute : PropertyAttribute
{
    public readonly string variableName;
    public readonly float comparisonValue;
    public readonly ComparisonType comparisonType;
    public bool isComparison => comparisonType != ComparisonType.None;

    public ConditionalAttribute(string variableName)
    {
        this.variableName = variableName;
        comparisonType = ComparisonType.None;
    }

    public ConditionalAttribute(string variableName, ComparisonType comparisonType, float comparisonValue = 0)
    {
        this.variableName = variableName;
        this.comparisonValue = comparisonValue;
        this.comparisonType = comparisonType;
    }
}

public enum ComparisonType
{
    None,
    greaterThan,
    lessThan,
    EqualTo
}

/// <summary>
/// Attribute is hidden unless condition is met
/// </summary>
public class ShowIfAttribute : ConditionalAttribute
{
    public ShowIfAttribute(string variableName) : base(variableName)
    {
    }

    public ShowIfAttribute(string variableName, ComparisonType comparisonType, float comparisonValue) : base(variableName, comparisonType, comparisonValue)
    {
    }
}

/// <summary>
/// Attribute is visible unless condition is met
/// </summary>
public class HideIfAttribute : ShowIfAttribute
{
    public HideIfAttribute(string variableName) : base(variableName)
    {
    }

    public HideIfAttribute(string variableName, ComparisonType comparisonType, float comparisonValue) : base(variableName, comparisonType, comparisonValue)
    {
    }
}