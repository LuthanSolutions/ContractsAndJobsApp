namespace DataServices.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DataTablePropertyAttribute : Attribute
{
    public int OrderIndex { get; init; }

    public DataTablePropertyAttribute(int orderIndex)
    {
        this.OrderIndex = orderIndex;
    }
}
