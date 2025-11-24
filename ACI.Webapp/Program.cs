using ACI.Webapp.Application.Models.ViewModels;
using ACI.Webapp.Application.Usecases;
using ACI.Webapp.Infrastructure.Configuration;
using ACI.Webapp.Infrastructure.Context;
using ACI.Webapp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
{
    var connectionString = builder.Configuration["Database:ConnectionString"];
    

    if(builder.Environment.IsDevelopment())
    {
        options.UseSqlite(connectionString);
    }
    else
    {
        options.UseNpgsql(connectionString);
    }
});

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDatabaseContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<AlunoRepository>();
builder.Services.AddScoped<CursoRepository>();
builder.Services.AddScoped<MatriculaRepository>();
builder.Services.AddScoped<CreateAlunoUsecase>();
builder.Services.AddScoped<GetAlunoMatriculaUsecase>();
builder.Services.AddScoped<GetCursosUsecase>();
builder.Services.AddScoped<CreateMatriculaUsecase>();
builder.Services.AddScoped<GetDashboardDataUsecase>();
builder.Services.AddScoped<EditAlunoUsecase>();
builder.Services.AddScoped<EditCursoUsecase>();
builder.Services.AddScoped<DeleteAlunoUsecase>();
builder.Services.AddScoped<DeleteCursoUsecase>();
builder.Services.AddScoped<CreateCursoUsecase>();
builder.Services.AddScoped<GetRelatoriosUsecase>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDatabaseContext>();
    context.Database.Migrate();
    await Seeder.InitializeAsync(services);
}

app.Run();