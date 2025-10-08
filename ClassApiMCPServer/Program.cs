// See https://aka.ms/new-console-template for more information

using ClassApiMCPServer.Services;
using Coso.ClassComNet.Sdk;
using dotenv.net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Register your MCP server + discover tools in the assembly
builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithPromptsFromAssembly()
    .WithResourcesFromAssembly()
    .WithToolsFromAssembly(); // scans for [McpServerTool] methods

builder.WebHost.UseUrls("http://localhost:5050");

DotEnv.Load();

builder.Services.AddSingleton<ClassApiClient>();
builder.Services.AddSingleton<TimeService>();

builder.Services.AddClassComSdk(httpClient =>
    {
        var host = Environment.GetEnvironmentVariable("ClassApiClientSettings:BaseAddress");
        var key = Environment.GetEnvironmentVariable("ClassApiClientSettings:SecretKey");
        httpClient.BaseAddress = new Uri(host);
        var headers = httpClient.DefaultRequestHeaders;
        headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", key);
    }
);


var app = builder.Build();

// Map the MCP endpoints for Streamable HTTP
// This adds the required /sse (events) and /messages endpoints.
app.MapMcp();

// (Optional) protect your MCP endpoints
// app.MapMcp().RequireAuthorization();

app.Run();