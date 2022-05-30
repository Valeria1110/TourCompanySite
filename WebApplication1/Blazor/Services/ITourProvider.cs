//интерфейс, описывающий получение данных по турам

using Blazor.Data.Models;

namespace Blazor.Services;

public interface ITourProvider
{
    Task<List<Tour>> GetAll();
    Task<Tour> GetOne(int id);
    Task<bool> Add(Tour item);
    Task<Tour> Edit(Tour item);
    Task<bool> Remove(int id);
}
