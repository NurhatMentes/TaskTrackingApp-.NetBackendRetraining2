﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";

        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string UserUpdated = "Kullanıcı başarıyla güncellendi";
        public static string UserRoleUpdated = "Kullanıcı rolü başarıyla güncellendi.";
        public static string UserRoleNotFound = "Kullanıcı rolü evcut değil.";

        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";
        public static string CheckIfUserIsAdmin = "Kullanıcı durumunu yalnızca Yöneticiler değiştirebilir";

        public static string InvalidOldPassword = "Güncel parola yanlış";
        public static string PasswordUpdated = "Parolanız güncellendi";



        public static string AuthorizationDenied = "Yetkiniz yok";


    }
}
