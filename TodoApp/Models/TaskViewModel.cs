namespace TodoApp.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? DateExecution { get; set; }
        public int? CategoryId { get; set; }
    }
}
