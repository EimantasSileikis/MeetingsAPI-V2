using MeetingsAPI_V2.Entities;

namespace MeetingsAPI_V2.Models
{
    public class MeetingGetDto
    {
        public int Id { get; init; }

        public string Name { get; set; } = string.Empty;

        public User[]? Users { get; set; }
    }
}
