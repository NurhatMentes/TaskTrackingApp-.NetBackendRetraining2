﻿namespace Business.Constants
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

    }
}
