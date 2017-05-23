using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega_demo.Controllers.Resources;
using vega_demo.Models;
using vega_demo.Persistence;

namespace vega_demo.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        public FeaturesController(VegaDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await context.Features.ToListAsync();
            return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);
        }
    }
}