using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega_demo.Models;
using vega_demo.Persistence;

namespace vega_demo.Controllers
{
    public class MakesController : Controller
    {
        private readonly VegaDbContext context;
        public MakesController(VegaDbContext context)
        {
            this.context = context;
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<Make>> GetMakes()
        {
            return await context.Makes.Include(m => m.Models).ToListAsync();
        }
    }
}