using System;
using System.Collections.Generic;
using System.Text;
using IdentityManagement.Domain;
namespace IdentityManagement.Service
{
    public interface IAppTokenService : FX.Data.IBaseService<AppToken, int>
    {
    }
}
