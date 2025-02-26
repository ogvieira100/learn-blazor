using BlazorApp.Client.Models;
using BlazorApp.Client.Pages;
using BlazorApp.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

//builder.Services.AddCascadingValue(sp =>
//{
//    var StyleContext = new BlazorApp.Client.Models.StyleContext { BackgroundColor = "#ADD8E6" };
//    var source = new CascadingValueSource<BlazorApp.Client.Models.StyleContext>(StyleContext, isFixed: false);
//    return source;
//});

builder.Services.AddCascadingValue(sp =>
    CascadingValueSource.CreateNotifying(new StyleContext()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/Error/{0}");  
app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();  
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorApp.Client._Imports).Assembly);

//builder.Services.Configure<CircuitOptions>(options => options.DetailedErrors = true);


app.Run();


public static class CascadingValueSource
{
    public static CascadingValueSource<T> CreateNotifying<T>(T value, bool isFixed = false) where T : INotifyPropertyChanged
    {
        var source = new CascadingValueSource<T>(value, isFixed);

        value.PropertyChanged += (sender, args) => source.NotifyChangedAsync();

        return source;
    }
}
