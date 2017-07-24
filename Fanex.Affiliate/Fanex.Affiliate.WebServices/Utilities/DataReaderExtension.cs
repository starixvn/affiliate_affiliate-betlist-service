namespace Fanex.Affiliate.WebServices.Utilities
{
    using System;
    using System.Data;

    public static class DataReaderExtension
    {
        public static string GetStringOrDefault(this IDataReader reader, string column, string defaultValue = "")
        {
            return reader[column] != DBNull.Value ? reader[column].ToString() : defaultValue;
        }

        public static string GetStringOrDefaultSafely(this IDataReader reader, string column, string defaultValue = "")
        {
            return reader.HasValueOfColumn(column) ? reader[column].ToString() : defaultValue;
        }

        public static int? GetNullableInt(this IDataReader reader, string column)
        {
            return reader[column] != DBNull.Value ? Convert.ToInt32(reader[column]) : new int?();
        }

        public static int? GetNullableIntSafely(this IDataReader reader, string column)
        {
            return reader.HasValueOfColumn(column) ? Convert.ToInt32(reader[column]) : new int?();
        }

        public static int GetInt(this IDataReader reader, string column, int defaultValue = 0)
        {
            return reader[column] != DBNull.Value ? Convert.ToInt32(reader[column]) : defaultValue;
        }

        public static int GetIntSafely(this IDataReader reader, string column, int defaultValue = 0)
        {
            return reader.HasValueOfColumn(column) ? Convert.ToInt32(reader[column]) : defaultValue;
        }

        public static bool HasValueOfColumn(this IDataReader reader, string columnName)
        {
            return ColumnExisted(reader, columnName) && reader[columnName] != DBNull.Value;
        }

        public static bool ColumnExisted(this IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}