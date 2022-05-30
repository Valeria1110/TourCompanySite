using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Data.DTOs;
using WebApplication1.Data.Models;
using WebApplication1.Data.Services;

[Route("api/[controller]")]
[ApiController]
public class TourController : ControllerBase
{
    private readonly TourService _context;

    public TourController(TourService context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<TourDTO>>> GetTours()
    {
        return await _context.GetTours();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TourDTO?>> GetTour(int id)
    {
        var tour = await _context.GetTour(id);

        if (tour == null)
        {
            return NotFound();
        }

        return tour;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TourDTO>> PutTour(int id, [FromBody] TourDTO tour)
    {
        var result = await _context.UpdateTour(id, tour);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TourDTO>> PostTour([FromBody] TourDTO tour)
    {
        var result = await _context.AddTour(tour);
        if (result == null)
        {
            BadRequest();
        }

        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTour(int id)
    {
        var tour = await _context.DeleteTour(id);
        if (tour)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpGet("/cost")]
    public async Task<ActionResult<IEnumerable<CostTour>>> GetCost()
    {
        return await _context.GetCosts();
    }

    [HttpGet("/inc")]
    public async Task<ActionResult<IEnumerable<TourDTO>>> GetIncTours()
    {
        return await _context.GetIncompleteTour();
    }
}
