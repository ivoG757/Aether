using System.ComponentModel.DataAnnotations;

namespace Aether.Data.Models
{
    public class TextChannel
    {
        [Key]
        public Guid Id { get; set; }
        public int Position { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string? Icon { get; set; }  // icon shouldnt be string, but for now it is
        public string? Description { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
