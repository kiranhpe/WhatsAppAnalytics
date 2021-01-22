using DataAccess.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Features
{
    public class ImportRepository : IGenericRepository<Import>
    {
        private readonly ILogger m_logger;
        public ImportRepository(ILogger<ImportRepository> logger)
        {
            m_logger = logger;
        }
        public List<Import> GetData(Import filter)
        {

            var importList = new List<Import>();

            m_logger.LogInformation("Trying to get Information");
            try 
            {
                using (var context = new WhatsappContext())
                {
                    importList = context.Imports.ToList();

                    return importList;
                }
            }
            catch(Exception exp)
            {
                m_logger.LogError("Error occured while getting import data, Details" + exp.Message);
                return importList;
            }
        }

        public List<Import> InsertData(List<Import> data)
        {
            throw new NotImplementedException();
        }
    }
}
