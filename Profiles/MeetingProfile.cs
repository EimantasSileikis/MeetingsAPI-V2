using AutoMapper;

namespace MeetingsAPI_V2.Profiles
{
    public class MeetingProfile: Profile
    {
        public MeetingProfile()
        {
            CreateMap<Entities.Meeting, Models.MeetingDto>();
            CreateMap<Models.MeetingDto, Entities.Meeting>();
        }
    }
}
