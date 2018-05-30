using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using IdentityManagement.WebProviders;
namespace IdentityManagement.Domain
{
    public class RBACMembershipUser : MembershipUser
    {
        public RBACMembershipUser(RBACMembershipProvider _Provider,int id ,string name, 
            string email, string firstname ,string familyname, string passwordQuestion, string comment, bool isApproved, 
            bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate,
            DateTime lastPasswordChangedDate, DateTime lastLockoutDate)
        {
            _MembershipProvider = _Provider;
            _UserName = name;
            _Email = email;
            _PasswordQuestion = passwordQuestion;
            _Comment = comment;
            _IsApproved = isApproved;
            _IsLockedOut = isLockedOut;
            _CreationDate = creationDate;
            _LastLoginDate = lastLoginDate;
            _LastActivityDate = lastActivityDate;
            _LastPasswordChangedDate = lastPasswordChangedDate;
            _LastLockoutDate = lastLockoutDate;
 
        }
        private RBACMembershipProvider _MembershipProvider;
        string _Comment;
        DateTime _CreationDate;
        string _Email;
        bool _IsApproved;
        bool _IsLockedOut;
        DateTime _LastActivityDate;
        DateTime _LastLockoutDate;
        DateTime _LastLoginDate;
        DateTime _LastPasswordChangedDate;
        string _PasswordQuestion;
        string _ProviderName;
        object _ProviderUserKey;
        string _UserName;

        string _FirtName;

        public string FirtName
        {
            get { return _FirtName; }
        }
        string _FamilyName;

        public string FamilyName
        {
            get { return _FamilyName; }
        }

        int _ID;

        public int ID
        {
            get { return _ID; }
        }

        // Summary:
        //     Gets or sets application-specific information for the membership user.
        //
        // Returns:
        //     Application-specific information for the membership user.
        public override string Comment { get { return _Comment; } set { _Comment = value; } }
        //
        // Summary:
        //     Gets the date and time when the user was added to the membership data store.
        //
        // Returns:
        //     The date and time when the user was added to the membership data store.
        public override DateTime CreationDate { get { return _CreationDate; } }
        //
        // Summary:
        //     Gets or sets the e-mail address for the membership user.
        //
        // Returns:
        //     The e-mail address for the membership user.
        public override string Email { get { return _Email; } set { _Email = value; } }
        //
        // Summary:
        //     Gets or sets whether the membership user can be authenticated.
        //
        // Returns:
        //     true if the user can be authenticated; otherwise, false.
        public override bool IsApproved { get { return _IsApproved; } set { _IsApproved = value; } }
        //
        // Summary:
        //     Gets a value indicating whether the membership user is locked out and unable
        //     to be validated.
        //
        // Returns:
        //     true if the membership user is locked out and unable to be validated; otherwise,
        //     false.
        public override bool IsLockedOut { get { return _IsLockedOut; } }
        //
        // Summary:
        //     Gets or sets the date and time when the membership user was last authenticated
        //     or accessed the application.
        //
        // Returns:
        //     The date and time when the membership user was last authenticated or accessed
        //     the application.
        public override DateTime LastActivityDate { get { return _LastActivityDate; } set { _LastActivityDate = value; } }
        //
        // Summary:
        //     Gets the most recent date and time that the membership user was locked out.
        //
        // Returns:
        //     A System.DateTime object that represents the most recent date and time that
        //     the membership user was locked out.
        public override DateTime LastLockoutDate { get { return _LastLockoutDate; } }
        //
        // Summary:
        //     Gets or sets the date and time when the user was last authenticated.
        //
        // Returns:
        //     The date and time when the user was last authenticated.
        public override DateTime LastLoginDate { get { return _LastLoginDate; } set { _LastLoginDate = value; } }
        //
        // Summary:
        //     Gets the date and time when the membership user's password was last updated.
        //
        // Returns:
        //     The date and time when the membership user's password was last updated.
        public override DateTime LastPasswordChangedDate { get { return _LastPasswordChangedDate; } }
        //
        // Summary:
        //     Gets the password question for the membership user.
        //
        // Returns:
        //     The password question for the membership user.
        public override string PasswordQuestion { get { return _PasswordQuestion; } }
        //
        // Summary:
        //     Gets the name of the membership provider that stores and retrieves user information
        //     for the membership user.
        //
        // Returns:
        //     The name of the membership provider that stores and retrieves user information
        //     for the membership user.
        public override string ProviderName { get { return _ProviderName; } }
        //
        // Summary:
        //     Gets the user identifier from the membership data source for the user.
        //
        // Returns:
        //     The user identifier from the membership data source for the user.
        public override object ProviderUserKey { get { return _ProviderUserKey; } }
        //
        // Summary:
        //     Gets the logon name of the membership user.
        //
        // Returns:
        //     The logon name of the membership user.
        public override string UserName { get { return _UserName; } }

