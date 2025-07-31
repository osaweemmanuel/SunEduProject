namespace SunEduProject.Model.DTos.Todoitem
{
    public class TodoItemResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; } = string.Empty;
        // Additional properties can be added as needed
    }
}
