using MeetingsAPI_V2.Data;
using MeetingsAPI_V2.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingsAPI_V2.Services
{
    public class MeetingRepository: IMeetingRepository
    {
        private readonly DataContext _context;

        public MeetingRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Meeting>> GetMeetingsAsync()
        {
            return await _context.Meetings.ToListAsync();
        }

        public async Task<Meeting?> GetMeetingAsync(int meetingId)
        {
            return await _context.Meetings.Where(m => m.Id == meetingId).FirstOrDefaultAsync();
        }



        public void AddMeetingAsync(Meeting meeting)
        {
            _context.Add(meeting);
        }

        public async Task<bool> MeetingExistAsync(int meetingId)
        {
            return await _context.Meetings.AnyAsync(meeting => meeting.Id == meetingId);
        }

        public async Task DeleteMeetingAsync(int meetingId)
        {
            var meeting = await GetMeetingAsync(meetingId);
            if (meeting != null)
            {
                _context.Remove(meeting);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
