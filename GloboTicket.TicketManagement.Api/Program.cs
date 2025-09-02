using GloboTicket.TicketManagement.Api;

var builder = WebApplication.CreateBuilder(args);
var app = builder.ConfigureServices()
                 .ConfigurePipeline();

await app.ResetDatabaseAsync();
app.MapGet("/", () => "API is running!");

app.Run();
