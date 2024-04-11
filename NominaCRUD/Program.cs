
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NominaCRUD.Controllers;
using NominaCRUD.Models;
using NuGet.Protocol.Core.Types;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ContabilidadService>();
builder.Services.AddHttpClient<AsientosContablesController>(client => {
    client.BaseAddress = new Uri("https://ap1-contabilidad.azurewebsites.net/");
});
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RecorverNominaContext>(
    options => options.UseSqlServer(  builder.Configuration.GetConnectionString("CadenaSQL") ) 
    );




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