        // Summary:
        //     Updates the password for the membership user in the membership data store.
        //
        // Parameters:
        //   oldPassword:
        //     The current password for the membership user.
        //
        //   newPassword:
        //     The new password for the membership user.
        //
        // Returns:
        //     true if the update was successful; otherwise, false.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     oldPassword is an empty string.  -or- newPassword is an empty string.
        //
        //   System.ArgumentNullException:
        //     oldPassword is null.  -or- newPassword is null.
        public override bool ChangePassword(string oldPassword, string newPassword)
        {
            throw new Exception("this method is not implement.");
        }
        //
        // Summary:
        //     Updates the password question and answer for the membership user in the membership
        //     data store.
        //
        // Parameters:
        //   password:
        //     The current password for the membership user.
        //
        //   newPasswordQuestion:
        //     The new password question value for the membership user.
        //
        //   newPasswordAnswer:
        //     The new password answer value for the membership user.
        //
        // Returns:
        //     true if the update was successful; otherwise, false.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     password is an empty string.  -or- newPasswordQuestion is an empty string.
        //      -or- newPasswordAnswer is an empty string.
        //
        //   System.ArgumentNullException:
        //     password is null.
        public override bool ChangePasswordQuestionAndAnswer(string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new Exception("this method is not implement.");
        }
        //
        // Summary:
        //     Gets the password for the membership user from the membership data store.
        //
        // Returns:
        //     The password for the membership user.
        public override string GetPassword()
        {
            throw new Exception("this method is not implement.");
        }
        //
        // Summary:
        //     Gets the password for the membership user from the membership data store.
        //
        // Parameters:
        //   passwordAnswer:
        //     The password answer for the membership user.
        //
        // Returns:
        //     The password for the membership user.
        public override string GetPassword(string passwordAnswer)
        {
            throw new Exception("this method is not implement.");
        }
        //
        // Summary:
        //     Resets a user's password to a new, automatically generated password.
        //
        // Returns:
        //     The new password for the membership user.
        public override string ResetPassword()
        {
            throw new Exception("this method is not implement.");
        }
        //
        // Summary:
        //     Resets a user's password to a new, automatically generated password.
        //
        // Parameters:
        //   passwordAnswer:
        //     The password answer for the membership user.
        //
        // Returns:
        //     The new password for the membership user.
        public override string ResetPassword(string passwordAnswer)
        {
            throw new Exception("this method is not implement.");
        }
        //
        // Summary:
        //     Returns the user name for the membership user.
        //
        // Returns:
        //     The System.Web.Security.MembershipUser.UserName for the membership user.
        public override string ToString()
        {
            throw new Exception("this method is not implement.");
        }
        //
        // Summary:
        //     Clears the locked-out state of the user so that the membership user can be
        //     validated.
        //
        // Returns:
        //     true if the membership user was successfully unlocked; otherwise, false.
        public override bool UnlockUser()
        {
            throw new Exception("this method is not implement.");
        }
    }
    
}
