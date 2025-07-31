using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SunEduProject.Data;
using SunEduProject.Model;
using SunEduProject.Model.DTos.Todoitem;
using SunEduProject.Service.IServiceRespository;

namespace SunEduProject.Service
{
    public class TodoService : ITodoServiceRepository
    {
        private readonly IMapper mapper;
        private readonly TodoDbContext dbContext;

        public TodoService(IMapper mapper,TodoDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<TodoItemResponseDto> CreateTodoItemAsync(CreateTodoItemRequestDto todoItemRequestDto, string userId)
        {
            var todoItem = mapper.Map<TodoItem>(todoItemRequestDto);
            todoItem.UserId = userId;
            todoItem.CreatedAt = DateTime.UtcNow;
            await dbContext.TodoItems.AddAsync(todoItem);
            await dbContext.SaveChangesAsync();
            return mapper.Map<TodoItemResponseDto>(todoItem);
            
        }

        public async Task<bool> DeleteTodoItemAsync(Guid id, string userId)
        {
            var todo = await dbContext.TodoItems.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (todo == null) return false;

            dbContext.TodoItems.Remove(todo);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<TodoItemResponseDto>> GetAllTodoItemsAsync()
        {
            var todoItems=await dbContext.TodoItems
                //.Where(t=>t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
            return mapper.Map<List<TodoItemResponseDto>>(todoItems);
         

        }

        public async Task<TodoItemResponseDto> GetTodoItemByIdAsync(Guid id, string userId)
        {
            var todoItem = await dbContext.TodoItems
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (todoItem == null)
            {
                throw new KeyNotFoundException("Todo item not found.");
            }
            return mapper.Map<TodoItemResponseDto>(todoItem);
        }

        public async Task<bool> MarkCompleteTodoItemAsync(Guid id, string userId)
        {
            var todoItem = await dbContext.TodoItems
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (todoItem == null) return false;

            todoItem.IsCompleted = true;
            dbContext.TodoItems.Update(todoItem);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<TodoItemResponseDto> UpdateTodoItemAsync(UpdateTodoItemDto updateTodoItem, string userId)
        {
           var todoItem = dbContext.TodoItems
                .FirstOrDefault(t => t.Id == updateTodoItem.Id && t.UserId == userId);
            if (todoItem == null)
            {
                throw new KeyNotFoundException("Todo item not found.");
            }
            todoItem.Title = updateTodoItem.Title;
            todoItem.Description = updateTodoItem.Description;

            dbContext.TodoItems.Update(todoItem);
            await dbContext.SaveChangesAsync();
            return mapper.Map<TodoItemResponseDto>(todoItem);
        }


    }
}
