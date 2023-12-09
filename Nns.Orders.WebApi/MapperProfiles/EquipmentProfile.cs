using AutoMapper;
using Azure.Core;
using Nns.Orders.Domain.Documents;
using Nns.Orders.WebApi.Models;


namespace Nns.Orders.WebApi.MapperProfiles
{
    public class EquipmentProfile : Profile
    {
        public EquipmentProfile()
        {
            CreateMap<EquipmentApplication, Equipment2WorkResponse>()
                .ForMember(dest => dest.EquipmentType,
            opt => opt.MapFrom(src => new EquipmentTypeDto { Id = src.EquipmentType.Id, Name = src.EquipmentType.Name }))
                .ForMember(dest => dest.WorkType,
            opt => opt.MapFrom(src => new WorkTypeDto { Id = src.WorkType.Id, Name = src.WorkType.Name }));

            CreateMap<CreateEquipment2WorkRequest, EquipmentApplication>()
                .ForMember(dest => dest.StartDate,
            opt => opt.MapFrom(src => src.StartDate.ToDateTime(TimeOnly.MinValue)));

            
        }
    }
}
