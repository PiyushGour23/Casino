using Casino.Data;
using Casino.IRepository;
using Casino.Repository;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();       

builder.Services.AddCors(p => p.AddPolicy("vaibhavpolicy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddCors(p => p.AddDefaultPolicy(build =>
{
    build.WithOrigins("https://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddRateLimiter(_ => _.AddFixedWindowLimiter(policyName: "FixedWindow", options =>
{
    options.Window = TimeSpan.FromSeconds(10);
    options.PermitLimit = 1;
    options.QueueLimit = 0;
    options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
}).RejectionStatusCode=401);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IVaibhavRepository, VaibhavRepository>();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RegisterDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRateLimiter();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
