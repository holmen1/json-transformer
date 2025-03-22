using json_transformer.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapPost("/assumptions", (AssumptionsModelDto requestModel) =>
    {
        // Map DTO to domain model
        var domainModel = new AssumptionsModel
        {
            Assumptions = requestModel.Assumptions.Select(a => new Assumption
            {
                Level1 = Enum.Parse<Level1Type>(a.Level1),
                Level2 = Enum.Parse<Level2Type>(a.Level2),
                Level3 = Enum.Parse<Level3Type>(a.Level3, ignoreCase: true),
                Value = a.Value
            }).ToList()
        };
    
        // Process the domain model
    
        return Results.Ok(requestModel); // Return DTO, not domain model
    })
    .WithName("PostAssumptions")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}