using Microsoft.Extensions.Options;
using ShoppingMongo.Services.CategoryService;
using ShoppingMongo.Services.CustomerService;
using ShoppingMongo.Services.EmailService;
using ShoppingMongo.Services.ProductService;
using ShoppingMongo.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//appsettings.json’daki "EmailSettings" bölümünü EmailSettings sınıfına map eder
//Bu, bir IOptions<EmailSettings> olarak saklanır.
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

//AddSingleton: Uygulama boyunca tek bir instance kullanılır (singleton). Tüm bağımlılıklar aynı instance'ı kullanır.
//AddScoped: Her HTTP isteği başına yeni bir instance oluşturulur.
//appsettings.json içindeki ayarları IEmailSettings arayüzüne sahip bir sınıfla eşleştirmek için kullanılır.
builder.Services.AddSingleton<IEmailSettings>(sp =>
    sp.GetRequiredService<IOptions<EmailSettings>>().Value);

builder.Services.AddScoped<IEmailService,EmailService>();

//ASP.NET Core'un ba??ml?l?k enjeksiyonu (Dependency Injection / DI) sisteminde bir servis kayd? yapar.
//Bu sayede bir s?n?f ba?ka bir s?n?fa ba??ml? oldu?unda, ASP.NET Core bu ba??ml?l??? otomatik olarak sa?lar.
builder.Services.AddScoped<ICategoryService,CategoryManager>();
builder.Services.AddScoped<ICustomerService,CustomerManager>();
builder.Services.AddScoped<IProductService,ProductManager>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//appsettings.json'dan okunan DatabaseSettings verisini, uygulama içinde IDatabaseSettings arayüzü üzerinden kullan?labilir hale getirmek.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettingsKey"));
//sp = Service Provider, yani ba??ml?l?klar? çözmek için kullan?lan sistemdir.
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    //IOptions<T> ASP.NET Core'un yap?land?rma sisteminde kullan?lan bir türdür.
    //Bu ça?r? ?unu der: “DatabaseSettings s?n?f?n?, appsettings.json'dan oku ve ver.”
    //Gerçek veriye .Value ile eri?irsin.
    //appsettings.json dosyas?ndan gelen ve DatabaseSettings s?n?f?na kar??l?k gelen veridir.
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(); // wwwroot içeriğini dışarıya açar

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
