namespace Data.Shared.Extensions;

public static class ModelBuilderExtensions
{
    public static void NamesToSnakeCase(this ModelBuilder modelBuilder)
    {
        foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        {
            string tableName = entity.GetTableName()!;
            entity.SetTableName(tableName.ToLower());

            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableProperty property in entity.GetProperties())
            {
                string colName = property.GetColumnName();
                property.SetColumnName(colName.ToLower());
            }
        }
    }

    public static void ConfigurePostgresFields(this ModelBuilder modelBuilder)
    {
        Dictionary<string, string> types = new Dictionary<string, string>
            {
                {"DATETIME", "TIMESTAMP" },
                {"FLOAT", "NUMERIC"},
                {"BIT", "BOOLEAN" }
            };

        foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableProperty property in entity.GetProperties())
                if (types.ContainsKey(property.GetColumnType() ?? ""))
                    property.SetColumnType(types[property.GetColumnType() ?? ""]);
    }

    public static ValueConverter<bool, BitArray> GetBoolToBitArrayConverter()
    {
        return new ValueConverter<bool, BitArray>(
            value => new BitArray(new[] { value }),
            value => value.Get(0));
    }
}