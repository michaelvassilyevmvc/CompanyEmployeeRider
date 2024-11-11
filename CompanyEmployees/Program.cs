using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);
LogManager.Setup()
    .LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly); // чтобы можно было брать контроллеры из другого проекта
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions()
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");  


app.UseAuthorization();

#region Пример создания midleware

// app.Use(async (context, next) =>
// {
//     Console.WriteLine("Логика перед выполнением следующего делегата в методе Use");
//     await next.Invoke();
//     Console.WriteLine($"Логика после выполнения следующего делегата в методе Use");
// });
//
// app.Map("/usingmapbranch", builder =>
// {
//     builder.Use(async (context, next) =>
//     {
//         Console.WriteLine("Логика ветвления карты в методе Use перед следующим делегатом");
//         await next.Invoke();
//         Console.WriteLine("Логика ветвления карты в методе Use после следующего делегата");
//     });
//     builder.Run(async context =>
//     {
//         Console.WriteLine("Ответ ветки сопоставления клиенту в методе Run");
//         await context.Response.WriteAsync( "Hello from Map Branch");
//     });
// });
//
// app.Map("/super", builder =>
// {
//     builder.Use(async (context, next) =>
//     {
//         Console.WriteLine("Вы зашли в супер раздел");
//         await next.Invoke();
//         Console.WriteLine("Вы вышли из супер раздела");
//     });
//     builder.Run(async context =>
//     {
//         context.Response.StatusCode = 200;
//         await context.Response.WriteAsync("Hello, you are in super direct");
//     });
// });
//
// app.MapWhen(context => context.Request.Query.ContainsKey("test"), builder =>
// {
//     builder.Run(async context =>
//     {
//         await context.Response.WriteAsync("Condition branch");
//     });
// });
#endregion

app.MapControllers();

app.Run();
