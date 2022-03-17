var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

#region Database
    builder.Services.AddDbContext<R6Context>(options => options.UseMySql(builder.Configuration["ConnectionStrings:R6db"],Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql")));
#endregion
app.UseAuthorization();

app.MapControllers();

app.Run();
