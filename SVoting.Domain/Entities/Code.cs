using System.ComponentModel.DataAnnotations;

namespace SVoting.Domain.Entities
{
    public class Code
    {
        [Key]
        public string Identifier { get; set; } = string.Empty;
        public Guid PollId { get; set; }
        public Poll? Poll { get; set; }
    }
}
