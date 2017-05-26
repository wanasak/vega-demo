using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega_demo.Models;

namespace vega_demo.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public async Task<Vehicle> GetVehicle(int id, bool isIncludeRelated = true)
        {
            if (!isIncludeRelated)
                return await context.Vehicles.FindAsync(id);
            else
                return await context.Vehicles
                    .Include(v => v.Features)
                        .ThenInclude(vf => vf.Feature)
                    .Include(v => v.Model)
                        .ThenInclude(m => m.Make)
                    .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }

        private IQueryable<Vehicle> ApplyOrdering(VehicleQuery queryObj, IQueryable<Vehicle> query, Dictionary<string, Expression<Func<Vehicle, object>>> columnsMap)
        {
            if (queryObj.IsSortAscending)
                return query.OrderBy(columnsMap[queryObj.SortBy]);
            else
                return query.OrderByDescending(columnsMap[queryObj.SortBy]);
        }

        public async Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {
            var query = context.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .AsQueryable();

            if (queryObj.MakeId.HasValue)
                query = query.Where(x => x.Model.Make.Id == queryObj.MakeId);

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
                ["id"] = v => v.Id,
            };

            query = this.ApplyOrdering(queryObj, query, columnsMap);

            // if (queryObj.SortBy == "make")
            //     query = (queryObj.IsSortAscending) ? query.OrderBy(x => x.Model.Make.Name) : query.OrderByDescending(x => x.Model.Make.Name);  
            // if (queryObj.SortBy == "model")
            //     query = (queryObj.IsSortAscending) ? query.OrderBy(x => x.Model.Name) : query.OrderByDescending(x => x.Model.Name);
            // if (queryObj.SortBy == "contactName")
            //     query = (queryObj.IsSortAscending) ? query.OrderBy(x => x.ContactName) : query.OrderByDescending(x => x.ContactName);
            // if (queryObj.SortBy == "id")
            //     query = (queryObj.IsSortAscending) ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);

            return await query.ToListAsync();
        }
    }
}