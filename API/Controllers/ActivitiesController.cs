using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        private readonly DataContext _context;
     
        /* click inside of context parameter, click ctrl + ., 
            then choose 'initialize field from parameter' to generate private readonly DataContext 
            and to assign the value inside the constructor
        */
        public ActivitiesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet] // api/activities <----this is the path to this controller
        public async Task<ActionResult<List<Activity>>>GetActivities() 
        {
            return await _context.Activities.ToListAsync();
        }

        [HttpGet("{id}")] // // api/activities/guid
        public async Task<ActionResult<Activity>> GetActivity(Guid id) // parameter name needs to match the param on line 27
        {
            return await _context.Activities.FindAsync(id);
        }
    }
}