using json_transformer.Mappers;
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
        // Use the mapper to convert DTO to domain model
        var domainModel = AssumptionMapper.ToAssumptionsModel(requestModel);

        var assumptionDictionary = AssumptionMapper.ToAssumptionsDictionary(domainModel);

        var revesedDomainModel = AssumptionMapper.ToAssumptionsModelDto(domainModel);
        return Results.Ok(revesedDomainModel); // Return domain model
    })
    .WithName("PostAssumptions")
    .WithOpenApi();

app.Run();