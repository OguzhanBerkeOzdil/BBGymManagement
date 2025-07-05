using System.Web;
using System.Web.Mvc;

namespace BBGymManagement.Helpers
{
    public static class LanguageHelper
    {
        // Kullanıcının seçtiği dili session'dan al, yoksa varsayılan olarak İngilizce döndür
        public static string GetCurrentLanguage()
        {
            var language = HttpContext.Current.Session["Language"] as string;
            return string.IsNullOrEmpty(language) ? "en" : language;
        }

        // Dil değiştirme işlemi - session'a kaydet
        public static void SetLanguage(string language)
        {
            HttpContext.Current.Session["Language"] = language;
        }

        // Dil koduna göre flag emoji döndür (görsel için)
        public static string GetLanguageFlag(string language)
        {
            switch (language)
            {
                case "tr":
                    return "🇹🇷";
                case "en":
                    return "🇺🇸";
                default:
                    return "🌍";
            }
        }

        // Dil adını döndür
        public static string GetLanguageName(string language)
        {
            switch (language)
            {
                case "tr":
                    return "Türkçe";
                case "en":
                    return "English";
                default:
                    return "Language";
            }
        }
    }
}
