using Core.Data.Entities;

namespace Core.Data.Dto.Task
{
    public class TaskResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }

    public static class TodoTaskExtension
    {
        public static TaskResponse ToTaskResponse(this TodoTask task)
        {
            return new TaskResponse()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted
            };
        }
    }
}
