using Database;
using IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<EventManagementContext>(
                options =>
                {
                    options.UseSqlite(configuration.GetConnectionString("EventManagementDb"));
                });

builder.Services.AddTransient<IEventService, EventService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("Kumojin", new OpenApiInfo { Title = "Kumojin", Version = "v1" });
});

WebApplication? app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    EventManagementContext? dataContext = scope.ServiceProvider.GetRequiredService<EventManagementContext>();
    dataContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("Kumojin/swagger.json", "Swagger KumojinAPI"); });
}

// Configure the HTTP request pipeline.
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
