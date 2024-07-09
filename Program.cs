using CMSGTechnical.Components;
using CMSGTechnical.Domain.Interfaces;
using CMSGTechnical.Domain.Models;
using CMSGTechnical.Mediator;
using CMSGTechnical.Repository;
using Microsoft.EntityFrameworkCore;


// Add services to the container.
WebApplication.CreateBuilder(args).Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    ;

WebApplication.CreateBuilder(args).Services.AddDbContextFactory<ApplicationDbContext>(optionsBuilder =>
{
    optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
});


WebApplication.CreateBuilder(args).Services.AddScoped(typeof(IRepo<>), typeof(Repo<>));
WebApplication.CreateBuilder(args).Services.AddMediatR(configuration =>
{
    configuration.Lifetime = ServiceLifetime.Scoped;
    configuration.RegisterServicesFromAssemblies(typeof(AssemblyHook).Assembly);
});


var app = WebApplication.CreateBuilder(args).Build();


var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
await dbContext.Database.EnsureCreatedAsync();

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

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    ;

app.Run();
