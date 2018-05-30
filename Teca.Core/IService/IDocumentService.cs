    using FX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teca.Core.Domain;

namespace Teca.Core.IService
{
    public interface IDocumentService : IBaseService<Document, int>
    {
        IList<Document> GetList(string name, int pageIndex, int pageSize, out int total);

        IList<Document> GetForAdmin(string name, int pageIndex, int pageSize, out int total);
    }
}
