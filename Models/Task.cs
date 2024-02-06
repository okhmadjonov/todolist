using System;
using System.ComponentModel.DataAnnotations;

namespace Todolist.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Task title is required")]
        [RegularExpression(@"^(?![0-9]+$).*$", ErrorMessage = "Task title must not contain only numbers")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Task description is required")]
        [RegularExpression(@"^(?![0-9]+$).*$", ErrorMessage = "Task description must not contain only numbers")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Task create date is required")]
       
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Task expire date is required")]
        
        public DateTime EndDate { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
