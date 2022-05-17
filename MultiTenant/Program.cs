    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Multitenant.Data;
    using Multitenant.Repository;

    var builder = WebApplication.CreateBuilder(args);

    var connection = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connection));
    builder.Services.AddTransient<ITenantDataService, TenantDataService>();
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddControllersWithViews();

    builder.Services.AddScoped<ITenantDataService, TenantDataService>();

    var app = builder.Build();
    //IWebHostEnvironment env = app.Environment;

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
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Tenant}/{action=Index}/{id?}");
    app.MapRazorPages();

    app.Run();
