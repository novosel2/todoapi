using Core.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Data.Dto.Task
{
    public class TaskAddRequest
    { 
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public TodoTask ToTask(string currentUser)
        {
            return new TodoTask
            {
                Title = Title,
                Description = Description,
                CreatedBy = currentUser
            };
        }
    }
}
