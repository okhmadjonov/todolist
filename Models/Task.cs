using System.ComponentModel.DataAnnotations;

namespace Todolist.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
