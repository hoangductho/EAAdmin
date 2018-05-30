using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teca.Core.Domain;
using Teca.Core.IService;

namespace Teca.Core.ServiceImpl
{
    public class EventsService : FX.Data.BaseService<Events, int>, IEventsService
    {
        public EventsService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }
    }
}
