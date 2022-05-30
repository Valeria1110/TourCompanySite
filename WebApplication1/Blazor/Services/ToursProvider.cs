//класс, в котором пропишем что именно необходимо делать при вызове того, или иного метода из интерфейса провайдера ITourProvider

/*Важно отметить, что класс наследуется от ранее созданного интерфейса ITourProvider, тем самым
 * задавая структуру и варианты забора данных. 
 * Далее мы создаем приватную переменную HttpClient, 
 * и в конструкторе класса присваеваем ей значение из аргумента класса */

using System.Net.Http.Json;
using Newtonsoft.Json;
using Blazor.Data.Models;

namespace Blazor.Services;

public class ToursProvider : ITourProvider
{
    private HttpClient _client;
    public ToursProvider(HttpClient client)
    {
        _client = client;
    }
    /*Обращение происходит на адрес сервера по пути api/tours(вспоминаем swagger из л.р. 4), 
     * то есть на сервере будет вызван контроллер authors, и, поскольку это get запрос, будет вызван метод-обработчик get запросов,
     * который вернет всех доступных авторов. Модель хранения автора идентична модели на сервере, поэтому при десериализации
     * мы полуим ровно такой же список авторов, что был на сервере */
    public async Task<List<Tour>> GetAll()
    {
        return await _client.GetFromJsonAsync<List<Tour>>("/api/Tour");
    }

    //id, который будет получен контроллером, а также get метод для получения автора по id будет брать из пути
    //и подставлять при вызове сервиса на сервере!
    //Результатом такого запроса будет получение одного объекта типа Tour.
    public async Task<Tour> GetOne(int id)
    {
        return await _client.GetFromJsonAsync<Tour>($"/api/Tour/{id}");
    }

    /*нам сначала необходимо получить текстовое значение объекта Tour(сериализовать). 
     * Для этого используем ранее подключенную библиотеку Newtonsoft.Json из репозитория NuGet. 
     * Экземпляром класса StringContent формируем объект контента для запроса, где подставляем наш 
     * сериализованный(из объекта в текстовый JSON) тур, указываем кодировку и тип контента. 
     * Делаем post запрос по пути api/tour с подставленным контентом в запрос. 
     * Тот контент, что мы добавили в запрос и будет считан в контроллере tour методом обработки post запроса. 
     * Данное значение считается сервером благодаря атрибуту [FromBody],  
     * который мы указывали для переменной в аргументе метода обработки post запроса на сервере! */
    public async Task<bool> Add(Tour item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var responce = await _client.PostAsync($"/api/Tour", httpContent);
        return await Task.FromResult(responce.IsSuccessStatusCode);
    }

    public async Task<Tour> Edit(Tour item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var responce = await _client.PutAsync($"/api/Tour", httpContent);
        Tour tour = JsonConvert.DeserializeObject<Tour>(responce.Content.ReadAsStringAsync().Result);
        return await Task.FromResult(tour);
    }

    public async Task<bool> Remove(int id)
    {
        var delete = await _client.DeleteAsync($"/api/Tour/${id}");

        return await Task.FromResult(delete.IsSuccessStatusCode);
    }
}
