namespace Business.Constants
{
    public static class Messages
    {
        //Auth
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Şifre hatalı.";
        public static string SuccessfulLogin = "Sisteme giriş başarılı.";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu.";

        //User
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcu.t";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi.";
        public static string UserUpdated = "Kullanıcı başarıyla güncellendi.";
        public static string UserRoleUpdated = "Kullanıcı rolü başarıyla güncellendi.";
        public static string UserRoleNotFound = "Kullanıcı rolü evcut değil.";
        public static string InvalidOldPassword = "Güncel parola yanlış.";
        public static string PasswordUpdated = "Parolanız güncellendi.";
        public static string CheckIfUserIsAdmin = "Kullanıcı durumunu yalnızca Yöneticiler değiştirebilir.";
        public static string AuthorizationDenied = "Yetkiniz yok.";

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

        //ChatRoom
        public static string ChatRoomCreated = "Sohbet odası oluşturuldu.";
        public static string ChatRoomUpdated = "Sohbet odası güncellendi.";
        public static string ChatRoomDeleted = "Sohbet odası silindi.";
        public static string ChatRoomNotFound = "Sohbet odası bulunamadı.";
        public static string ChatRoomNameRequired = "Sohbet odası adı gereklidir.";
        public static string ChatRoomNameTooLong = "Sohbet odası adı en fazla 100 karakter olmalıdır.";

        // ChatRoomUser
        public static string ChatRoomUserAdded = "Kullanıcı sohbet odasına eklendi.";
        public static string ChatRoomUserDeleted = "Kullanıcı sohbet odasından silindi.";
        public static string ChatRoomUserNotFound = "Kullanıcı sohbet odasında bulunamadı.";
        public static string ChatRoomUserFound = "Kullanıcı sohbet odasında bulundu.";
        public static string ChatRoomUsersListed = "Sohbet odası kullanıcıları listelendi.";
        public static string ChatRoomUserAlreadyExists = "Bu kullanıcı zaten sohbet odasına kayıtlı.";
        public static string InvalidUserId = "Geçersiz kullanıcı ID'si.";
        public static string InvalidChatRoomId = "Geçersiz sohbet odası ID'si.";

        // Message
        public static string MessageAdded = "Mesaj gönderildi.";
        public static string MessageUpdated = "Mesaj güncellendi.";
        public static string MessageDeleted = "Mesaj silindi.";
        public static string MessageNotFound = "Mesaj bulunamadı.";
        public static string MessageNotFoundUser = "Mesajınız bulunamadı.";
        public static string MessagesNotFoundInChatRoom = "Sohbet de mesaj bulunamadı.";
        public static string MessageUpdateNotAllowed = "Mesaj sadece 5 dakika içinde güncellenebilir.";
        public static string MessageUpdateNotFound = "Güncellenmek istenen mesaj bulunamadı.";
        public static string MessageContentCannotBeEmpty = "Mesaj içeriği boş olamaz.";
        public static string MessageContentLength = "Mesaj içeriği 1 ile 1000 karakter arasında olmalıdır.";
        public static string InvalidChatRoom = "Geçersiz sohbet odası.";
        public static string InvalidUser = "Geçersiz kullanıcı.";
    }
}
