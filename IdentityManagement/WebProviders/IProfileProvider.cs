using System;
using System.Collections.Generic;
using System.Web.Profile;
using IdentityManagement.Domain;
namespace IdentityManagement.WebProviders
{
   public interface IProfileProvider
    {
        /// <summary>
        /// deletes profile properties and information for the supplied list of profiles.
        /// </summary>
        /// <param name="Profiles">A System.Web.Profile.ProfileInfoCollection of information about profiles that are to be deleted.</param>
        /// <returns>The number of profiles deleted from the data source.</returns>
        int DeleteProfiles(List<UserProfile> Profiles);
        /// <summary>
        /// deletes profile properties and information for profiles that match the supplied user names.
        /// </summary>
        /// <param name="usernames">string of User Name</param>
        /// <returns> The number of profiles deleted from the data source.</returns>
        int DeleteProfiles(string usernames);
        /// <summary>
        /// deletes profile properties and information  for profiles that match the supplied list of user names.
        /// </summary>
        /// <param name="usernames">A string array of user names for profiles to be deleted.</param>
        /// <returns>The number of profiles deleted from the data source.</returns>
        int DeleteProfiles(string[] usernames);
        /// <summary>
        /// retrieves profile information for profiles  in which the user name matches the specified user names.
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        IDictionary<string, UserProfile> FindProfilesByUserName(string UserName);
        object GetProperty(string UserName, string PropertyName);
        int CreateProfileForUser(string UserName, string[] PropertyNames, object[] PropertyValues);
        int UpdateProfileForUser(string UserName, string[] PropertyNames, object[] PropertyValues);
        int AddPorfileforUser(string UserName, string PropertyNames, object PropertyValues);
        int UpdateProfileForUser(user mUser, string[] PropertyNames, object[] PropertyValues);
        
    }
}
