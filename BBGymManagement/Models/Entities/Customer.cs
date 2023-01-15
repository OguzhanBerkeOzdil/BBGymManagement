using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBGymManagement.Models.Entities
{
    public class Customer:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string VerPassword { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public int RolId { get; set; }
        
    }
}