using BrainwaveAPI.Database;
using BrainwaveAPI.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddJsonFile("appsettings.json");

        var dbConnectionString = builder.Configuration.GetValue<string>("ConnectionString");
        var keepAliveConnection = new SqliteConnection(dbConnectionString);
        await keepAliveConnection.OpenAsync();

        AppDomain.CurrentDomain.ProcessExit += (s, e) => CloseKeepAliveConnection(keepAliveConnection);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)  // Read configuration from appsettings.json
            .Enrich.FromLogContext()
            .CreateLogger();
        // Add services to the container.

        builder.Services.AddControllers()
            .AddJsonOptions(configuration =>
            {
                configuration.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                configuration.JsonSerializerOptions.WriteIndented = true;
            });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<BrainwaveDbContext>(_ => _.UseSqlite(dbConnectionString));
        builder.Services.AddScoped<IBrainwaveDbContext, BrainwaveDbContext>();
        builder.Services.AddScoped<IQuizService, QuizService>();
        builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        builder.Services.AddScoped<IQuestionService, QuestionService>();
        builder.Logging.AddSerilog();
        builder.Services.AddSerilog();

        var app = builder.Build();
        app.UseSerilogRequestLogging();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<BrainwaveDbContext>();

            if (app.Environment.IsDevelopment())
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
            }
            else
            {
                context.Database.Migrate();
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }

    private static async void CloseKeepAliveConnection(SqliteConnection connection)
    {
        await connection.CloseAsync();
    }
}
