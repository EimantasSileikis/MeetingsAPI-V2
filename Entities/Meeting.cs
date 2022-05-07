using System.ComponentModel.DataAnnotations;

namespace MeetingsAPI_V2.Entities
{
    public class Meeting
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Users { get; set; } = string.Empty;
    }
}
