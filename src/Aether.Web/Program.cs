using Aether.Data.Repositories;
using Aether.Services.Core;
using Aether.Web.Extensions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDatabase(builder.Configuration);

builder.Services.ConfigureIdentity(builder.Configuration);

builder.Services.AddControllersWithViews();

builder.Services.RegisterRepositories(typeof(RealmRepository));
builder.Services.RegisterServices(typeof(UserService));

builder.Services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

//app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.Run();
