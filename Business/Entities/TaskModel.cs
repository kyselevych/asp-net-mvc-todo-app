namespace Business.Entities
{
    public class TaskModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDone { get; set; }

        public DateTime? Deadline { get; set; }

        public DateTime? DateExecution { get; set; }

        public int? CategoryId { get; set; }
        
        public CategoryModel? Category { get; set; }
    }
}
