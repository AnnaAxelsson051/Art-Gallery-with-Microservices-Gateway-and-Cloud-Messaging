using Micro.GatewaySolution.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppAuthentication();
builder.Services.AddOcelot();
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.UseOcelot();
app.Run();

