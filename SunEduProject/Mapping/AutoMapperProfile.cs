using AutoMapper;
using SunEduProject.Model;
using SunEduProject.Model.DTos.Todoitem;

namespace SunEduProject.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TodoItem,CreateTodoItemRequestDto>().ReverseMap();
            CreateMap<TodoItem, TodoItemResponseDto>().ReverseMap();
            CreateMap<TodoItem,UpdateTodoItemDto>().ReverseMap();

        }
    }
}
