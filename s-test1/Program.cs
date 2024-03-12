using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using PEMS.Contracts;
using PEMS.Providers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration.WriteTo.Console();
    configuration.WriteTo.File("pemslogs/logs.txt", rollingInterval: RollingInterval.Day);
});

// Add services to the container.

builder.Services.AddControllersWithViews();

object p = builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});
RepoDb.SqlServerBootstrap.Initialize();

OracleConfiguration.TnsAdmin = "D:\\temp\\ConsoleApp4\\ConsoleApp4\\Wallet_ZEQ0RP65WJJJ8LAJ";
OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;

builder.Services.AddSingleton<IDataAccessProvider, DataAccessProvider>();
builder.Services.AddSingleton<IOracleDataAccessProvider, OracleDataAccessProvider>();
builder.Services.AddSingleton<IPEMSProvider, PEMSProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
