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

        //public ICollection<User> Users { get; set; } = new List<User>();
    }
}
