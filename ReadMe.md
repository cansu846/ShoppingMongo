# ShoppingMongo
🛍️ ShoppingMongo — Modern E-Ticaret Web Uygulaması
ShoppingMongo, .NET 9.0 ile geliştirilmiş, MongoDB veritabanı altyapısını kullanan dinamik ve kullanıcı dostu bir e-ticaret web uygulamasıdır. MVC (Model-View-Controller) mimarisiyle inşa edilen bu projede ürün yönetimi, kategori filtreleme, arama ve e-posta bildirim özellikleri gibi temel ihtiyaçlara modern çözümler sunulmuştur.

🚀 Özellikler
🔧 Admin Paneli
Yetkili kullanıcılar, admin paneli üzerinden kategori, ürün ve içerik yönetimini kolayca gerçekleştirebilir.

🗂️ Ürün Listeleme ve Kategoriye Göre Filtreleme
Tüm ürünler modern bir arayüzle listelenir. Kullanıcılar, belirli kategorilere göre filtreleme yaparak aradıkları ürünleri hızlıca bulabilir.

🔍 Anahtar Kelime ile Ürün Arama
Gelişmiş arama özelliği sayesinde kullanıcılar arama kutusuna girdikleri kelimelerle ilgili ürünlere kolayca erişebilir.

📬 Bülten Aboneliği ve E-Posta Bildirimi
Kullanıcılar e-posta adreslerini bırakarak kampanya ve fırsatlardan haberdar olabilir. Bu işlem için güvenilir ve esnek bir e-posta kütüphanesi olan MailKit kullanılmıştır.

♻️ Dependency Injection ile Modüler Mimari
Projede bağımlılıklar minimuma indirilmiş ve tüm servisler Dependency Injection (bağımlılık enjeksiyonu) prensibiyle yönetilmiştir. Bu sayede test edilebilirlik, ölçeklenebilirlik ve sürdürülebilirlik artırılmıştır.

🧩 Katmanlı Mimari ve Temiz Kod Yaklaşımı
Kod yapısı, servisler, controller'lar, DTO'lar ve veritabanı işlemleri arasında açık bir ayrım sunar. SOLID prensiplerine uyumlu bir yapı tercih edilmiştir.

🛠️ Kullanılan Teknolojiler
ASP.NET Core 9.0

MongoDB (NoSQL)

MVC Mimarisi

MailKit – E-posta gönderimleri için

AutoMapper – DTO dönüşümleri için

Dependency Injection – Servis bağımlılık yönetimi için


# Projede kullanılan özelliklerin Açıklamaları

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

![Screenshot 2025-06-13 231159](https://github.com/user-attachments/assets/5b9fcf27-c79b-4671-a2a9-7ce60eb68d1d)
![Screenshot 2025-06-13 231144](https://github.com/user-attachments/assets/07dc0d33-ef61-4396-a1d3-48e6ff224a14)
![Screenshot 2025-06-13 231746](https://github.com/user-attachments/assets/4c5aed9b-ee8d-4108-87af-be3e6c694530)
![Screenshot 2025-06-13 231716](https://github.com/user-attachments/assets/17fc123f-71f8-4200-bbb8-978a78c1ccf8)
![Screenshot 2025-06-13 231621](https://github.com/user-attachments/assets/d42f3f28-3669-4cff-92da-1cb06c7f8e6e)
![Screenshot 2025-06-13 231608](https://github.com/user-attachments/assets/45348518-936b-41c4-8b49-dc34a497b660)
![Screenshot 2025-06-13 231441](https://github.com/user-attachments/assets/5fa0b16a-7d7c-4693-a300-5f5c1c90ecb9)
![Screenshot 2025-06-13 231422](https://github.com/user-attachments/assets/3a69c909-5588-4320-89eb-a75708939f69)
![Screenshot 2025-06-13 231249](https://github.com/user-attachments/assets/1722be05-d0fb-4033-98f1-487567996039)
![Screenshot 2025-06-13 231813](https://github.com/user-attachments/assets/ac416759-be18-4557-af6b-eef44989a940)

