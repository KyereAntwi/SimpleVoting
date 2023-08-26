using SVoting.Presentation.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.Run();

public partial class Program { }
