using FX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teca.Core.Domain;
using Teca.Core.IService;

namespace Teca.Core.ServiceImpl
{
    public class DocumentService : BaseService<Document, int>, IDocumentService
    {
        public DocumentService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath) 
        { }

        public IList<Document> GetList(string name, int pageIndex, int pageSize, out int total)
        {
            var qr = Query.Where(p => p.Active);
            if (!string.IsNullOrWhiteSpace(name))
                qr = qr.Where(p => p.Name.Contains(name) || p.NameENG.Contains(name));
            total = qr.Count();
            qr = qr.OrderByDescending(p => p.CreateDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public IList<Document> GetForAdmin(string name, int pageIndex, int pageSize, out int total)
        {
            var qr = Query;
            if (!string.IsNullOrWhiteSpace(name))
                qr = qr.Where(p => p.Name.Contains(name) || p.NameENG.Contains(name));
            total = qr.Count();
            qr = qr.OrderByDescending(p => p.CreateDate);
            return qr.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }
    }
}
