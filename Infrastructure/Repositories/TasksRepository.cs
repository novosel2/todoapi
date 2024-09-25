using Core.Data.Entities;
using Core.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly AppDbContext _db;

        public TasksRepository(AppDbContext db)
        {
            _db = db;
        }


        public async Task<List<TodoTask>> GetTasksAsync(string currentUser)
        {
            return await _db.Tasks.Where(t => t.CreatedBy == currentUser).ToListAsync();
        }

        public async Task<TodoTask> GetTaskByIdAsync(Guid taskId)
        {
            return await _db.Tasks.FirstAsync(t => t.Id == taskId);
        }

        public async Task<bool> TaskExistsAsync(Guid taskId)
        {
            return await _db.Tasks.AnyAsync(t => t.Id == taskId);
        }

        public async Task AddTaskAsync(TodoTask task)
        {
            await _db.Tasks.AddAsync(task);
        }

        public void CompleteTaskAsync(TodoTask task)
        {
            task.IsCompleted = true;
            _db.Tasks.Entry(task).Property(t => t.IsCompleted).IsModified = true;
        }

        public void UpdateTask(TodoTask existingTask, TodoTask updatedTask)
        {
            _db.Tasks.Entry(existingTask).CurrentValues.SetValues(updatedTask);
            _db.Tasks.Entry(existingTask).State = EntityState.Modified;
        }

        public void DeleteTaskAsync(TodoTask task)
        {
            _db.Tasks.Remove(task);
        }

        public async Task<bool> IsSavedAsync()
        {
            int saved = await _db.SaveChangesAsync();

            return saved > 0;
        }
    }
}
