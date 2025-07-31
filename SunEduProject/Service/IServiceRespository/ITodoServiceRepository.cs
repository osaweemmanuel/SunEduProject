using SunEduProject.Model.DTos.Todoitem;

namespace SunEduProject.Service.IServiceRespository
{
    public interface ITodoServiceRepository
    {
        Task<TodoItemResponseDto> CreateTodoItemAsync(CreateTodoItemRequestDto todoItemRequestDto,string userId);
        Task<List<TodoItemResponseDto>> GetAllTodoItemsAsync();
        Task<TodoItemResponseDto> GetTodoItemByIdAsync(Guid id, string userId);
        Task<bool>DeleteTodoItemAsync(Guid id, string userId);
        Task<TodoItemResponseDto> UpdateTodoItemAsync(UpdateTodoItemDto updateTodoItem, string userId);
        Task<bool>MarkCompleteTodoItemAsync(Guid id, string userId);
    }
}
