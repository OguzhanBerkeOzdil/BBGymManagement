using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using BBGymManagement.Models.Context;
using BBGymManagement.Models.Entities;
using BBGymManagement.Helpers;

namespace BBGymManagement.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BBGymManagement.Models.Context.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BBGymManagement.Models.Context.EFDbContext context)
        {
            // Seed verilerini burada tanımlayabilirsiniz
            context.Rols.AddOrUpdate(
                r => r.Name,
                new Rol { Name = "Admin" },
                new Rol { Name = "Customer" },
                new Rol { Name = "PersonalTrainer" }
            );

            context.SaveChanges();

            var adminRole = context.Rols.FirstOrDefault(r => r.Name == "Admin");
            if (adminRole != null)
            {
                var md5password = MD5EncryptionCustom.MD5Encryption("123321");
                context.Customers.AddOrUpdate(
                    c => c.Email,
                    new Customer
                    {
                        Name = "Administrator",
                        Surname = "Administrator",
                        Email = "admin@bbgym.com",
                        Password = md5password,
                        VerPassword = md5password,
                        RolId = adminRole.Id,
                        SecurityQuestion = "Kaç cm ?",
                        SecurityAnswer = "5cm"
                    }
                );
            }
        }
    }
}
