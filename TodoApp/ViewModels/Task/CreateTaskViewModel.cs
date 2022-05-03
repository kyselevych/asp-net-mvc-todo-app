using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public class CreateTaskViewModel
    {
        [Required (ErrorMessage = "Required field")]
        [StringLength (255, ErrorMessage = "Max name length 255 symbols")]
        public string Name { get; set; }

        public DateTime? Deadline { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Category not found")]
        public int? CategoryId { get; set; }
    }
}
