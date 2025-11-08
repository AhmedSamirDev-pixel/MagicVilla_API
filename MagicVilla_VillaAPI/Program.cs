
using MagicVilla_VillaAPI.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


#region Configure the global Serilog logger
// -- Create a new LoggerConfiguration object
//Log.Logger = new LoggerConfiguration()   
//     // Set the minimum log level to Debug (logs everything: Debug, Info, Warning, Error, Fatal)
//    .MinimumLevel.Debug()                
//    // Add a sink to write logs to the console (so you can see logs in real-time while running the app)
//    .WriteTo.Console()
//    // Add a sink to write logs to a file named "villaLogs.txt" inside a "logs" folder
//    // Automatically create a new log file each day
//    .WriteTo.File("logs/villaLogs.txt", rollingInterval: RollingInterval.Day)
//    // Build and assign the logger so it can be used globally in the application
//    .CreateLogger();                    

//builder.Services.AddSerilog();
#endregion


builder.Services.AddControllers(option => 
    {
        //option.ReturnHttpNotAcceptable = true;
    }).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILogging, Logging>();

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
