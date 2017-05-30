using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChefBot
{
    public class SessionHelper
    {
        private static void Set(string key, object value)
        {
            try
            {
                if (HttpContext.Current.Session[key] == null)
                    HttpContext.Current.Session.Add(key, value);
            }
            catch { }
        }

        private static T Get<T>(string key)
        {
            try
            {
                return (T)HttpContext.Current.Session[key];
            }
            catch
            {
                return default(T);
            }
        }

        public static Models.UserSessionModel User
        {
            get
            {
                return Get<Models.UserSessionModel>("AdminUser");
            }
            set
            {
                Set("AdminUser", value);
            }
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}