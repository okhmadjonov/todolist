namespace Todolist.Models
{
    public class FilterViewModel
    {
        public Priority? Priority { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
