using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlazorAppTreino.Components;
using BlazorAppTreino.Components.Account;
using BlazorAppTreino.Domain.Data;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using BlazorAppTreino.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using BlazorAppTreino.Domain.Utils;
using Microsoft.AspNetCore.Http;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddCascadingValue(sp =>
    CascadingValueSource.CreateNotifying(new StyleContext()));


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<StateContainerStyle>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
    .EnableSensitiveDataLogging()
    .UseLazyLoadingProxies()
    );

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddScoped(typeof(IRepositoryConsult<>), typeof(RepositoryConsult<>));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddHttpClient("ApiTreino", (http) =>
{

    http.BaseAddress = new Uri("https://localhost:7095");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorAppTreino.Client._Imports).Assembly);
// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

#region "Minimal Apis"

app.MapPost("", async ([FromBody] string customer) =>
{

    await Task.FromResult(true);
    return Results.Ok(new { });
});

app.MapPut("", async ([FromBody] string customer) =>
{

    await Task.FromResult(true);
    return Results.Ok(new { });
});

app.MapDelete("api/customers/{id}", async (
    IBaseRepository<Customers> repositoryCustomer,
    long id
    ) =>
{
    var respApi = await Commons.TreatResponse(async () =>
    {
        var customer =  (await repositoryCustomer.RepositoryConsult.SearchAsync(x => x.Id == id))?.FirstOrDefault();
        if (customer != null)
            repositoryCustomer.Remove(customer);
        await repositoryCustomer.UnitOfWork.CommitAsync();    
        return new {   };
    });
    return CascadingValueSource.ReturnApi(respApi);
});

app.MapGet("api/customers", async (
        IBaseRepository<Customers> repositoryCustomer,
        [FromQuery] string? search

    ) =>
{
    var respApi = await Commons.TreatResponse(async () =>
    {
        var query = repositoryCustomer.RepositoryConsult.GetQueryable();
        if (!string.IsNullOrEmpty(search))
        {
            
            var customer = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerSearch>(search);
            if (!string.IsNullOrEmpty(customer.Name))
                query = query.Where(x => x.Name.Contains(customer.Name));

            return await query.PaginateAsync(customer);
        }
        return await query.PaginateAsync(new PagedDataRequest { });
    });
    return CascadingValueSource.ReturnApi(respApi);
});

#endregion

app.Run();


public static class CascadingValueSource
{

    public static IResult ReturnApi<T>(ResponseApi<T> respApi) where T : class
    {
        if (respApi.Notys.Any(x => x.TypeNotificationNoty == TypeNotificationNoty.Error))
            return Results.BadRequest(respApi);
        return Results.Ok(respApi);
    }
    public static CascadingValueSource<T> CreateNotifying<T>(T value, bool isFixed = false) where T : INotifyPropertyChanged
    {
        var source = new CascadingValueSource<T>(value, isFixed);

        value.PropertyChanged += (sender, args) => source.NotifyChangedAsync();

        return source;
    }
}

