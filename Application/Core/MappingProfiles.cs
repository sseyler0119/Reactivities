using Application.Activities;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        /* where we want to go from and where we want to go to*/
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();

            // d = destination members, o = options, s = source
            CreateMap<Activity, ActivityDto>()
                .ForMember(d => d.HostUsername, o => o
                    .MapFrom(s => s.Attendees.FirstOrDefault(x => x.IsHost).AppUser.UserName));

            CreateMap<ActivityAttendee, AttendeeDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.AppUser.Bio))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos
                    .FirstOrDefault(x => x.IsMain).Url)); 

            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(d => d.Image, o => o.MapFrom(s=> s.Photos
                    .FirstOrDefault(x => x.IsMain).Url));
        }        
    }
}