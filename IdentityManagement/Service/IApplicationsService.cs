using System;
using System.Collections.Generic;
using System.Text;
using IdentityManagement.Domain;
namespace IdentityManagement.Service
{
    public interface IApplicationsService : FX.Data.IBaseService<Applications, int>
    {
        Applications GetByName(string AppName);
        IList<Applications> SearchApplication(string AppName);

        Applications CreateNewApplication(string AppName, string AppDescription, string AppUrl, string username, string password);
        Applications CreateNewWithShareUser(string AppName, string AppDescription, string AppUrl, string username);
        Applications GetByURL(string AppURL);
    }
}
