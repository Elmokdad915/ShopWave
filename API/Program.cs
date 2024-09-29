using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfiles));


builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumentation();


var app = builder.Build();




// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.UseSwaggerDocumentaion();

app.MapControllers();

//Apply migrations and create database on startup and seed data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    //Apply migrations async
    await context.Database.MigrateAsync();
    //Seed the database async
    await StoreContextSeed.SeedAsync(context);

}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred while migrating the database.");
}
app.Run();


