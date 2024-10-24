using ExamForms.Data;
using ExamForms.Manager;
using ExamForms.Manager.Accounts;
using ExamForms.Models.Accounts;
using ExamForms.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DB") ?? throw new InvalidOperationException("Connection string 'DB' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ExamFormDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

// Services
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();

builder.Services.AddTransient<AccountManager>();
builder.Services.AddTransient<AdministrationManager>();
builder.Services.AddTransient<TemplateManager>();
builder.Services.AddTransient<QuestionManager>();
builder.Services.AddTransient<FormsManager>();

builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<TemplateRepository>();
builder.Services.AddTransient<QuestionRepository>();
builder.Services.AddTransient<FormsRepository>();

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
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
