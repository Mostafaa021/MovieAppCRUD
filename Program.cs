using Microsoft.EntityFrameworkCore;
using MovieAppCRUD.Models;
using MovieAppCRUD.Repositories;
using MovieAppCRUD.Services;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Adding DbContext
builder.Services.AddDbContext<ApplicationDBContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//Register Other Libraries Like NToastNotify
builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions { 
    ProgressBar = true , 
    CloseButton = true ,
    CloseDuration =  true , 
    PositionClass = ToastPositions.TopRight,
    PreventDuplicates = true
});
// Adding Services which injected 
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();

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
