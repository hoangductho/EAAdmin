using FX.Utils.MvcPaging;
using IdentityManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Teca.Admin.Models
{

    public class AccountModels
    {
        public user tmpUser { get; set; }
        public string RetypePassword { get; set; }
        public string[] AllRoles { get; set; }
        public string[] UserRoles { get; set; }  
    }

    public class IndexAccountModel
    {
        public string username { get; set; }
        public IPagedList<user> PageListUser;
    }

    public class ChangePasswordModel
    {
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Current password")]
        public string oldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm new password")]
        public string RetypePassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }

        public string lblErrorMessage { get; set; }        
    }

    public class Captcha
    {
        [Display(Name = "Captcha", Order = 20)]
        [Remote("ValidateCaptcha", "Captcha", "", ErrorMessage = "ErrorMessage")]
        [Required(ErrorMessage = "Required")]
        public virtual string captch { get; set; }
    }

    public class RoleModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public List<permission> Permissions { get; set; }
    }
}
