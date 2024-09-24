## Proje Yönetimi ve Görev Takip Sistemi (Task Tracking App)

Yazılıma ara verdikten bir yıl sonra, öğrendiklerimi hatırlamak ve kendimi geliştirmek için kurumsal yazılım mimarisi üzerine hazırladım 3 aşamalı proje.  
Bu proje, bir organizasyon içinde etkili proje yönetimi ve görev ataması yapmayı sağlamak amacıyla geliştirilmiş kapsamlı bir Proje Yönetimi ve Görev Takip Sistemidir. Takımların birden fazla projeyi, görevleri, kullanıcıları ve sohbet iletişimlerini oluşturup yönetmelerini sağlar. Sistem, modern teknolojiler ve mimari en iyi uygulamalar kullanılarak ölçeklenebilirlik ve performans göz önünde bulundurularak tasarlanmıştır.

### İlgili Projeler

Projenin UI ile ilgili kısmı.

> Projenin UI kısmı tasarım aşamasındadır


---

## Özellikler

*   **Proje Yönetimi**: Ayrıntılı açıklamalar, zaman çizelgeleri ve durumlarla birden fazla proje oluşturma ve yönetme imkanı.
*   **Görev Takibi**: Kullanıcılara görev atama, öncelik belirleme, ilerleme takibi ve son tarihleri yönetme.
*   **Kullanıcı Yönetimi**: Güvenli kimlik doğrulama ve yetkilendirme mekanizmaları ile rol tabanlı erişim kontrolü.
*   **Sohbet Sistemi**: Proje iletişimi için gerçek zamanlı sohbet odaları, mesaj takibi ve kullanıcıya özgü bildirimler.
*   **Bildirimler**: Görev güncellemeleri, yeni mesajlar ve proje değişiklikleri için bildirimler alma.
*   **Denetim Kaydı**: Sistem içinde gerçekleştirilen değişiklikleri ve eylemleri izleme.
*   **Performans Optimizasyonu**: Yüksek performansı sağlamak için önbellekleme ve optimize edilmiş veri erişim stratejileri.

 
##  Kullanılan Teknolojiler

*   **.NET 8**: Uygulamanın backend'i, yüksek performanslı uygulamalar oluşturmak için .NET 8 kullanılarak inşa edilmiştir.
*   **Entity Framework Core**: LINQ tabanlı sorgu yetenekleriyle veritabanı üzerindeki veri erişimini sağlayan soyutlama.
*   **Autofac 8.1.0**: Uygulama genelinde bağımlılıkları verimli bir şekilde yönetmek ve enjekte etmek için kullanılan teknoloji.
*   **FluentValidation**: Sisteme giren tüm verilerin tanımlanmış kurallara uymasını sağlamak için model doğrulama.
*   **MemoryCache**: Sıkça erişilen verilerin bellekte saklanarak veritabanı yükünün azaltılması ve yanıt sürelerinin iyileştirilmesi.
*   **Transaction Management**: Veri bütünlüğünü korumak için tüm işlemler başarıyla tamamlanmadan veritabanına işlenmesini engelleme.
*   **Performans Aspects**: Kritik işlemlerin performansını izlemek ve optimize etmek için AOP (Aspect-Oriented Programming) kullanılarak uygulanan özel yönler.
*   **Güvenlik**: Kimlik doğrulama için JWT kullanımı, veri güvenliği için şifreleme ve hashleme araçları.  
       
     

## Mimari Genel Bakış

Proje, endişeleri ayırmak ve sürdürülebilirliği artırmak için çok katmanlı bir mimari ile organize edilmiştir:

### Ana Modüller

*   **Business**: Çekirdek iş mantığını içerir, soyut ve somut uygulamalar halinde organize edilmiştir.
    *   **Aspects**: Oriented Programming bir uygulama içerisindeki authorization, logging, caching, exception handling, validation gibi cross-cutting concern sayılan işlevlerin ayrıştırılmasıyla modülariteyi arttırmayı hedefleyen bir yaklaşımdır.
    *   **DependencyResolvers**: Autofac kullanarak bağımlılık enjeksiyonunu yapılandırır.
    *   **ValidationRules**: FluentValidation kullanarak varlıklar için doğrulama kuralları.
*   **Core**: Uygulama genelinde paylaşılan bağımsız bileşenler ve yardımcılar.
    *   **CrossCuttingConcerns**: Önbellekleme ve doğrulama gibi çapraz kesen kaygıları uygular.
    *   **Security**: Kimlik doğrulama ve yetkilendirme, JWT yönetimi ve veri şifreleme içerir.
    *   **Utilities**: Sonuç yönetimi, iş kuralları ve diğer paylaşılan işlevler için genel yardımcılar.
*   **DataAccess**: Repository pattern ve Entity Framework Core ile veri erişimini yönetir.
*   **Entities**: Sistem genelinde kullanılan domain varlıkları ve veri transfer objelerini (DTO'lar) içerir.
*   **WebAPI**: Sistemle etkileşim için RESTful endpoint'ler sağlar, frontend uygulamaları veya diğer servisler tarafından tüketilebilir.
    

### Proje Yapısı

```plaintext
├── Business
│   ├── Abstract
│   ├── Concrete
│   ├── Aspects
│   │   └── Autofac
│   ├── DependencyResolvers
│   │   └── Autofac
│   ├── ValidationRules
│       └── FluentValidation
├── Core
│   ├── Aspects
│   │   └── Autofac
│   ├── CrossCuttingConcerns
│   │   ├── Caching
│   │   └── Validation
│   ├── Entities
│   ├── Extensions
│   └── Utilities
│       ├── Business
│       ├── IaC
│       ├── Interceptors
│       ├── Results
│       ├── Security
│       │   ├── Encryption
│       │   ├── Hashing
│       │   └── JWT
│   ├── DataAccess
│       ├── EntityFramework
│   ├── DependencyResolvers
├── DataAccess
│   ├── Dependencies
│   │   ├── Abstract
│   │   └── Concrete
│   ├── EntityFramework  
├── Entities
│   ├── Abstract
│   ├── Concrete
│   └── DTOs
├── WebAPI
```

---

Nasıl Çalıştırılır?

1.  Projeyi klonlayın:
    
    ```plaintext
    git clone https://github.com/NurhatMentes/TaskTrackingApp-.NetBackendRetraining2.git
    cd TaskTrackingApp-.NetBackendRetraining2.git
    ```
    
2.  Gerekli bağımlılıkları yükleyin:
    
    ```plaintext
    dotnet restore
    ```
    
3.  Projeyi çalıştırın:
    
    ```plaintext
    dotnet run
    ```
    
    ---
    

## Katkıda Bulunanlar

*   [Nurhat Menteş](https://github.com/NurhatMentes)

## Lisans

Bu proje MIT Lisansı ile lisanslanmıştır.
