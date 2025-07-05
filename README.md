# 💪 BB Gym Management System

Modern, responsive and multilingual gym management system.

## 👨‍💻 Developer

**Oğuzhan Berke Özdil**

- 🎓 AGH University of Science and Technology - Computer Science Department, 2nd Year
- 📅 Initial Development: 2022
- 🔄 Major Update: July 2025 (Complete modernization and multilingual support)

## 📖 About the Project

This project was developed in 2022 as part of the "Web Project Development" course for 2nd year Computer Science students at AGH University. The system, built using the ASP.NET MVC framework, offers comprehensive features for both customers and administrators.

## ✨ Features

### 🌍 Multilingual Support

- 🇹🇷 Turkish
- 🇺🇸 English
- Session-based language management
- Dynamic language switching

### 🎨 Modern UI/UX

- Responsive design (Mobile-first approach)
- Modern Bootstrap 5.3.3 usage
- Font Awesome 6.0 icon set
- Gradient colors and shadow effects
- Smooth animations and hover effects

### 👥 User Management

- Customer registration and login system
- Admin panel
- Role-based authorization
- MD5 encryption
- Form validation

### 🏋️ Gym Features

- Membership package management
- Personal trainer services
- Body fat percentage calculator
- Contact form
- Category-based product management

### 🛡️ Security

- SQL injection protection (Entity Framework)
- XSS protection
- CSRF token usage
- Secure password policies

## 🛠️ Technologies

### Backend

- **Framework:** ASP.NET MVC 5.2.9
- **Database:** Entity Framework 6.4.4 (Code First)
- **Authentication:** Custom authentication system
- **Encryption:** MD5 password hashing

### Frontend

- **CSS Framework:** Bootstrap 5.3.3
- **JavaScript:** jQuery 3.7.1
- **Icons:** Font Awesome 6.0
- **Validation:** jQuery Validation 1.19.5
- **Bundling:** ASP.NET Web Optimization

### Additional Libraries

- **QR Code:** QRCoder 1.4.3
- **Scheduling:** Quartz.NET 3.5.0
- **JSON:** Newtonsoft.Json 13.0.3

## 🚀 Installation

### Requirements

- Visual Studio 2019 or later
- .NET Framework 4.8.1
- SQL Server (LocalDB supported)
- IIS Express

### Steps

1. Clone the repository
```bash
git clone [repository-url]
```

2. Open the solution file in Visual Studio
```
BBGymManagement.sln
```

3. Restore NuGet packages
```
Tools > NuGet Package Manager > Package Manager Console
Update-Package -Reinstall
```

4. Update connection string (Web.config)
```xml
<connectionStrings>
    <add name="EFDbContext" 
         connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BBGymDB;Integrated Security=True" 
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

5. Run database migrations
```
Enable-Migrations
Update-Database
```

6. Run the project (F5)

## 👤 Admin Login

**Email:** admin@bbgym.com  
**Password:** 123321

## 📁 Project Structure

```
BBGymManagement/
├── 📁 Areas/Admin/          # Admin panel
├── 📁 Controllers/          # MVC Controllers
├── 📁 Models/               # Entity models and ViewModels
├── 📁 Views/                # Razor view files
├── 📁 Content/              # CSS and static files
├── 📁 Scripts/              # JavaScript files
├── 📁 Services/             # Business logic layer
├── 📁 Helpers/              # Helper classes
├── 📁 Migrations/           # EF Code First migrations
└── 📁 App_Data/             # Database files
```

## 🔄 2025 Updates

### 🎯 Major Updates

- ✅ **Full Responsive Design:** All pages made responsive with mobile-first approach
- ✅ **Complete Multilingual System:** Full English-Turkish language support with dynamic switching
- ✅ **Modern UI/UX:** Upgraded to Bootstrap 5.3.3, modern colors and animations  
- ✅ **Security Improvements:** XSS protection, updated NuGet packages
- ✅ **Code Refactoring:** Clean code principles, Turkish comments
- ✅ **Admin Panel Complete Overhaul:** All admin pages modernized and made multilingual

### 🛠️ Technical Improvements

- ✅ **Package Updates:** All NuGet packages updated with security vulnerabilities fixed
- ✅ **Database Optimization:** Connection string and EF configuration improvements
- ✅ **Performance:** CSS/JS bundling and minification optimizations
- ✅ **Accessibility:** ARIA labels and keyboard navigation support
- ✅ **Action Button Fix:** Resolved HTML encoding issues in admin action buttons  
- ✅ **Language Routing:** Fixed language switching in admin panel
- ✅ **CRUD Operations:** All Create, Read, Update, Delete operations working perfectly

### 🎨 UI/UX Enhancements

- ✅ **Modern Navbar:** Responsive menu system and language selector
- ✅ **Card-based Layout:** Modern card designs and hover effects
- ✅ **Form Improvements:** Enhanced form validation and user feedback
- ✅ **Color Scheme:** Consistent color palette and gradient usage
- ✅ **Typography:** Modern font stack and readability improvements
- ✅ **Admin Panel:** Complete dashboard redesign with statistics and modern interface
- ✅ **PersonalTrainer Page:** Fully redesigned with static trainer cards and modern layout
- ✅ **Icon Integration:** FontAwesome icons properly displaying in all action buttons

### 🏛️ Admin Panel Features

- ✅ **Modern Dashboard:** Statistics cards, activity feed, and quick actions (Full multilingual)
- ✅ **Customer Management:** Visual customer list with search and filtering (Full multilingual)
- ✅ **Product Management:** Card-based product display with images (Full multilingual)
- ✅ **Order Management:** Order tracking with status indicators (Full multilingual)
- ✅ **Role Management:** Visual role cards with permission display (Full multilingual)
- ✅ **Responsive Design:** Mobile-friendly admin interface
- ✅ **Action Buttons:** Modern icon-based action buttons with proper rendering
- ✅ **Language Consistency:** All admin pages support Turkish/English switching
- ✅ **Form Modernization:** All Create/Edit forms redesigned with modern UI

## 📱 Pages

### 🏠 Home Page

- Hero section
- Feature cards
- Call-to-action buttons

### 🔐 Authentication

- Modern login form
- Registration page
- Password strength indicator
- Form validation

### 💪 Gym Services

- Membership packages
- Personal trainer profiles with expertise areas
- Body fat percentage calculator

### 🎯 Admin Panel

- **Dashboard:** Overview statistics, recent activities, system info
- **Customer Management:** User management with advanced filtering
- **Product Management:** Visual product catalog management
- **Order Management:** Order tracking and status management
- **Role Management:** Permission-based access control

### 📞 Contact

- Contact form
- Location information
- Social media links

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Create a Pull Request

## 📄 License

This project was developed for educational purposes. Created as part of AGH University Computer Science coursework.

## � Developer Contact

### Oğuzhan Berke Özdil

- 🎓 AGH University - Computer Science
- 💼 https://www.linkedin.com/in/oguzhanberkeozdil/

---

### 🏆 Project Achievements

- ✅ Modern and responsive web design
- ✅ Multilingual support implementation
- ✅ Secure authentication system
- ✅ Clean code and best practices
- ✅ Cross-browser compatibility
- ✅ SEO-friendly URL structure

**Development Process:** Started in 2022 and updated to modern web standards in 2025, this project reflects technical growth and the learning process in web development.

---
⭐ If you liked this project, don't forget to give it a star!
