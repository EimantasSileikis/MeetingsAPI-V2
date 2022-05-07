using MeetingsAPI_V2.Entities;
using System.ComponentModel.DataAnnotations;

namespace MeetingsAPI_V2.Models
{
    public class MeetingWithUserObjDto
    {
        [Required]
        public User[]? Users { get; set; }
    }
}
