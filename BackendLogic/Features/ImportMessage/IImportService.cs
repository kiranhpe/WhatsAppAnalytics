using System;
using System.Collections.Generic;
using System.Text;

using DataAccess.Models;

namespace BackendLogic.Features.ImportMessage
{
    public interface IImportService
    {
        List<Import> GetImports();

        void  InsertImports();

    }
}
