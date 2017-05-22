using AutoMapper;
using vega_demo.Controllers.Resources;
using vega_demo.Models;

namespace vega_demo.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
        }
    }
}