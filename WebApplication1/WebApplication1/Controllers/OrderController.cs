using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.DTOs;
using WebApplication1.Data.Models;
using WebApplication1.Data.Services;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _context;

    public OrderController(OrderService context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
    {
        return await _context.GetOrders();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO?>> GetOrder(int id)
    {
        var order = await _context.GetOrder(id);

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OrderDTO>> PutOrder(int id, [FromBody] OrderDTO order)
    {
        var result = await _context.UpdateOrder(id, order);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDTO>> PostOrder([FromBody] OrderDTO norder)
    {
        var result = await _context.AddOrder(norder);

        if (result == null)
        {
            BadRequest();
        }

        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.DeleteOrder(id);

        if (order)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpGet("/incomplete")]
    public async Task<ActionResult<IEnumerable<IncompleteOrder>>> GetIncOrds()
    {
        return await _context.GetIncompleteOrd();
    }
}
