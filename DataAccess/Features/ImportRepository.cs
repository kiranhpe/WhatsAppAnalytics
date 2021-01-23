using DataAccess.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Features
{
    public class ImportRepository : IImportRepository
    {
        private readonly ILogger m_logger;
        public ImportRepository(ILogger<ImportRepository> logger)
        {
            m_logger = logger;
        }

        public List<Import> GetImportData(Import filter)
        {
            var importList = new List<Import>();

            m_logger.LogInformation("Trying to get Information");
            try
            {
                using var context = new whatsappContext();
                importList = context.Imports.ToList();

                return importList;
            }
            catch (Exception exp)
            {
                m_logger.LogError("Error occured while getting import data, Details" + exp.Message);
                return importList;
            }
        }
        public List<Import> GetRawData(RawDatum filter)
        {
            throw new NotImplementedException();
        }

        public List<Import> InsertData(List<Import> data)
        {
            throw new NotImplementedException();
        }

        public Import InsertImportData(Import data)
        {
            try
            {
                if (data != null)
                {
                    using var context = new whatsappContext();
                    context.Imports.Add(data);

                    context.SaveChanges();
                }

                return data;
            }
            catch(Exception exp)
            {
                m_logger.LogError("Error occured while inserting import data, Details" + exp.Message);
                return null;
            }
        }

        public List<RawDatum> InsertRawtData(List<RawDatum> data)
        {
            try
            {
                if (data != null)
                {
                    using var context = new whatsappContext();
                    foreach(var item in data)
                    {
                        context.RawData.Add(item);
                    }
                    context.SaveChanges();
                }

                return data;
            }
            catch (Exception exp)
            {
                m_logger.LogError("Error occured while inserting Raw data, Details" + exp.Message);
                return null;
            }
        }

        public Import UpdateImportData(Import data)
        {
            try
            {
                if (data != null)
                {
                    using var context = new whatsappContext();

                    var dataToUpdate = context.Imports.FirstOrDefault(x => x.Id == data.Id);

                    if(dataToUpdate != null)
                    {
                        dataToUpdate.ImportStatusId = data.ImportStatusId;
                    }
                    context.SaveChanges();
                }

                return data;
            }
            catch (Exception exp)
            {
                m_logger.LogError("Error occured while inserting updating data, Details" + exp.Message);
                return null;
            }
        }
    }
}
