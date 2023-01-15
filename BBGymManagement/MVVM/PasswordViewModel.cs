using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBGymManagement.MVVM
{
    public class PasswordViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirmation { get; set; }
        public string SecurityAnswer { get; set; }
        public string SecurityQuestion { get; set; }
    }
}