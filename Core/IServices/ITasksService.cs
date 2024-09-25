using Core.Data.Dto.Task;

namespace Core.IServices
{
    public interface ITasksService
    {
        /// <summary>
        /// Adds a task to database
        /// </summary>
        /// <param name="taskAddRequest">Task we want to add</param>
        /// <param name="currentUser">Current user email</param>
        public Task AddTaskAsync(TaskAddRequest taskAddRequest, string currentUser);

        /// <summary>
        /// Get list of tasks based on current user
        /// </summary>
        /// <param name="currentUser">Current user for filtering tasks</param>
        /// <returns>List of task responses</returns>
        public Task<List<TaskResponse>> GetTasksAsync(string currentUser);

        /// <summary>
        /// Get task by id
        /// </summary>
        /// <param name="taskId">Id of task we want to get</param>
        /// <returns>Task response</returns>
        public Task<TaskResponse> GetTaskByIdAsync(Guid taskId);

        /// <summary>
        /// Complete specified task
        /// </summary>
        /// <param name="taskId">Id of task we want to complete</param>
        public Task CompleteTaskAsync(Guid taskId, string currentUser);

        /// <summary>
        /// Update task with new information
        /// </summary>
        /// <param name="updatedTask">Updated information</param>
        public Task UpdateTaskAsync(Guid taskId, TaskAddRequest updatedTask, string currentUser);

        /// <summary>
        /// Delete task with specified id
        /// </summary>
        /// <param name="taskId">Id of task we want to delete</param>
        public Task DeleteTaskAsync(Guid taskId, string currentUser);
    }
}
