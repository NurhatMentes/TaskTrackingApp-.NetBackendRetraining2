namespace Business.Constants
{
    public static class Messages
    {
        //Auth
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";

        //User
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string UserUpdated = "Kullanıcı başarıyla güncellendi";
        public static string UserRoleUpdated = "Kullanıcı rolü başarıyla güncellendi.";
        public static string UserRoleNotFound = "Kullanıcı rolü evcut değil.";
        public static string InvalidOldPassword = "Güncel parola yanlış";
        public static string PasswordUpdated = "Parolanız güncellendi";
        public static string CheckIfUserIsAdmin = "Kullanıcı durumunu yalnızca Yöneticiler değiştirebilir";
        public static string AuthorizationDenied = "Yetkiniz yok";

        //Project
        public static string ProjectAdded = "Proje başarıyla eklendi.";
        public static string ProjectUpdated = "Proje başarıyla güncellendi.";
        public static string ProjectNotFound = "Proje bulunamadı.";
        public static string ProjectStatusChanged = "Proje durumu başarıyla değiştirildi.";
        public static string ProjectNameNotEmpty = "Proje adı boş olamaz.";
        public static string ProjectNameMinLength = "Proje adı en az 3 karakter uzunluğunda olmalıdır.";
        public static string ProjectDescriptionMaxLength = "Proje açıklaması en fazla 1000 karakter uzunluğunda olabilir.";
        public static string ProjectStartDateNotEmpty = "Başlangıç tarihi boş olamaz.";
        public static string ProjectStartDateGreaterThanNow = "Başlangıç tarihi bugünden daha eski olamaz.";
        public static string ProjectEndDateGreaterThanStartDate = "Bitiş tarihi, başlangıç tarihinden sonra olmalıdır.";
        public static string ProjectStatusNotEmpty = "Proje durumu boş olamaz.";
        public static string ProjectStatusInvalid = "Geçerli bir durum seçmelisiniz.";
        public static string CreatedByUserIdNotEmpty = "Proje oluşturan kullanıcı ID'si boş olamaz.";

        //ProjectUser
        public static string ProjectUserNotFound = "Proje kullanıcısı bulunamadı.";
        public static string ProjectUserAdded = "Proje kullanıcısı başarıyla eklendi.";
        public static string ProjectUserUpdated = "Proje kullanıcısı başarıyla güncellendi.";
        public static string ProjectIdCannotBeEmpty = "Proje Id'si boş olamaz.";
        public static string ProjectIdMustBeGreaterThanZero = "Proje ID'si sıfırdan büyük olmalıdır.";
        public static string UserIdCannotBeEmpty = "Kullanıcı Kimliği boş olamaz.";
        public static string UserIdMustBeGreaterThanZero = "Kullanıcı kimliği sıfırdan büyük olmalıdır.";
        public static string RoleCannotBeEmpty = "Rol boş olamaz.";
        public static string RoleLengthMustBeBetween1And100 = "Rol uzunluğu 1 ile 100 karakter arasında olmalıdır.";

        //Task
        public static string TaskAdded = "Görev başarıyla eklendi.";
        public static string TaskUpdated = "Görev başarıyla güncellendi.";
        public static string TaskDeleted = "Görev başarıyla silindi.";
        public static string TaskListed = "Görevler başarıyla listelendi.";
        public static string TaskNotFound = "Görev bulunamadı.";
        public static string TaskNameRequired = "Görev adı zorunludur.";
        public static string TaskDescriptionMaxLength = "Görev açıklaması en fazla 500 karakter olabilir.";
        public static string TaskEndDateRequired = "Görev bitiş tarihi zorunludur.";
        public static string TaskEndDateInFuture = "Görev bitiş tarihi gelecekte bir tarih olmalıdır.";
        public static string TaskStatusRequired = "Görev durumu zorunludur.";
        public static string TaskPriorityInvalid = "Görev önceliği geçersiz. 'Düşük', 'Orta' veya 'Yüksek' seçilmelidir.";
    }
}
