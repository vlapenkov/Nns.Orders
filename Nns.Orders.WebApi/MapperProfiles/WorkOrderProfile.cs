using AutoMapper;
using Azure.Core;
using Nns.Orders.Domain.Documents;
using Nns.Orders.WebApi.Models;


namespace Nns.Orders.WebApi.MapperProfiles
{
    public class WorkOrderProfile : Profile
    {
        public WorkOrderProfile()
        {
            CreateMap<WorkOrder, WorkOrderResponse>()
                .ForMember(dest => dest.Excavation,
            opt => opt.MapFrom(src => new ExcavationDto { Id = src.ExcavationId, Name = src.Excavation.Name }))
                .ForMember(dest => dest.EquipmentType,
            opt => opt.MapFrom(src => new EquipmentTypeDto { Id = src.EquipmentTypeId, Name = src.EquipmentType.Name }))
                .ForMember(dest => dest.WorkType,
            opt => opt.MapFrom(src => new WorkTypeDto { Id = src.WorkTypeId, Name = src.WorkType.Name }));

            CreateMap<WorkOrder, WorkOrderShortResponse>()
              .ForMember(dest => dest.Excavation,
          opt => opt.MapFrom(src => new ExcavationDto { Id = src.ExcavationId, Name = src.Excavation.Name }));
              

            CreateMap<CreateWorkOrderRequest, WorkOrder>()
                .ForMember(dest => dest.StartDate,
            opt => opt.MapFrom(src => src.StartDate.ToDateTime(TimeOnly.MinValue)));

            
        }
    }
}
