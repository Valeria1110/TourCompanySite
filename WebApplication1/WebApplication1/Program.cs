using WebApplication1.Data;
using WebApplication1.Data.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//внешний обработчик JSONа с опцией перехвата циклической сериализации
builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TourContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//мои сервисы
builder.Services.AddTransient<TourService>();
builder.Services.AddTransient<ScheduleService>();
builder.Services.AddTransient<OrderService>();

var app = builder.Build();

//добавляет промежуточное ПО Swagger только в том случае, если текущая среда настроена на разработку.
//Вызов UseSwaggerUIметода включает промежуточное ПО статического файла.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Swagger-это язык описания интерфейса для описания интерфейсов RESTful API, выраженных с помощью JSON.
//Пользовательский интерфейс Swagger обеспечивает пользовательский веб-интерфейс,
//предоставляющий сведения о службе с использованием созданной спецификации OpenAPI.


//Когда клиент пытается запросить данные/контент с сервера на другом источнике(англ. "origin").
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
