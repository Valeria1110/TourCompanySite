using WebApplication1.Data;
using WebApplication1.Data.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//������� ���������� JSON� � ������ ��������� ����������� ������������
builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TourContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//��� �������
builder.Services.AddTransient<TourService>();
builder.Services.AddTransient<ScheduleService>();
builder.Services.AddTransient<OrderService>();

var app = builder.Build();

//��������� ������������� �� Swagger ������ � ��� ������, ���� ������� ����� ��������� �� ����������.
//����� UseSwaggerUI������ �������� ������������� �� ������������ �����.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Swagger-��� ���� �������� ���������� ��� �������� ����������� RESTful API, ���������� � ������� JSON.
//���������������� ��������� Swagger ������������ ���������������� ���-���������,
//��������������� �������� � ������ � �������������� ��������� ������������ OpenAPI.


//����� ������ �������� ��������� ������/������� � ������� �� ������ ���������(����. "origin").
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
