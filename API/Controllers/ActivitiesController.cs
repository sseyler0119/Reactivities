using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
     
        [HttpGet] // api/activities <----this is the path to this controller
        public async Task<ActionResult<List<Activity>>>GetActivities() 
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")] // // api/activities/guid
        public async Task<ActionResult<Activity>> GetActivity(Guid id) // parameter name needs to match the param on line 27
        {
            return await Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            await Mediator.Send(new Create.Command { Activity = activity });
            return Ok();
        }

        [HttpPut("{id}")] // used to update resources
        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            await Mediator.Send(new Edit.Command { Activity = activity });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            await Mediator.Send(new Delete.Command{Id = id});
            return Ok();
        }
    }
}