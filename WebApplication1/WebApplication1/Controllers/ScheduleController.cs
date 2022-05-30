using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Data.DTOs;
using WebApplication1.Data.Models;
using WebApplication1.Data.Services;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly ScheduleService _context;

    public ScheduleController(ScheduleService context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScheduleDTO?>>> GetSchedules()
    {
        return await _context.GetSchedules();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleDTO>> GetSchedule(int id)
    {
        var schedule = await _context.GetSchedule(id);

        if (schedule == null)
        {
            return NotFound();
        }

        return schedule;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ScheduleDTO>> PutSchedule(int id, [FromBody] ScheduleDTO schedule)
    {
        var result = await _context.UpdateSchedule(id, schedule);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<ActionResult<ScheduleDTO>> PostSchedule([FromBody] ScheduleDTO nschedule)
    {
        var result = await _context.AddSchedule(nschedule);

        if (result == null)
        {
            BadRequest();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSchedule(int id)
    {
        var schedule = await _context.DeleteSchedule(id);

        if (schedule)
        {
            return Ok();
        }
        return BadRequest();
    }

    
    [HttpGet("/incompl")]
    public async Task<ActionResult<IEnumerable<ScheduleDTO>>> GetIncSchs()
    {
        return await _context.GetIncompleteSch();
    }
    
}
