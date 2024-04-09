using Microsoft.Extensions.Logging.ApplicationInsights;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = builder.Configuration.GetSection("ApplicationInsights:ConnectionString").Value;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry(); // need to display request log on performance in application insights
//builder.Services.AddLogging(log =>
//{
//    log.AddApplicationInsights();
//    log.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Trace);
//});
builder.Logging.AddApplicationInsights(
	configureTelemetryConfiguration: (config) => 
		config.ConnectionString = connectionString,
	configureApplicationInsightsLoggerOptions: (options) => { }
);

builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>("ApplicationInsightTest", LogLevel.Trace);

var app = builder.Build();
app.Logger.LogInformation("Adding Routes");
app.MapGet("/", () => "Hello World!");
app.Logger.LogInformation("Starting the app");
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
