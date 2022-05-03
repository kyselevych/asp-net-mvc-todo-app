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
        
        [Range(1, int.MaxValue, ErrorMessage = "Category id must be greate than 1")]
        [Remote("IsCategoryExist", "Categories", ErrorMessage = "Category is not exist")]
        [FromForm(Name = "id")]
        public int? CategoryId { get; set; }
    }
}
