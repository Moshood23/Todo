using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class TodoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.DateTime)]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }
    }
}