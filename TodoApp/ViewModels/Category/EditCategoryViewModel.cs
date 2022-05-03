using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public class EditCategoryViewModel
    {
        [Required (ErrorMessage = "Required field")]
        [Range (1, int.MaxValue)]
        public int Id { get; set; }

        [Required (ErrorMessage = "Required field")]
        [StringLength (255, ErrorMessage = "Max length of name 255 symbols")]
        public string Name { get; set; }
    }
}
