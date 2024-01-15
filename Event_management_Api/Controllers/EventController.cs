using Event_management_Api.Dto;
using Event_Management_System.Contexts;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly EventDbContext _context;

    public EventController(EventDbContext context)
    {
        _context = context;
    }
    [HttpGet("odata")]
    [EnableQuery]
    public IQueryable<Event> GetODataJobs()
    {
        return _context.Events;
    }
    // GET: api/Events
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
    {
        return await _context.Events.ToListAsync();
    }

    // GET: api/Events/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
        var @event = await _context.Events.FindAsync(id);

        if (@event == null)
        {
            return NotFound();
        }

        return @event;
    }

    // POST: api/Events
    [HttpPost]
    public async Task<ActionResult<Event>> PostEvent(EventDTO eventDTO)
    {
        var @event = new Event
        {
            OrganizerId = eventDTO.OrganizerId,
            Title = eventDTO.Title,
            Description = eventDTO.Description,
            Date = eventDTO.Date,
            Time = eventDTO.Time,
            Location = eventDTO.Location,
            CategoryId = eventDTO.CategoryId
            // You can set other properties as needed
        };

        _context.Events.Add(@event);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEvent), new { id = @event.Id }, @event);
    }

    // PUT: api/Events/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEvent(int id, EventDTO eventDTO)
    {
        var @event = await _context.Events.FindAsync(id);

        if (@event == null)
        {
            return NotFound();
        }

        @event.OrganizerId = eventDTO.OrganizerId;
        @event.Title = eventDTO.Title;
        @event.Description = eventDTO.Description;
        @event.Date = eventDTO.Date;
        @event.Time = eventDTO.Time;
        @event.Location = eventDTO.Location;
        @event.CategoryId = eventDTO.CategoryId;
        // You can update other properties as needed

        _context.Entry(@event).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EventExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Events/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var @event = await _context.Events.FindAsync(id);
        if (@event == null)
        {
            return NotFound();
        }

        _context.Events.Remove(@event);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EventExists(int id)
    {
        return _context.Events.Any(e => e.Id == id);
    }
}
