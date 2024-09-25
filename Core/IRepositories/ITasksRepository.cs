using Core.Data.Entities;

namespace Core.IRepositories
{
    public interface ITasksRepository
    {
        /// <summary>
        /// Get all tasks based on current user
        /// </summary>
        /// <param name="currentUser">Current user for filtering tasks</param>
        /// <returns>List of all tasks</returns>
        public Task<List<TodoTask>> GetTasksAsync(string currentUser);

        /// <summary>
        /// Get a task by id
        /// </summary>
        /// <param name="taskId">Id of task we want to get</param>
        /// <returns>Task with specified id</returns>
        public Task<TodoTask> GetTaskByIdAsync(Guid taskId);

        /// <summary>
        /// Checks if task id exists
        /// </summary>
        /// <param name="taskId">Id we want to check</param>
        /// <returns>True if exists, false if not</returns>
        public Task<bool> TaskExistsAsync(Guid taskId);

        /// <summary>
        /// Add task to database
        /// </summary>
        /// <param name="task">Task we want to add</param>
        public Task AddTaskAsync(TodoTask task);

        /// <summary>
        /// Checks the specified task as completed
        /// </summary>
        /// <param name="task">Task we want to complete</param>
        public void CompleteTaskAsync(TodoTask task);

        /// <summary>
        /// Update existing task with updated information
        /// </summary>
        /// <param name="existingTask">Task we want to update</param>
        /// <param name="updatedTask">Updated information</param>
        public void UpdateTask(TodoTask existingTask, TodoTask updatedTask);

        /// <summary>
        /// Delete task from database
        /// </summary>
        /// <param name="task">Task we want to delete</param>
        public void DeleteTaskAsync(TodoTask task);

        /// <summary>
        /// Check if any changes are made
        /// </summary>
        /// <returns>True if changes are made, false if not</returns>
        public Task<bool> IsSavedAsync();
    }
}
