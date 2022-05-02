using AutoMapper;
using MeetingsAPI_V2.Entities;
using MeetingsAPI_V2.Models;
using MeetingsAPI_V2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Text.Json;

namespace MeetingsAPI_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;
        //private readonly string _url = "http://contacts:5000/contacts/";
        private readonly string _url = "http://localhost/contacts/";
        static readonly HttpClient client = new HttpClient();

        public MeetingsController(IMeetingRepository meetingRepository,
            IMapper mapper)
        {
            _meetingRepository = meetingRepository ?? throw new ArgumentNullException(nameof(meetingRepository));
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetingGetDto>>> GetMeetings()
        {
            var meetingEntities = await _meetingRepository.GetMeetingsAsync();
            ICollection<MeetingGetDto> meetingList = new List<MeetingGetDto>();

            foreach (var meeting in meetingEntities)
            {
                var users = JsonConvert.DeserializeObject<User[]>("[" + meeting.Users + "]");
                MeetingGetDto meetingDto = new MeetingGetDto()
                {
                    Id = meeting.Id,
                    Name = meeting.Name,
                    Users = users
                };
                meetingList.Add(meetingDto);
            }

            return Ok(meetingList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meeting>> GetMeeting(int id)
        {
            var meeting = await _meetingRepository.GetMeetingAsync(id);

            if (meeting == null)
            {
                return NotFound();
            }

            var users = JsonConvert.DeserializeObject<User[]>("[" + meeting.Users + "]");

            MeetingGetDto meetingDto = new MeetingGetDto()
            {
                Id = meeting.Id,
                Name = meeting.Name,
                Users = users
            }; 

            return Ok(meetingDto);
        }

        [HttpPost]
        public async Task<ActionResult<Meeting>> CreateMeeting(MeetingDto meetingDto)
        {
            var meeting = _mapper.Map<Meeting>(meetingDto);

            _meetingRepository.AddMeetingAsync(meeting);
            await _meetingRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMeeting), new { id = meeting.Id }, meeting);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMeeting(int id, MeetingDto meetingDto)
        {
            var meeting = await _meetingRepository.GetMeetingAsync(id);

            if (meeting == null)
            {
                return NotFound();
            }

            _mapper.Map(meetingDto, meeting);

            await _meetingRepository.SaveChangesAsync();

            return Ok(meeting);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeeting(int id)
        {
            var meeting = await _meetingRepository.GetMeetingAsync(id);

            if (meeting == null)
            {
                return NotFound();
            }

            await _meetingRepository.DeleteMeetingAsync(id);
            await _meetingRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/Users/{userId}")]
        public async Task<ActionResult<User>> AddUserToMeeting(int id, int userId)
        {
            var meeting = await _meetingRepository.GetMeetingAsync(id);

            if (meeting == null)
            {
                return NotFound();
            }
            User user = null;

            try
            {
                string responseBody = await client.GetStringAsync(_url + userId);
                user = JsonConvert.DeserializeObject<User>(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
            
            if(user == null)
            {
                return NotFound();
            }
            if(meeting.Users == null || meeting.Users == string.Empty)
            {
                meeting.Users += (JsonConvert.SerializeObject(user));
            }
            else
            {
                meeting.Users += ("," + JsonConvert.SerializeObject(user));
            }
            await _meetingRepository.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{id}/Users/{userId}")]
        public async Task<ActionResult> RemoveUserFromMeeting(int id, int userId)
        {
            var meeting = await _meetingRepository.GetMeetingAsync(id);

            if (meeting == null)
            {
                return NotFound();
            }

            var users = JsonConvert.DeserializeObject<User[]>("[" + meeting.Users + "]");
            if(users != null)
            {
                users = users.Where(user => user.Id != userId).ToArray();
                meeting.Users = JsonConvert.SerializeObject(users);
                meeting.Users = meeting.Users.Substring(1, meeting.Users.Length - 2);
                await _meetingRepository.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
