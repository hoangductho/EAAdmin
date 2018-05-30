using FX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teca.Core.Domain;
using Teca.Core.IService;

namespace Teca.Core.ServiceImpl
{
    public class IntroductionService : BaseService<Introduction, int>, IIntroductionService
    {
        public IntroductionService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) { }
    }
}
