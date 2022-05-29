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
#region Dynamic Context Used

//using (var scope = app.Services.CreateScope())
//using (var context = scope.ServiceProvider.GetService<ApplicationDbContext>())
//{
//    context.Database.Migrate();

//    long count = context.TenantInfo.Count();
//    long modu = (count % 10);

//    bool isNeedNewDB = count % 10 == 0 ? true : false;
//    var DbCount = ((count / 10) + 1);
//    var DatabaseName = "DB" + DbCount.ToString();

//    var dynamicConnectionString = $"Server=DESKTOP-87Q4097;Database={DatabaseName};Trusted_Connection=True;MultipleActiveResultSets=true";
//    var dynamicContext = new ApplicationDbContext(dynamicConnectionString);
//    if (dynamicContext.Database.EnsureCreated())
//    {
//        dynamicContext.Database.Migrate();
//    }
//}

#endregion
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





