using Microsoft.EntityFrameworkCore;
using Todolist.Automapper;
using Todolist.Data;
using Todolist.Repositories;
using Todolist.Services;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ContextName")));

builder.Services.AddTransient<ITaskRepository, TaskService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProvider).Assembly);

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
