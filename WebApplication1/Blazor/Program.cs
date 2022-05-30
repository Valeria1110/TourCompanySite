using Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7111") });


/*Зарегистрируем провайдеры при инициализации приложения,
чтобы при необходимости подставлять единственный экземпляр в компонентах и страницах*/

builder.Services.AddScoped<ITourProvider, ToursProvider>();
builder.Services.AddScoped<IOrderProvider, OrdersProvider>();
builder.Services.AddScoped<IScheduleProvider, SchedulesProvider>();

await builder.Build().RunAsync();