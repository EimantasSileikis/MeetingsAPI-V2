using MeetingsAPI_V2.Entities;

namespace MeetingsAPI_V2.Models
{
    public class MeetingGetDto
    {
        public int Id { get; init; }

        public string Name { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
