using System.Collections.Generic;

namespace BBGymManagement.Helpers
{
    public static class TextResources
    {
        // Tüm çeviriler burada saklanacak - gerçek projede bu database'den gelir ama şimdilik basit tutalım
        private static Dictionary<string, Dictionary<string, string>> translations = new Dictionary<string, Dictionary<string, string>>
        {
            // Genel kelimeler ve ifadeler
            ["welcome"] = new Dictionary<string, string> { ["en"] = "Welcome", ["tr"] = "Hoş Geldiniz" },
            ["home"] = new Dictionary<string, string> { ["en"] = "Home", ["tr"] = "Ana Sayfa" },
            ["about"] = new Dictionary<string, string> { ["en"] = "About", ["tr"] = "Hakkımızda" },
            ["contact"] = new Dictionary<string, string> { ["en"] = "Contact", ["tr"] = "İletişim" },
            ["login"] = new Dictionary<string, string> { ["en"] = "Login", ["tr"] = "Giriş Yap" },
            ["signup"] = new Dictionary<string, string> { ["en"] = "Sign Up", ["tr"] = "Kayıt Ol" },
            ["language"] = new Dictionary<string, string> { ["en"] = "Language", ["tr"] = "Dil" },
            
            // Navigation menü öğeleri
            ["gym_membership"] = new Dictionary<string, string> { ["en"] = "Gym Membership", ["tr"] = "Spor Salonu Üyeliği" },
            ["personal_trainer"] = new Dictionary<string, string> { ["en"] = "Personal Trainer", ["tr"] = "Kişisel Antrenör" },
            ["body_fat_calculator"] = new Dictionary<string, string> { ["en"] = "Body Fat Calculator", ["tr"] = "Vücut Yağ Hesaplayıcısı" },
            
            // Ana sayfa içerikleri
            ["welcome_to_bb_gym"] = new Dictionary<string, string> { ["en"] = "Welcome to BB GYM!", ["tr"] = "BB GYM'e Hoş Geldiniz!" },
            ["gym_description"] = new Dictionary<string, string> { 
                ["en"] = "You're in the right place to achieve your dream body. We offer you the best service with our professional team and modern equipment.", 
                ["tr"] = "Hayalinizdeki vücuda ulaşmak için doğru yerdesiniz. Profesyonel ekibimiz ve modern ekipmanlarımızla size en iyi hizmeti sunuyoruz." 
            },
            ["get_membership"] = new Dictionary<string, string> { ["en"] = "Get Membership", ["tr"] = "Üyelik Alın" },
            
            // Özellikler
            ["modern_equipment"] = new Dictionary<string, string> { ["en"] = "Modern Equipment", ["tr"] = "Modern Ekipmanlar" },
            ["expert_trainers"] = new Dictionary<string, string> { ["en"] = "Expert Trainers", ["tr"] = "Uzman Antrenörler" },
            ["24_7_access"] = new Dictionary<string, string> { ["en"] = "24/7 Access", ["tr"] = "7/24 Erişim" },
            ["progress_tracking"] = new Dictionary<string, string> { ["en"] = "Progress Tracking", ["tr"] = "İlerleme Takibi" },
            
            // Özellik açıklamaları
            ["modern_equipment_desc"] = new Dictionary<string, string> { 
                ["en"] = "We provide fitness training with the latest technology equipment.", 
                ["tr"] = "Son teknoloji fitness ekipmanları ile antrenman yapın." 
            },
            ["expert_trainers_desc"] = new Dictionary<string, string> { 
                ["en"] = "Reach your goals with certified personal trainers.", 
                ["tr"] = "Sertifikalı personal trainerlar ile hedeflerinize ulaşın." 
            },
            ["24_7_access_desc"] = new Dictionary<string, string> { 
                ["en"] = "Access our gym 24/7 to train whenever you want.", 
                ["tr"] = "İstediğiniz saatte antrenman yapmak için 7/24 erişim saatte." 
            },
            ["progress_tracking_desc"] = new Dictionary<string, string> { 
                ["en"] = "Track your progress and stay motivated on your fitness journey.", 
                ["tr"] = "Gelişiminizi takip edin ve motivasyonunuzu artırın." 
            },
            
            // Hızlı hizmetler başlığı
            ["quick_services"] = new Dictionary<string, string> { ["en"] = "Quick Services", ["tr"] = "Hızlı Hizmetler" },
            
            // Footer
            ["all_rights_reserved"] = new Dictionary<string, string> { ["en"] = "All rights reserved.", ["tr"] = "Tüm hakları saklıdır." },
            
            // Hakkımızda sayfası
            ["our_story"] = new Dictionary<string, string> { ["en"] = "Our Story", ["tr"] = "Hikayemiz" },
            ["bb_gym_story"] = new Dictionary<string, string> { 
                ["en"] = "BB Gym was founded in 2015 by a group of young fitness enthusiasts. Our goal is to provide an accessible, quality and motivational fitness experience for everyone.", 
                ["tr"] = "BB Gym, 2015 yılında fitness tutkunu bir grup genç tarafından kurulmuştur. Amacımız, herkese ulaşılabilir, kaliteli ve motivasyonel bir fitness deneyimi sunmaktır." 
            },
            ["our_experience"] = new Dictionary<string, string> { 
                ["en"] = "With our 8 years of experience, we have helped thousands of our members reach their fitness goals. We would be happy to see you among us with our modern equipment, expert trainers and friendly atmosphere.", 
                ["tr"] = "8 yıllık tecrübemizle, binlerce üyemizin fitness hedeflerine ulaşmasına yardımcı olduk. Modern ekipmanlarımız, uzman antrenörlerimiz ve samimi atmosferimizle sizleri de aramızda görmekten mutluluk duyarız." 
            },
            
            // Form alanları
            ["name"] = new Dictionary<string, string> { ["en"] = "Name", ["tr"] = "Ad" },
            ["surname"] = new Dictionary<string, string> { ["en"] = "Surname", ["tr"] = "Soyad" },
            ["email"] = new Dictionary<string, string> { ["en"] = "Email", ["tr"] = "E-posta" },
            ["phone"] = new Dictionary<string, string> { ["en"] = "Phone", ["tr"] = "Telefon" },
            ["message"] = new Dictionary<string, string> { ["en"] = "Message", ["tr"] = "Mesaj" },
            ["send"] = new Dictionary<string, string> { ["en"] = "Send", ["tr"] = "Gönder" },
            
            // Butonlar
            ["learn_more"] = new Dictionary<string, string> { ["en"] = "Learn More", ["tr"] = "Daha Fazla Bilgi" },
            ["get_started"] = new Dictionary<string, string> { ["en"] = "Get Started", ["tr"] = "Başlayın" },
            ["join_now"] = new Dictionary<string, string> { ["en"] = "Join Now", ["tr"] = "Hemen Katıl" },
            
            // İletişim sayfası
            ["how_can_we_help"] = new Dictionary<string, string> { ["en"] = "How can we help you?", ["tr"] = "Size nasıl yardımcı olabiliriz?" },
            ["send_us_message"] = new Dictionary<string, string> { ["en"] = "Send Us a Message", ["tr"] = "Bize Mesaj Gönderin" },
            ["subject"] = new Dictionary<string, string> { ["en"] = "Subject", ["tr"] = "Konu" },
            ["get_in_touch"] = new Dictionary<string, string> { ["en"] = "Get In Touch", ["tr"] = "İletişime Geçin" },
            ["visit_us"] = new Dictionary<string, string> { ["en"] = "Visit Us", ["tr"] = "Bizi Ziyaret Edin" },
            ["our_address"] = new Dictionary<string, string> { ["en"] = "Our Address", ["tr"] = "Adresimiz" },
            ["call_us"] = new Dictionary<string, string> { ["en"] = "Call Us", ["tr"] = "Bizi Arayın" },
            ["email_us"] = new Dictionary<string, string> { ["en"] = "Email Us", ["tr"] = "E-posta Gönderin" },
            ["working_hours"] = new Dictionary<string, string> { ["en"] = "Working Hours", ["tr"] = "Çalışma Saatleri" },
            ["mon_fri"] = new Dictionary<string, string> { ["en"] = "Monday - Friday", ["tr"] = "Pazartesi - Cuma" },
            ["sat_sun"] = new Dictionary<string, string> { ["en"] = "Saturday - Sunday", ["tr"] = "Cumartesi - Pazar" },
            
            // Sayfalar arası genel
            ["success"] = new Dictionary<string, string> { ["en"] = "Success", ["tr"] = "Başarılı" },
            ["error"] = new Dictionary<string, string> { ["en"] = "Error", ["tr"] = "Hata" },
            ["warning"] = new Dictionary<string, string> { ["en"] = "Warning", ["tr"] = "Uyarı" },
            ["info"] = new Dictionary<string, string> { ["en"] = "Info", ["tr"] = "Bilgi" }
        };

        // Çeviri al - dil kodu ve anahtar ile
        public static string Get(string key, string language = "en")
        {
            // Eğer anahtar bulunamazsa, anahtarın kendisini döndür (development sırasında kolay debug için)
            if (!translations.ContainsKey(key))
                return $"[{key}]"; // Missing translation indicator
            
            // Eğer istenen dil bulunamazsa, İngilizce'yi döndür
            if (!translations[key].ContainsKey(language))
                return translations[key].ContainsKey("en") ? translations[key]["en"] : $"[{key}]";
            
            return translations[key][language];
        }

        // Mevcut dil için çeviri al (session'dan dil bilgisini otomatik alır)
        public static string Get(string key)
        {
            var currentLanguage = LanguageHelper.GetCurrentLanguage();
            return Get(key, currentLanguage);
        }
    }
}
