using System.ComponentModel.DataAnnotations;

namespace MeetingsAPI_V2.Models
{
    public class MeetingDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

    }
}
