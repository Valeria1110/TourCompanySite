//интерфейс, описывающий получение данных по расписаниям

using Blazor.Data.Models;

namespace Blazor.Services;

public interface IScheduleProvider
{
    Task<List<Schedule>> GetAll();
    Task<Schedule> GetOne(int id);
    Task<bool> Add(Schedule item);
    Task<Schedule> Edit(Schedule item);
    Task<bool> Remove(int id);
}
