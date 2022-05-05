using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public class CreateTaskViewModel
    {
        [Required (ErrorMessage = "Required field")]
        [StringLength (255, ErrorMessage = "Max name length 255 symbols")]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Deadline { get; set; }
        
        public int? CategoryId { get; set; }
    }
}
