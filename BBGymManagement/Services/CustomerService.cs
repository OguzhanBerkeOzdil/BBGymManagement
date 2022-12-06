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
            var a = Get(x => x.Email == email && x.Password == password).Any();
            return a;
        }

        public bool IsAdmin(string email, string password)
        {
            var rolId = _rolService.Get(f => f.Name == "Admin").FirstOrDefault().Id;
            return Get(x => x.Email == email && x.Password == password && x.RolId == rolId).Any();
        }
    }
}