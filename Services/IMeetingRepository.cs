using MeetingsAPI_V2.Entities;

namespace MeetingsAPI_V2.Services
{
    public interface IMeetingRepository
    {
        Task<IEnumerable<Meeting>> GetMeetingsAsync();
        Task<Meeting?> GetMeetingAsync(int meetingId);
        Task<bool> SaveChangesAsync();
        Task<bool> MeetingExistAsync(int meetingId);
        void AddMeetingAsync(Meeting meeting);
        Task DeleteMeetingAsync(int meetingId);
    }
}
