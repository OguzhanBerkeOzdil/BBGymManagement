using BBGymManagement.Helpers;
using BBGymManagement.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBGymManagement.Services
{
    public class CustomerService : Repository<Customer>
    {
        RolService _rolService = new RolService();
        public bool CheckEmail(string email)
        {
            return GetAll().Any(x => x.Email.Equals(email));
        }

        public bool IsCustomer(string email, string password)
        {
            var md5password = MD5EncryptionCustom.MD5Encryption(password);
            return Get(x => x.Email == email && x.Password == md5password).Any();
        }

        public bool IsAdmin(string email, string password)
        {
            var rolId = _rolService.Get(f => f.Name == "Admin").FirstOrDefault().Id;
            var md5password= MD5EncryptionCustom.MD5Encryption(password);
            return Get(x => x.Email == email && x.Password == md5password && x.RolId == rolId).Any();
        }

    }
}