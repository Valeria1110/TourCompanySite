using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Models;
using WebApplication1.Data.DTOs;

namespace WebApplication1.Data.Services;
public class TourService
{
    private TourContext _context;
    public TourService(TourContext context)
    {
        _context = context;
    }
    public async Task<TourDTO?> AddTour(TourDTO tourDTO)
    {
        Tour tour = new Tour
        {
            Name = tourDTO.Name,
            Country = tourDTO.Country,
            Days = tourDTO.Days,
            Cost = tourDTO.Cost,
            Level = tourDTO.Level,
            Desc=tourDTO.Desc,
            Image=tourDTO.Image,
            Schedules = new List<Schedule>()
        };
        var result = _context.Tours.Add(tour);
        await _context.SaveChangesAsync();
        var ncustomerDTO = new TourDTO
        {
            Id = result.Entity.Id,
            Name = result.Entity.Name,
            Country = result.Entity.Country,
            Days= result.Entity.Days,
            Cost= result.Entity.Cost,
            Level= result.Entity.Level,
            Desc = result.Entity.Desc,
            Image = result.Entity.Image,
            SchedulesIds = tourDTO.SchedulesIds
        };
        return await Task.FromResult(tourDTO);
    }

    public async Task<TourDTO?> GetTour(int id)
    {
        var tour = await _context.Tours.Include(a=>a.Schedules).FirstOrDefaultAsync(t => t.Id == id);
        if (tour == null)
            return null;
        var tourDTO = new TourDTO
        {
            Id=tour.Id,
            Name=tour.Name,
            Country=tour.Country,
            Days = tour.Days,
            Cost=tour.Cost,
            Level = tour.Level,
            Desc=tour.Desc,
            Image=tour.Image,
            SchedulesIds = tour.Schedules.Select(sch => sch.IdS).ToArray()
        };

        return await Task.FromResult(tourDTO);
    }

    public async Task<List<TourDTO>> GetTours()
    {
        var tour = await _context.Tours.Select(tour => new TourDTO
        {
            Id=tour.Id,
            Name = tour.Name,
            Country = tour.Country,
            Days = tour.Days,
            Cost= tour.Cost,
            Level= tour.Level,
            Desc = tour.Desc,
            Image = tour.Image,
            SchedulesIds = tour.Schedules.Select(sch => sch.IdS).ToArray()
        }).ToListAsync();
        return await Task.FromResult(tour);

    }
    public async Task<TourDTO?> UpdateTour(int id, TourDTO updatedTour)
    {
        var tour = await _context.Tours.FirstOrDefaultAsync(t => t.Id == id);
        if (tour != null)
        {
            tour.Name = updatedTour.Name;
            tour.Country = updatedTour.Country;
            tour.Days = updatedTour.Days;
            tour.Cost = updatedTour.Cost;
            tour.Level = updatedTour.Level;
            tour.Desc = updatedTour.Desc;
            tour.Image= updatedTour.Image;

            _context.Update(tour);
            _context.Entry(tour).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var ntourDTO = new TourDTO
            {
                Id = tour.Id,
                Name = tour.Name,
                Country = tour.Country,
                Days = tour.Days,
                Cost = tour.Cost,
                Level = tour.Level,
                Desc = tour.Desc,
                Image = tour.Image,
                SchedulesIds = tour.Schedules.Select(sch => sch.IdS).ToArray()
            };
            return await Task.FromResult(ntourDTO);
        }
        return null;
    }

    public async Task<bool> DeleteTour(int id)
    {
        var tour = await _context.Tours.FirstOrDefaultAsync(t => t.Id == id);
        if (tour != null)
        {
            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;

    }
    public async Task<List<CostTour>> GetCosts()
    {
        var tours = await _context.Tours.ToListAsync();
        List<CostTour> result = new List<CostTour>();
        tours.ForEach(t => result.Add(new CostTour { Name = t.Name, Cost =t.Cost }));
        return await Task.FromResult(result);
    }
    public async Task<List<TourDTO>> GetIncompleteTour()
    { 
        var tours = await _context.Tours.ToListAsync();
        List<TourDTO> result = new List<TourDTO>();
        tours.ForEach(t => result.Add(new TourDTO { Id=t.Id, Name = t.Name, Country=t.Country, Days=t.Days, Cost=t.Cost, Desc=t.Desc }));
        return await Task.FromResult(result);
    }

}
