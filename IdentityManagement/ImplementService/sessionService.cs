using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FX.Data;
using IdentityManagement.Domain;
using IdentityManagement.Service;

namespace IdentityManagement.ImplementService
{
    public class sessionService : BaseService<session, int>, IsessionService
    {
        public sessionService(string sessionFactoryConfigPath)
            : base(sessionFactoryConfigPath)
        { }
    }

}