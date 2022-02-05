namespace OrderManager.ApplicationCore.Infrastructure.Attributes;

/// <summary>
/// A class marked with this attribute has properties/fields which can be expanded
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ExpandableModel : Attribute { }