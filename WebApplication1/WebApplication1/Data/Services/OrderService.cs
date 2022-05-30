using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.DTOs;
using WebApplication1.Data.Models;

namespace WebApplication1.Data.Services;

//Создание базовой реализации основных операций с данными. Данные операции называются CRUD(Create, Read, Update, Delete).
public class OrderService
{
    private TourContext _context;
    public OrderService(TourContext context)
    {
        _context = context;
    }
    public async Task<OrderDTO?> AddOrder(OrderDTO order)
    {
        var schedule = await _context.Schedules.FirstOrDefaultAsync(sch => sch.IdS == order.IdS);

        if (schedule == null)
            return null;
        Order norder = new Order
        {
            FName = order.FName,
            LName = order.LName,
            MName = order.MName,
            Age= order.Age,
            Phone = order.Phone,
            Email = order.Email,
            Image = order.Image,
            Schedule=schedule,
        };
        var result = _context.Orders.Add(norder);
        await _context.SaveChangesAsync();
        order.IdOrder = result.Entity.IdOrder;
        return await Task.FromResult(order);
    }

    public async Task<OrderDTO?> GetOrder(int id)
    {
        var order = await _context.Orders.Include(a=>a.Schedule).FirstOrDefaultAsync(o => o.IdOrder== id);
        if (order == null)
            return null;

        var orderDTO = new OrderDTO
        {
            IdOrder = order.IdOrder,
            FName = order.FName,
            LName= order.LName,
            MName= order.MName,
            Age= order.Age,
            Phone= order.Phone,
            Email= order.Email,
            Image= order.Image,
            IdS=order.Schedule.IdS,
        };
        /*var result = _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        orderDTO.IdOrder = result.Entity.IdOrder;*/
        return await Task.FromResult(orderDTO);
    }

    public async Task<List<OrderDTO>> GetOrders()
    {
        return await _context.Orders.Select(order => new OrderDTO
        {
            IdOrder = order.IdOrder,
            FName= order.FName,
            LName=order.LName,
            MName=order.MName,
            Age=order.Age,
            Phone= order.Phone,
            Email = order.Email,
            Image=order.Image,
            IdS = order.Schedule.IdS,

        }).ToListAsync();
    }

    internal Task AddOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderDTO?> UpdateOrder(int id, OrderDTO updatedOrder)
    {
        var uporder = await _context.Orders.FirstOrDefaultAsync(o => o.IdOrder == id);
        if (uporder == null)
            return null;
        uporder.FName = updatedOrder.FName;
        uporder.LName = updatedOrder.LName;
        uporder.MName = updatedOrder.MName;
        uporder.Age= updatedOrder.Age;
        uporder.Phone = updatedOrder.Phone;
        uporder.Email = updatedOrder.Email;
        uporder.Image = updatedOrder.Image;

        _context.Orders.Update(uporder);
        _context.Entry(uporder).State = EntityState.Modified;
        return await Task.FromResult(updatedOrder);
    }

    public async Task<bool> DeleteOrder(int id)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.IdOrder == id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;

    }
    public async Task<List<IncompleteOrder>> GetIncompleteOrd()
    {
        var ords = await _context.Orders.ToListAsync();
        List<IncompleteOrder> result = new List<IncompleteOrder>();
        ords.ForEach(o => result.Add(new IncompleteOrder { FName = o.FName, LName = o.LName, Age=o.Age}));
        return await Task.FromResult(result);
    }
}
