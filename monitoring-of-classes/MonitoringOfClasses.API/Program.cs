using Microsoft.EntityFrameworkCore;
using MonitoringOfClasses.Infrastructure;
using MonitoringOfClasses.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<TeacherRepository>();
builder.Services.AddScoped<LessonRepository>();
builder.Services.AddSwaggerGen();

// Register DbContext with the connection string from appsettings.json
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
);


builder.Services.AddCors(policy => {
    policy.AddPolicy("Policy_Name", builder =>
        builder.WithOrigins("https://localhost:7276/")
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyOrigin()
            .WithMethods("GET", "POST", "PUT", "DELETE")
            .AllowAnyHeader()
    );
});

var app = builder.Build();

// Enable CORS using the defined policy
app.UseCors("Policy_Name");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
