using serversideproject.Areas.Database.Models;
using serversideproject.Codes;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using serversideproject.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IHashingexamples, Hashingexamples>();

//Connection strings 
var testDbConString = builder.Configuration.GetConnectionString("TestDbConString");
var identityConString = builder.Configuration.GetConnectionString("serversideprojectContextConnection");

//Entity framework connection
builder.Services.AddDbContext<TestContext>(options =>
    options.UseSqlServer(testDbConString));

//Identity framework autogenerated
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<serversideprojectContext>();
builder.Services.AddDbContext<serversideprojectContext>(options => options.UseSqlServer(identityConString));
builder.Services.AddRazorPages();

//Add a policy to require login/authentication on certain pages/views
builder.Services.AddAuthorization(options => { options.AddPolicy("RequireAuthenticatedUser", policy => { policy.RequireAuthenticatedUser(); }); });

//Cryptography integration
builder.Services.AddDataProtection();
//This is the default algorithms for the cryptographic integration.
//builder.Services.AddDataProtection().UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
//{
//    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
//    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
//});

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
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
