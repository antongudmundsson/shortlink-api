using ShortLink.Api.Storage;
using ShortLink.Api.Utilities;
using ShortLink.Api.Services;
using ShortLink.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSingleton<IShortLinkStore, InMemoryShortLinkStore>();
builder.Services.AddSingleton<IShortCodeGenerator, ShortCodeGenerator>();
builder.Services.AddSingleton<IShortLinkService, ShortLinkService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () => Results.Ok(new
{
    name = "ShortLink API",
    status = "Running"
}));

app.MapShortLinkEndpoints();

app.Run();