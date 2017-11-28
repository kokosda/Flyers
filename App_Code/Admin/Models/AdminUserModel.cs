using System;
using System.Collections;
using System.Data;
using System.Web;

namespace FlyerMe.Admin.Models
{
    [Serializable]
    public sealed class AdminUserModel
    {
        public AdminUserModel(String email, String firstName, String lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public String Email { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public static AdminUserModel ToAdminUserModel(DataTable dt)
        {
            var result = new AdminUserModel(dt.Rows[0]["UserEmailID"] as String, dt.Rows[0]["FirstName"] as String, dt.Rows[0]["LastName"] as String);

            return result;
        }

        public static AdminUserModel SetAdminUserModelToSession(DataTable dt)
        {
            var result = ToAdminUserModel(dt);

            HttpContext.Current.Session.Add(GetKey(), result);

            return result;
        }

        public static AdminUserModel GetAdminUserModelFromSession()
        {
            AdminUserModel result = null;
            
            try
            {
                result = HttpContext.Current.Session[GetKey()] as AdminUserModel;
            }
            catch (HttpUnhandledException)
            {
                RemoveAdminUserModelFromSession();
            }

            return result;
        }

        public static void RemoveAdminUserModelFromSession()
        {
            HttpContext.Current.Session.Remove(GetKey());
        }

        #region private

        private static String GetKey()
        {
            return "AdminUser";
        }

        #endregion
    }
}