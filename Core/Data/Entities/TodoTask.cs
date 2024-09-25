using System.ComponentModel.DataAnnotations;

namespace Core.Data.Entities
{
    public class TodoTask
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public bool IsCompleted { get; set; } = false;

        public string CreatedBy { get; set; } = string.Empty;
    }
}
