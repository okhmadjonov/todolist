namespace Todolist.Models.Dtos
{
    public class TaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
