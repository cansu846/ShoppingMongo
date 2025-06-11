✅ builder.Services
ASP.NET Core uygulamasının servis (bağımlılık) koleksiyonudur.

✅ .AddScoped<TInterface, TImplementation>()
Şunu söyler:

TInterface: Kullanılmak istenen soyut arayüz (ICategoryService)

TImplementation: Bu arayüzün karşılığı olan somut sınıf (CategoryManager)

Scoped ömrü: Her HTTP isteğinde bir kez oluşturulur ve o istek boyunca kullanılır.

| Metot          | Yaşam Süresi                          | Ne Zaman Kullanılır?                     |
| -------------- | ------------------------------------- | ---------------------------------------- |
| `AddSingleton` | Uygulama boyunca tek bir örnek        | Değişmeyen veriler için (örneğin cache)  |
| `AddScoped`    | Her HTTP isteği için bir örnek        | Genellikle servis ve repository'ler için |
| `AddTransient` | Her kullanımda yeni örnek oluşturulur | Hafif servisler, geçici nesneler         |


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
ASP.NET Core uygulamasında AutoMapper kütüphanesini başlatmak ve kullanıma hazır hale getirmek için yazılır.

AutoMapper, iki farklı sınıf arasında otomatik olarak nesne eşlemesi (mapping) yapan bir kütüphanedir.

Örneğin:

DTO (Data Transfer Object) sınıflarını Entity sınıflarına dönüştürmek (veya tersi)

Form verilerini modele çevirmek

Veritabanı nesnelerini API çıktısına uygun hale getirmek

🔹 .AddAutoMapper(...)
AutoMapper kütüphanesini servis sistemine ekler.

🔹 Assembly.GetExecutingAssembly()
Şu an çalışan (yani bu kodun içinde bulunduğu) derlemeyi (assembly) temsil eder.

Bu sayede AutoMapper şunu yapar:

"Bu proje içerisindeki tüm Profile sınıflarını bul ve kullanıma hazır hale getir."

Profile sınıfı, hangi sınıfın hangisine nasıl çevrileceğini belirtir.