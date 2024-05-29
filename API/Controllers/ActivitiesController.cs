using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    private readonly ApplicationDbContext _context;

    public ActivitiesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet] // api/activities
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await _context.Activities.ToListAsync();
    }

    [HttpGet("{id}")] //api/activities/0eeb91c5-956c-4cdf-bb0e-02d2ec4d8c1f
    public async Task<ActionResult<Activity?>> GetActivity(Guid id)
    {
        return await _context.Activities.FindAsync(id);
    }
}