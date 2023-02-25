using DataServices.Attributes;
using System.Data;
using System.Reflection;

namespace DataServices.Services;

public interface IDataService
{
    T GetObjectFromReader<T>(IDataReader reader);
    DataTable GetDataTable<T>(IEnumerable<T> data);
}

public class DataService : IDataService
{
    public T GetObjectFromReader<T>(IDataReader reader)
    {
        var retVal = (T)Activator.CreateInstance(typeof(T))!;
        foreach (var property in retVal.GetType().GetProperties())
        {
            if (!DataReaderHasColumn(reader, property.Name)) continue;
            if (reader.IsDBNull(reader.GetOrdinal(property.Name))) continue;
            property.SetValue(retVal, reader[property.Name]);
        }
        return retVal;
    }

    public DataTable GetDataTable<T>(IEnumerable<T> data)
    {
        ValidateData<T>();
        var table = GetDataTable<T>();
        PopulateDataTableRows(table, data);
        return table;
    }

    private static bool DataReaderHasColumn(IDataReader reader, string columnName)
    {
        var schemaTable = reader.GetSchemaTable()!;
        var rows = schemaTable.Rows.OfType<DataRow>();
        return rows.Any(row => row["ColumnName"].ToString() == columnName);
    }

    private static void ValidateData<T>()
    {
        var instance = (T)Activator.CreateInstance(typeof(T))!;
        var attributes = new List<DataTablePropertyAttribute>();
        foreach (var property in instance.GetType().GetProperties())
        {
            var attribute = property.GetCustomAttributes(false).OfType<DataTablePropertyAttribute>().SingleOrDefault();
            if (attribute != null)
            {
                attributes.Add(attribute);
            }
        }
        if (attributes.Any())
        {
            var hasDuplicates = attributes.GroupBy(att => att.OrderIndex).Any(grp => grp.Count() > 1);
            if (hasDuplicates)
            {
                throw new ArgumentException("Duplicate DataTablePropertyAttributes.OrderIndex found in data.");
            }
            var anyLessThanZero = attributes.Any(att => att.OrderIndex < 0);
            if (anyLessThanZero)
            {
                throw new ArgumentException("DataTablePropertyAttributes.OrderIndex less than zero found in data.");
            }
        }
        else
        {
            throw new ArgumentException("No properties decorated with DataTablePropertyAttribute found in data.");
        }
    }

    private static DataTable GetDataTable<T>()
    {
        DataTable table = new();
        var instance = Activator.CreateInstance(typeof(T))!;
        foreach (var property in GetOrderedPropertyInfos(instance))
        {
            var type = property.PropertyType;
            var name = property.Name;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type);
            }
            table.Columns.Add(name, type!);
        }

        return table;
    }

    private static void PopulateDataTableRows<T>(DataTable table, IEnumerable<T> items)
    {
        var instance = Activator.CreateInstance(typeof(T))!;
        var properties = GetOrderedPropertyInfos(instance);
        object[] values = new object[properties.Count()];
        foreach (var item in items)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = properties[i]!.GetValue(item)!;
            }
            table.Rows.Add(values);
        }
    }

    private static List<PropertyInfo> GetOrderedPropertyInfos(object? instance)
    {
        return instance!.GetType().GetProperties()
            .Where(prop => prop.GetCustomAttributes(false).OfType<DataTablePropertyAttribute>().FirstOrDefault() != null)
            .OrderBy(prop => prop.GetCustomAttributes(false).OfType<DataTablePropertyAttribute>().FirstOrDefault()?.OrderIndex)
            .ToList();
    }
}