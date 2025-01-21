using Core.IRepository;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Metricas do Prometheus
var requestCounter = Metrics.CreateCounter("api_requests_total", "Total de requisi��es recebidas pela API");


// Instanciando o appsettings
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddControllers();
// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Serializa��o
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DB_CONNECTION"));
}, ServiceLifetime.Scoped);

// M�todos
builder.Services.AddScoped<IContato, ContatoRepository>();
builder.Services.AddHttpClient<IContato, ContatoRepository>();

var app = builder.Build();

// Configure o pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware do Prometheus
app.Use(async (context, next) =>
{
    requestCounter.Inc(); // Incrementa o contador de requisi��es
    await next.Invoke();  // Continua o processamento da requisi��o
});


// Expondo as m�tricas para o Prometheus
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapMetrics();  // Mapeia as m�tricas para a rota /metrics
});

app.UseAuthorization();

app.MapControllers();

app.Run();
