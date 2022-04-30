using AutoMapper;
using MeetingsAPI_V2.Entities;
using MeetingsAPI_V2.Models;
using MeetingsAPI_V2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingsAPI_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IMapper _mapper;

        public MeetingsController(IMeetingRepository meetingRepository,
            IMapper mapper)
        {
            _meetingRepository = meetingRepository ?? throw new ArgumentNullException(nameof(meetingRepository));
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meeting>>> GetMeetings()
        {
            var meetingEntities = await _meetingRepository.GetMeetingsAsync();

            return Ok(meetingEntities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meeting>> GetMeeting(int id)
        {
            var meeting = await _meetingRepository.GetMeetingAsync(id);

            if (meeting == null)
            {
                return NotFound();
            }

            return Ok(meeting);
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
    }
}
