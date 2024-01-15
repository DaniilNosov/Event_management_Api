using Event_management_Api.Dto;
using Event_Management_System.Contexts;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ParticipantController : ControllerBase
{
    private readonly EventDbContext _context;

    public ParticipantController(EventDbContext context)
    {
        _context = context;
    }

    // GET: api/Participants
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Participant>>> GetParticipants()
    {
        return await _context.Participants.ToListAsync();
    }

    // GET: api/Participants/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Participant>> GetParticipant(int id)
    {
        var participant = await _context.Participants.FindAsync(id);

        if (participant == null)
        {
            return NotFound();
        }

        return participant;
    }

    // POST: api/Participants
    [HttpPost]
    public async Task<ActionResult<Participant>> PostParticipant(ParticipantDTO participantDTO)
    {
        var participant = new Participant
        {
            EventId = participantDTO.EventId,
            UserId = participantDTO.UserId
            // You can set other properties as needed
        };

        _context.Participants.Add(participant);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetParticipant), new { id = participant.Id }, participant);
    }

    // PUT: api/Participants/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutParticipant(int id, ParticipantDTO participantDTO)
    {
        var participant = await _context.Participants.FindAsync(id);

        if (participant == null)
        {
            return NotFound();
        }

        participant.EventId = participantDTO.EventId;
        participant.UserId = participantDTO.UserId;
        // You can update other properties as needed

        _context.Entry(participant).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ParticipantExists(id))
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

    // DELETE: api/Participants/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParticipant(int id)
    {
        var participant = await _context.Participants.FindAsync(id);
        if (participant == null)
        {
            return NotFound();
        }

        _context.Participants.Remove(participant);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ParticipantExists(int id)
    {
        return _context.Participants.Any(e => e.Id == id);
    }
}
