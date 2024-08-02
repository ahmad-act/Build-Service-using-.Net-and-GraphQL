using BookInformationService;
using BookInformationService.BusinessLayer;
using BookInformationService.DataAccessLayer;
using BookInformationService.DatabaseContext;
using BookInformationService.GraphQL;
using BookInformationService.GraphQL.Mutations;
using BookInformationService.GraphQL.Queries;
using GraphQL.Server;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Display;
using Serilog.Sinks.Email;
using System.Net;

try
{
    var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();

    var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");


    AppSettings? appSettings = configuration.GetRequiredSection("AppSettings").Get<AppSettings>();

    var emailInfo = new EmailSinkOptions
    {
        Subject = new MessageTemplateTextFormatter(appSettings.EmailSubject, null),
        Port = appSettings.Port,
        From = appSettings.FromEmail,
        To = new List<string>() { appSettings.ToEmail },
        Host = appSettings.MailServer,
        //EnableSsl = appSettings.EnableSsl,
        Credentials = new NetworkCredential(appSettings.FromEmail, appSettings.EmailPassword)
    };

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Serilog\\log_.txt"), rollOnFileSizeLimit: true, fileSizeLimitBytes: 1000000, rollingInterval: RollingInterval.Month, retainedFileCountLimit: 24, flushToDiskInterval: TimeSpan.FromSeconds(1))
        //.WriteTo.Email(emailInfo)                           
        .CreateLogger();





    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    // EF
    builder.Services.AddDbContext<SystemDbContext>(options =>
                options.UseSqlite(defaultConnectionString));

    // Models
    builder.Services.AddScoped<IBookInformationDL, BookInformationDL>();
    builder.Services.AddScoped<IBookInformationBL, BookInformationBL>();

    // Register GraphQL
    builder.Services.AddScoped<BookInformationQuery>();
    builder.Services.AddScoped<BookInformationMutation>();
    builder.Services.AddScoped<AppSchema>();
    builder.Services.AddGraphQL().AddSystemTextJson();

    Log.Information("BookInfo Service is started.");

    var app = builder.Build();

    //// Apply migrations at startup
    //using (var scope = app.Services.CreateScope())
    //{
    //    var dbContext = scope.ServiceProvider.GetRequiredService<SystemDbContext>();
    //    dbContext.Database.Migrate();
    //}


    if (app.Environment.IsDevelopment())
    {

    }

    app.UseHttpsRedirection();

    //GraphQL
    app.UseGraphQL<AppSchema>();
    app.UseGraphQLGraphiQL("/ui/graphql");


    app.Run();

    Log.Information("BookInfo Service is stopped.");
}
catch (Exception ex)
{
    Log.Fatal(ex, "BookInfo Service is failed to run correctly.");
}
finally
{
    Log.CloseAndFlush();
}

