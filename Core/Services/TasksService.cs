using Core.Data.Dto.Task;
using Core.Data.Entities;
using Core.Exceptions;
using Core.IRepositories;
using Core.IServices;

namespace Core.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepo;

        public TasksService(ITasksRepository tasksRepo)
        {
            _tasksRepo = tasksRepo;
        }

        public async Task<List<TaskResponse>> GetTasksAsync(string currentUser)
        {
            List<TaskResponse> taskResponses = ( await _tasksRepo.GetTasksAsync(currentUser) ).Select(t => t.ToTaskResponse()).ToList();

            return taskResponses;
        }

        public async Task<TaskResponse> GetTaskByIdAsync(Guid taskId)
        {
            if (! await _tasksRepo.TaskExistsAsync(taskId))
            {
                throw new NotFoundException($"Task Not Found, ID: {taskId}");
            }

            TaskResponse taskResponse = ( await _tasksRepo.GetTaskByIdAsync(taskId) ).ToTaskResponse();

            return taskResponse;
        }

        public async Task CompleteTaskAsync(Guid taskId, string currentUser)
        {
            if (!await _tasksRepo.TaskExistsAsync(taskId))
            {
                throw new NotFoundException($"Task Not Found, ID: {taskId}");
            }

            TodoTask task = await _tasksRepo.GetTaskByIdAsync(taskId);

            if (task.CreatedBy != currentUser)
            {
                throw new ForbiddenException("Task not created by current user.");
            }

            _tasksRepo.CompleteTaskAsync(task);

            if (! await _tasksRepo.IsSavedAsync())
            {
                throw new UpdatingFailedException("Updating task failed.");
            }
        }

        public async Task UpdateTaskAsync(Guid taskId, TaskAddRequest updatedTask, string currentUser)
        {
            if (! await _tasksRepo.TaskExistsAsync(taskId))
            {
                throw new NotFoundException($"Task not found, ID: {taskId}");
            }

            var existingTask = await _tasksRepo.GetTaskByIdAsync(taskId);
            var taskUpdated = updatedTask.ToTask(currentUser);

            taskUpdated.IsCompleted = existingTask.IsCompleted;
            taskUpdated.Id = taskId;

            if (existingTask.CreatedBy != currentUser)
            {
                throw new ForbiddenException("Task not created by current user.");
            }

            _tasksRepo.UpdateTask(existingTask, taskUpdated);

            if (! await _tasksRepo.IsSavedAsync())
            {
                throw new UpdatingFailedException("Updating task failed.");
            }
        }

        public async Task AddTaskAsync(TaskAddRequest taskAddRequest, string currentUser)
        {
            TodoTask task = taskAddRequest.ToTask(currentUser);

            await _tasksRepo.AddTaskAsync(task);

            if (! await _tasksRepo.IsSavedAsync())
            {
                throw new CreationFailedException("Creating task failed.");
            }
        }

        public async Task DeleteTaskAsync(Guid taskId, string currentUser)
        {
            if (! await _tasksRepo.TaskExistsAsync(taskId))
            {
                throw new NotFoundException($"Task not found, ID: {taskId}");
            }

            TodoTask task = await _tasksRepo.GetTaskByIdAsync(taskId);

            if (task.CreatedBy != currentUser)
            {
                throw new ForbiddenException("Task not created by current user.");
            }

            _tasksRepo.DeleteTaskAsync(task);

            if (! await _tasksRepo.IsSavedAsync())
            {
                throw new SaveChangesException("Deleting task failed.");
            }
        }
    }
}
