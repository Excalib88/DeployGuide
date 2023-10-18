using DeployGuide;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(policy => policy.AddPolicy("default", opt =>
{
    opt.AllowAnyHeader();
    opt.AllowCredentials();
    opt.AllowAnyMethod();
    opt.SetIsOriginAllowed(_ => true);
}));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("default");

app.UseAuthorization();
app.MapControllers();

app.Run();
