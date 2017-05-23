using AutoMapper;
using vega_demo.Controllers.Resources;
using vega_demo.Models;
using System.Linq;

namespace vega_demo.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to resource
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();

            // Resource to Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature { FeatureId = id})));
        }
    }
}