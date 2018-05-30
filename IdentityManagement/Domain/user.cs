using System;
using System.Collections.Generic;
using System.Web.Security;
namespace IdentityManagement.Domain
{
    public class user
    {
        private int _userid;
        private string _username;
        private string _password;
        private int _PasswordFormat;
        private string _PasswordSalt;
        private string _email;
        private string _PasswordQuestion;
        private string _PasswordAnswer;
        private Boolean _IsApproved;
        private Boolean _IsLockedOut;
        private DateTime _CreateDate = DateTime.Now;
        private DateTime _LastLoginDate = DateTime.Now ;
        private DateTime _LastPasswordChangedDate = DateTime.Now ;
        private DateTime _LastLockoutDate = DateTime.Now ;
        private int _FailedPasswordAttemptCount;
        private int _FailedPasswordAnswerAttemptCount;
        private DateTime _FailedPasswordAttemptWindowStart = DateTime.Now;
        private DateTime _FailedPasswordAnswerAttemptStart = DateTime.Now;
        private string _GroupName;        

        public virtual int userid
        {
            get { return _userid; }
            set { _userid = value; }
        }

        public virtual string username
        {
            get { return _username; }
            set { _username = value; }
        }

        public virtual string password
        {
            get { return _password; }
            set { _password = value; }
        }
        public virtual int PasswordFormat
        {
            get { return _PasswordFormat; }
            set { _PasswordFormat = value; }
        }

        public virtual string PasswordSalt
        {
            get { return _PasswordSalt; }
            set { _PasswordSalt = value; }
        }

        public virtual string email
        {
            get { return _email; }
            set { _email = value; }
        }

        public virtual string PasswordQuestion
        {
            get { return _PasswordQuestion; }
            set { _PasswordQuestion = value; }
        }

        public virtual string PasswordAnswer
        {
            get { return _PasswordAnswer; }
            set { _PasswordAnswer = value; }
        }

        public virtual Boolean IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }

        public virtual Boolean IsLockedOut
        {
            get { return _IsLockedOut; }
            set { _IsLockedOut = value; }
        }

        public virtual DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        public virtual DateTime LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }

        public virtual DateTime LastPasswordChangedDate
        {
            get { return _LastPasswordChangedDate; }
            set { _LastPasswordChangedDate = value; }
        }

        public virtual DateTime LastLockoutDate
        {
            get { return _LastLockoutDate; }
            set { _LastLockoutDate = value; }
        }

        public virtual int FailedPasswordAttemptCount
        {
            get { return _FailedPasswordAttemptCount; }
            set { _FailedPasswordAttemptCount = value; }
        }

        public virtual int FailedPasswordAnswerAttemptCount
        {
            get { return _FailedPasswordAnswerAttemptCount; }
            set { _FailedPasswordAnswerAttemptCount = value; }
        }

        public virtual DateTime FailedPasswordAnswerAttemptStart
        {
            get { return _FailedPasswordAnswerAttemptStart; }
            set { _FailedPasswordAnswerAttemptStart = value; }
        }

        public virtual DateTime FailedPasswordAttemptWindowStart
        {
            get { return _FailedPasswordAttemptWindowStart; }
            set { _FailedPasswordAttemptWindowStart = value; }
        }

        public virtual string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        IList<Applications> _ApplicationList;

        public virtual IList<Applications> ApplicationList
        {
            get { return _ApplicationList; }
            set { _ApplicationList = value; }
        }

        IList<role> _Roles;
        public virtual IList<role> Roles
        {
            get { return _Roles; }
            set { _Roles = value; }
        }

        IList<session> _sessions;
        public virtual IList<session> Sessions
        {
            get { return _sessions; }
            set { _sessions = value; }
        }       
    }
}
