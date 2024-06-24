using Application.Activities;
using Application.Core;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    [HttpGet] // api/activities
    public async Task<IActionResult> GetActivities([FromQuery] ActivityParams activityParams)
    {
        return HandlePagedResult(await Mediator.Send(new List.Query { Params = activityParams }));
    }

    [HttpGet("{id}")] //api/activities/fffffffbgdjhkhfdgfjhj
    public async Task<IActionResult> GetActivity(Guid id)
    {
        return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
    }

    [HttpPost] // api/activities
    public async Task<IActionResult> CreateActivity(Activity activity)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Activity = activity }));
    }

    [Authorize(Policy = "IsActivityHost")]
    [HttpPut("{id}")] //api/activities/fngmbnfmbfndm
    public async Task<IActionResult> EditActivity(Guid id, Activity activity)
    {
        activity.Id = id;
        return HandleResult(await Mediator.Send(new Edit.Command { Activity = activity }));
    }

    [Authorize(Policy = "IsActivityHost")]
    [HttpDelete("{id}")] // api/activities/mdfnmbdf
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }

    [HttpPost("{id}/attend")]
    public async Task<IActionResult> Attend(Guid id)
    {
        return HandleResult(await Mediator.Send(new UpdateAttendance.Command { Id = id }));
    }
}