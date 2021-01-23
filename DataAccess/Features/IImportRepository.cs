using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Features
{
    public interface IImportRepository
    {
        List<Import> GetImportData(Import filter);

        Import InsertImportData(Import data);

        Import UpdateImportData(Import data);


        List<Import> GetRawData(RawDatum filter);

        List<RawDatum> InsertRawtData(List<RawDatum> data);
    }
}
