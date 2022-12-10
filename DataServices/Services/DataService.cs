using System.Data;

namespace DataServices.Services
{
    public interface IDataService
    {
        T GetObjectFromReader<T>(IDataReader reader);
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

        private static bool DataReaderHasColumn(IDataReader reader, string columnName)
        {
            var schemaTable = reader.GetSchemaTable()!;
            var rows = schemaTable.Rows.OfType<DataRow>();
            return rows.Any(row => row["ColumnName"].ToString() == columnName);
        }
    }
}