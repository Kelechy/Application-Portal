using Application_Portal.Data;
using Application_Portal.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Cosmos DB
string connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
string databaseName = "MyDB";
string containerName = "CandidatApplicationDB";

CosmosClient cosmosClient = new CosmosClient(connectionString);
Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
Container container = await database.CreateContainerIfNotExistsAsync(containerName, "/id");

// Register repository and container
builder.Services.AddSingleton<Container>(container);
builder.Services.AddScoped<ICandidateApplicationService, CandidateApplicationService>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.UseAuthorization();

app.MapControllers();

app.Run();
