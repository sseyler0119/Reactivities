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
        }        
    }
}