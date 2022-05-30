using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.DTOs;
using WebApplication1.Data.Models;
namespace WebApplication1.Data.Services;
public class ScheduleService
{
    private TourContext _context;
    public ScheduleService(TourContext context)
    {
        _context = context;
    }
    public async Task<ScheduleDTO?> AddSchedule(ScheduleDTO schDTO)

    {
        var tour = await _context.Tours.FirstOrDefaultAsync(t => t.Id == schDTO.Id);

        if (tour == null)
            return null;
        Schedule schedule = new Schedule
        {
            DateBegin= schDTO.DateBegin,
            DateEnd= schDTO.DateEnd,
            FreePlaces= schDTO.FreePlaces,
            Image= schDTO.Image,
            Orders = new List<Order>(),
            Tour=tour,
        };
        var result = _context.Schedules.Add(schedule);
        await _context.SaveChangesAsync();
        var schDTO1 = new ScheduleDTO
        {
            IdS=result.Entity.IdS,
            DateBegin=result.Entity.DateBegin,
            DateEnd=result.Entity.DateEnd,
            FreePlaces=result.Entity.FreePlaces,
            Image= result.Entity.Image,
            OrdersIds = schDTO.OrdersIds,
            Id=schDTO.Id,
        };
        return await Task.FromResult(schDTO);
    }

    public async Task<ScheduleDTO?> GetSchedule(int id)
    {
        var schedule = await _context.Schedules.Include(a=>a.Tour).Include(b=>b.Orders).FirstOrDefaultAsync(s => s.IdS == id);
        if (schedule == null)
            return null;
        var schDTO = new ScheduleDTO
        {
            IdS= schedule.IdS,
            DateBegin= schedule.DateBegin,
            DateEnd= schedule.DateEnd,
            FreePlaces = schedule.FreePlaces,
            Image = schedule.Image,
            OrdersIds=schedule.Orders.Select(o => o.IdOrder).ToArray(),
            Id = schedule.Tour.Id,
        };
        return await Task.FromResult(schDTO);

    }

    public async Task<List<ScheduleDTO>> GetSchedules()
    {
        return await _context.Schedules.Select(schedule => new ScheduleDTO
        {
            IdS = schedule.IdS,
            DateBegin = schedule.DateBegin,
            DateEnd = schedule.DateEnd,
            FreePlaces = schedule.FreePlaces,
            Image = schedule.Image,
            OrdersIds = schedule.Orders.Select(o => o.IdOrder).ToArray(),
            Id = schedule.Tour.Id,
        }).ToListAsync();
    }
    public async Task<ScheduleDTO?> UpdateSchedule(int id, ScheduleDTO updatedSchedule)
    {
        var schedule = await _context.Schedules.FirstOrDefaultAsync(s => s.IdS == id);
        if (schedule != null)
        {
            schedule.DateBegin= updatedSchedule.DateBegin;
            schedule.DateEnd= updatedSchedule.DateEnd;
            schedule.FreePlaces = updatedSchedule.FreePlaces;
            schedule.Image = updatedSchedule.Image;

            _context.Update(schedule);
            _context.Entry(schedule).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var scheduleDTO1 = new ScheduleDTO
            {
                IdS= schedule.IdS,
                DateBegin= schedule.DateBegin,
                DateEnd= schedule.DateEnd,
                FreePlaces= schedule.FreePlaces,
                Image= schedule.Image,
                OrdersIds= schedule.Orders.Select(o => o.IdOrder).ToArray(),
            };
            return await Task.FromResult(scheduleDTO1);
        }
        return null;
    }

    internal Task AddSchedule(Schedule schedule)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteSchedule(int id)
    {
        var schedule= await _context.Schedules.FirstOrDefaultAsync(s => s.IdS == id);
        if (schedule != null)
        {
            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;

    }
    public async Task<List<ScheduleDTO>> GetIncompleteSch()
    {
        var schs = await _context.Schedules.ToListAsync();
        List<ScheduleDTO> result = new List<ScheduleDTO>();
        schs.ForEach(s => result.Add(new ScheduleDTO { IdS = s.IdS, DateBegin = s.DateBegin, DateEnd = s.DateEnd, FreePlaces = s.FreePlaces, Image = s.Image}));
        return await Task.FromResult(result);
    }
}
