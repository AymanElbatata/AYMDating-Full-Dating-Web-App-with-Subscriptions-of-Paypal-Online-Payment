using AYMDating.API.Helpers;
using AYMDating.Blazor.Data;
using AYMDating.Blazor.Data.Helpers;
using AYMDating.DAL.Interfaces;
using AYMDating.DAL.Repositories;
using AYMDating.DAL.Services;
using Blazored.LocalStorage;
using Blazored.LocalStorage.StorageOptions;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/login";
    });


builder.Services.AddAuthorization();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSignalR();


builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<ApiServiceLogin>();
builder.Services.AddScoped<AppState>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<JsInteropService>();
builder.Services.AddScoped<ChatManagement>();

//builder.Services.AddTransient<AuthenticationHandler>();
//builder.Services.AddHttpClient("AuthorizedClient")
//    .AddHttpMessageHandler<AuthenticationHandler>();


//builder.Services.AddTransient<AuthenticationHandler>();
//builder.Services.AddHttpClient("AuthorizedClient")
//    .AddHttpMessageHandler<AuthenticationHandler>();

//builder.Services.AddScoped(sp =>
//{
//    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
//    return clientFactory.CreateClient("AuthorizedClient");
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");

app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapHub<ChatHub>("/chathub");
    endpoints.MapFallbackToPage("/_Host");
    //endpoints.MapFallbackToFile("index.html");
});

app.Run();
