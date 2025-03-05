namespace TasksProgramAPI.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Priority { get; set; } = "בינונית";
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = "ממתינה";
    }
}