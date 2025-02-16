using Elysian.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Use AddControllers for API controllers without views
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS for your Next.js frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJs", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Replace with your Next.js frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Enable static files if needed
app.UseStaticFiles();

// Enable CORS
app.UseCors("AllowNextJs");

app.UseRouting();

// Add middleware for authorization (if needed for secured APIs)
app.UseAuthorization();

// Map controller routes
app.MapControllers(); // Ensure this is used for API endpoints

// Optional: Keep the default MVC route for existing Razor pages or views
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
