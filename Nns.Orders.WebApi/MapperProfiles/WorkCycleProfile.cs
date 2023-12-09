using AutoMapper;
using Azure.Core;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Domain.Entities;
using Nns.Orders.WebApi.Models;


namespace Nns.Orders.WebApi.MapperProfiles
{
    public class WorkCycleProfile : Profile
    {
        public WorkCycleProfile()
        {
            CreateMap<WorkCycle, WorkCycleResponse>()
                .ForMember(dest => dest.WorkType,
            opt => opt.MapFrom(src => new WorkTypeDto { Id = src.WorkType.Id, Name = src.WorkType.Name }));
                

            CreateMap<CreateWorkCycleRequest, WorkCycle>()
                .ForMember(dest => dest.StartDate,
            opt => opt.MapFrom(src => src.StartDate.ToDateTime(TimeOnly.MinValue)));

            
        }
    }
}
