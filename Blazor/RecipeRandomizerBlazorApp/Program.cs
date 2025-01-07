using Amazon.CloudWatchLogs;
using Amazon.Runtime;
using MassTransit;
using RecipeRandomizerBlazorApp.Components;
using RecipeRandomizerBlazorApp.Events.Factories;
using RecipeRandomizerBlazorApp.Events.Factories.Interfaces;
using RecipeRandomizerBlazorApp.StateContainer;
using Serilog;
using Serilog.Sinks.AwsCloudWatch;
using Utilities.ConfigurationManager.Extensions;
using Amazon;
using UserManagementService.Api.Client;
using RecipeRandomizerBlazorApp.UserManagementService.Proxy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(cfg => 
{
    cfg.UsingRabbitMq();
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<StateContainer>();

builder.Services.AddSingleton<IPageViewedEventFactory, PageViewedEventFactory>();

builder.Services.AddHttpClient("UserManagementServiceClient", config => 
{
    config.BaseAddress = new Uri(builder.Configuration.GetStringValue("UserManagementServiceClient:Url"));
});

builder.Services.AddSingleton(c => 
{
    var factory = c.GetService<IHttpClientFactory>();
    var httpClient = factory?.CreateClient("UserManagementServiceClient");

    ArgumentNullException.ThrowIfNull(httpClient);

    httpClient.BaseAddress = new Uri(builder.Configuration.GetStringValue("UserManagementServiceClient:Url"));
    
    return new UserClient(httpClient);
});

builder.Services.AddTransient<IUserManagementServiceProxy, UserManagementServiceProxy>();

var app = builder.Build();

if(builder.Configuration.GetBoolValue("AwsCloudwatchLogging:Enabled") == true)
{
    var client = new AmazonCloudWatchLogsClient(new BasicAWSCredentials(builder.Configuration.GetStringValue("AwsCloudwatchLogging:AccessKey"), builder.Configuration.GetStringValue("AwsCloudwatchLogging:SecretKey")), RegionEndpoint.USEast1);

    Log.Logger = new LoggerConfiguration().WriteTo.AmazonCloudWatch(
        logGroup: builder.Configuration.GetStringValue("AwsCloudwatchLogging:LogGroup"),
        logStreamPrefix: builder.Configuration.GetStringValue("AwsCloudwatchLogging:LogStreamPrefix"),
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose,
        createLogGroup: true,
        appendUniqueInstanceGuid: true,
        appendHostName: false,
        logGroupRetentionPolicy: LogGroupRetentionPolicy.ThreeDays,
        cloudWatchClient: client).CreateLogger();
}
else
{
    Log.Logger = new LoggerConfiguration().WriteTo.File("./Logs/logs-", rollingInterval: RollingInterval.Day).MinimumLevel.Debug().CreateLogger();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
