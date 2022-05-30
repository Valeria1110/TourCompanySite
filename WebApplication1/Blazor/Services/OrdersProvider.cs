//класс, в котором пропишем что именно необходимо делать при вызове того, или иного метода из интерфейса провайдера IOrderProvider

/*Важно отметить, что класс наследуется от ранее созданного интерфейса IOrderProvider, тем самым
 * задавая структуру и варианты забора данных. 
 * Далее мы создаем приватную переменную HttpClient, 
 * и в конструкторе класса присваеваем ей значение из аргумента класса */

using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Blazor.Data.Models;
using Newtonsoft.Json;

namespace Blazor.Services;

public class OrdersProvider : IOrderProvider
{
    private HttpClient _client;
    public OrdersProvider(HttpClient client)
    {
        _client = client;
    }

    /*Обращение происходит на адрес сервера по пути api/orders(вспоминаем swagger из л.р. 4), 
     * то есть на сервере будет вызван контроллер authors, и, поскольку это get запрос, будет вызван метод-обработчик get запросов,
     * который вернет всех доступных авторов. Модель хранения автора идентична модели на сервере, поэтому при десериализации
     * мы полуим ровно такой же список авторов, что был на сервере */
    public async Task<List<Order>> GetAll()
    {
        return await _client.GetFromJsonAsync<List<Order>>("/api/order");
    }

    //id, который будет получен контроллером, а также get метод для получения автора по id будет брать из пути
    //и подставлять при вызове сервиса на сервере!
    //Результатом такого запроса будет получение одного объекта типа Order.
    public async Task<Order> GetOne(int id)
    {
        return await _client.GetFromJsonAsync<Order>($"/api/order/{id}");
    }

    public async Task<bool> Add(Order item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var responce = await _client.PostAsync($"/api/order", httpContent);
        return await Task.FromResult(responce.IsSuccessStatusCode);
    }

    public async Task<Order> Edit(Order item)
    {
        string data = JsonConvert.SerializeObject(item);
        StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
        var responce = await _client.PutAsync($"/api/order", httpContent);
        Order order = JsonConvert.DeserializeObject<Order>(responce.Content.ReadAsStringAsync().Result);
        return await Task.FromResult(order);
    }

    public async Task<bool> Remove(int id)
    {
        var delete = await _client.DeleteAsync($"/api/order/${id}");

        return await Task.FromResult(delete.IsSuccessStatusCode);
    }
}
