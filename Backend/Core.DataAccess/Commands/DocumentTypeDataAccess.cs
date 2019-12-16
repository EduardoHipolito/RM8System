using Core.Domain;
using Framework.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DataAccess.Commands
{
    public class DocumentTypeDataAccess : DataAccessBase<DocumentType, CoreContext>
    {
        public override IQueryable<DocumentType> GetAll(int IdCompany = 0)
        {
            return base.GetAll();
        }
    }
}
