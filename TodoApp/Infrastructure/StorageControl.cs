using TodoApp.Enums;

namespace TodoApp.Infrastructure
{
    public class StorageControl
    {
        public StorageType Type { get; set; } = StorageType.Mssql;
    }
}
