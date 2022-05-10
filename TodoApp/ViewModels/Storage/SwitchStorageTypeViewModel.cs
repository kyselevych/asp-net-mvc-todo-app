using TodoApp.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    
    public class SwitchStorageTypeViewModel
    {
        [RegularExpression("xml|mssql", ErrorMessage = "Storage type is invalid")]
        public string Type { get; set; }
    }
}
