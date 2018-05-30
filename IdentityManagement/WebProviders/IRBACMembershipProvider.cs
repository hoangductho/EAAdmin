using System;
using System.Collections.Generic;
using IdentityManagement.Domain;
using System.Web.Security;
namespace IdentityManagement.WebProviders
{
    public interface IRBACMembershipProvider
    {
        user CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out string status);
        bool DeleteUser(int userid, bool deleteAllRelatedData);
        bool DeleteUser(user mUser, bool deleteAllRelatedData);
        user AuthenUser(string mUserName, string mPassword);
        Applications Application { get; }
        string ApplicationName { get; set; }
        bool EnablePasswordReset { get; }
        bool EnablePasswordRetrieval { get; }
        int MaxInvalidPasswordAttempts { get; }
        int MinRequiredNonAlphanumericCharacters { get; }
        int MinRequiredPasswordLength { get; }
        int PasswordAttemptWindow { get; }
        MembershipPasswordFormat PasswordFormat { get; }
        string PasswordStrengthRegularExpression { get; }
        bool RequiresQuestionAndAnswer { get; }
        bool RequiresUniqueEmail { get; }

        bool ChangePassword(string username, string oldPassword, string newPassword);
        bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer);

        user CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status);
        user CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, string groupName, out string status);
        IList<user> FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords);
        IList<user> FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);

        IList<user> GetAllUsers(int pageIndex, int pageSize, out int totalRecords);
        int GetNumberOfUsersOnline();
        string GetPassword(string username, string answer);
        user GetUser(object providerUserKey, bool userIsOnline);
        user GetUser(string username, bool userIsOnline);
        string GetUserNameByEmail(string email);

        string ResetPassword(string username, string answer);
        bool UnlockUser(string userName);
        void UpdateUser(user user);
        bool ValidateUser(string username, string password);
    }
}
