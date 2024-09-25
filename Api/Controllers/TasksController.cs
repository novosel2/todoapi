using Core.Data.Dto.Task;
using Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tasks/")]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;
        private readonly string currentUserEmail;

        public TasksController(ITasksService tasksService, IHttpContextAccessor contextAccessor)
        {
            _tasksService = tasksService;
            currentUserEmail = contextAccessor.HttpContext!.User.Claims.First(c => c.Type == "name").Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            List<TaskResponse> taskResponses = await _tasksService.GetTasksAsync(currentUserEmail);

            return Ok(taskResponses);
        }

        [HttpPost("add-task")]
        public async Task<IActionResult> AddTask(TaskAddRequest taskAddRequest)
        {
            await _tasksService.AddTaskAsync(taskAddRequest, currentUserEmail);

            return Ok("Task successfully created.");
        }

        [HttpPut("complete-task")]
        public async Task<IActionResult> CompleteTask(Guid taskId)
        {
            await _tasksService.CompleteTaskAsync(taskId, currentUserEmail);

            return Ok("Task successfully completed.");
        }

        [HttpPut("update-task")]
        public async Task<IActionResult> UpdateTask(Guid taskId, TaskAddRequest updatedTask)
        {
            await _tasksService.UpdateTaskAsync(taskId, updatedTask, currentUserEmail);

            return Ok("Task successfully updated.");
        }

        [HttpDelete("delete-task")]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            await _tasksService.DeleteTaskAsync(taskId, currentUserEmail);

            return Ok("Task successfully deleted.");
        }
    }
}
