using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SunEduProject.Model;
using SunEduProject.Model.DTos.Todoitem;
using SunEduProject.Service.IServiceRespository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SunEduProject.Controllers
{
    [Authorize]
    [Route("api/Todo")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoServiceRepository todoService;
        private readonly UserManager<ApplicationUser> userManager;

        public TodoItemController(ITodoServiceRepository todoService,UserManager<ApplicationUser> userManager)
        {
            this.todoService = todoService;
            this.userManager = userManager;
            ;
        }

        //GETUserid
        [HttpGet("All")]
        public async Task<IActionResult> GetTodoItems()
        {
            //var user = await userManager.GetUserAsync(User);
            //if (user == null)
            //{
            //    return Unauthorized("User is not authenticated.");
            //}
            var todoItems = await todoService.GetAllTodoItemsAsync();
            return Ok(todoItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItemById(Guid id)
        {
            var user=await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("User is not authenticated.");
            }
            try
            {
                var todoItem = await todoService.GetTodoItemByIdAsync(id, user.Id);
                return Ok(todoItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Todo item not found.");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTodoItem([FromBody] CreateTodoItemRequestDto createTodoItemRequestDto)
        {
            if (createTodoItemRequestDto == null)
            {
                return BadRequest("Todo item request cannot be null.");
            }
            var user = await userManager.GetUserAsync(User);
         

            if (user == null)
            {
                return Unauthorized("User is not authenticated.");
            }
            var createdTodoItem = await todoService.CreateTodoItemAsync(createTodoItemRequestDto, user.Id);
            return CreatedAtAction(nameof(GetTodoItemById), new { id = createdTodoItem.Id }, createdTodoItem);

        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            var user= await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("User is not authenticated.");
            }
            try
            {
                await todoService.DeleteTodoItemAsync(id, user.Id);
                return Ok(new { Message = "Todo item deleted successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Todo item not found.");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTodoItem([FromRoute] Guid id, [FromBody] UpdateTodoItemDto updateTodoItem)
        {
            if (updateTodoItem == null)
            {
                return BadRequest("Update request cannot be null.");
            }
            var user= await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("User is not authenticated.");
            }
            try
            {
                var updatedTodoItem = await todoService.UpdateTodoItemAsync(updateTodoItem, user.Id);
                return Ok(updatedTodoItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Todo item not found.");
            }
        }

        [HttpPut("Complete/{id}")]
        public async Task<IActionResult> MarkCompleteTodoItem(Guid id)
        {
            var user = await userManager.GetUserAsync(User); ;
            if (user == null)
            {
                return Unauthorized("User is not authenticated.");
            }
            var result = await todoService.MarkCompleteTodoItemAsync(id, user.Id);
            if (!result)
            {
                return NotFound("Todo item not found or already completed.");
            }
            return Ok(new
            {
                result = true,
                Message = "Todo item marked as completed successfully.",
              
            });
        }

    }
}
