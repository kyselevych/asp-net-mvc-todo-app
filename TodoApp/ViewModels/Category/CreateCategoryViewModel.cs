using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required (ErrorMessage = "Required field")]
        [StringLength (255, ErrorMessage = "Max length of name 255 symbols")]
        public string Name { get; set; }
    }
}
