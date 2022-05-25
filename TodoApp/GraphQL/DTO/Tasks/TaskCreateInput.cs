namespace TodoApp.GraphQL.DTO
{
    public class TaskCreateInput
    {
        public string Name { get; set; }

        public DateTime? Deadline { get; set; }

        public int? CategoryId { get; set; }
    }
}
