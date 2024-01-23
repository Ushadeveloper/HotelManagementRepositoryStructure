using HotelManagementRepositoryStructure.Connection;
using HotelManagementRepositoryStructure.Repository;
using HotelManagementRepositoryStructure.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IGuestRepository, GuestRepository>();
builder.Services.AddTransient<IGuestService, GuestService>();
builder.Services.AddTransient<IConnectionString, ConnectionString>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins(new string[]
                    {
                      "https://localhost:3000",
                      "http://localhost:3000",
                       "http://localhost:1445"


                    })
                .WithMethods("POST", "GET", "PUT", "DELETE")
         .AllowAnyHeader()
         .AllowCredentials();
        });
});
//builder.Services.AddTransient<>
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
