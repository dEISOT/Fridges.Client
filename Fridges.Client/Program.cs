using Fridges.Client.Services;
using Fridges.Client.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAssortmentService, Assortmentservice>();
builder.Services.AddScoped<IFridgeService, FridgeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddTransient<UnathorizedHandler>();

builder.Services.AddHttpClient<IFridgeService, FridgeService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7183");

});
//builder.Services.AddHttpClient<IAssortmentService, Assortmentservice>(client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7183");

//});
builder.Services.AddHttpClient<IAccountService, AccountService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7183");

});

builder.Services.AddHttpClient<IHttpFridgeClient, HttpFridgeClient>()
    .AddHttpMessageHandler<UnathorizedHandler>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Fridge}/{action=Index}");

app.Run();
