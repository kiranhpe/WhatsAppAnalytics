using DataAccess.Features;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BackendLogic.Features.ImportMessage
{
    public class ImportMessageService : IImportService
    {
        private readonly IImportRepository m_importRepository;
        public ImportMessageService(IImportRepository importRepository)
        {
            m_importRepository = importRepository;
        }
        public List<Import> GetImports()
        {
            return this.m_importRepository.GetImportData(new Import() { });
        }

        public Import InsertImports()
        {
            var import = InsertImportData();

            try
            {
                var chats = new List<RawDatum>();

                var chatsFromFile = GetChatsFromFile();

                var defaultDate = DateTime.ParseExact("01/01/01", "dd/MM/yy", CultureInfo.InvariantCulture);

                for (var i = 0; i < chatsFromFile.Length; i++)
                {
                    var chat = new RawDatum()
                    {
                        ImportId = import.Id,
                        Chat = GetChatMesseage(chatsFromFile[i]) ?? GetChatMesseage(chatsFromFile[i - 1] + chatsFromFile[i]),
                        ChatDateTime = GetChatDateTime(chatsFromFile[i]) == defaultDate ? GetChatDateTime(chatsFromFile[i - 1] + chatsFromFile[i]) : GetChatDateTime(chatsFromFile[i])
                    };

                    chats.Add(chat);
                }
                m_importRepository.InsertRawtData(chats);

                UpdateImportData(import.Id, 1);

                return import;
            }
            catch(Exception exp)
            {
                UpdateImportData(import.Id, 3);

                return null;
            }
            
        }

        private string[] GetChatsFromFile()
        {
           // string[] lines = System.IO.File.ReadAllLines(@"D:\Work\WhatsAppAnalytics\WhatsAppChatwithMounica.txt");
            string[] lines = System.IO.File.ReadAllLines(@"D:\Work\WhatsAppAnalytics\WhatsAppChatwithHarshi.txt");


            return lines;
        }

        private string GetChatMesseage(string message)
        {

            if(message.IndexOf(" - ") > -1)
            {
                return message.Substring(message.IndexOf('-')).Replace("-","").Trim();
            }
            return null;
        }

        private DateTime GetChatDateTime(string message)
        {
            if (message.IndexOf(" - ") > -1)
            {
                var dateString = message.Split("-")[0].Replace(",", "").ToUpper().Trim();
                return DateTime.ParseExact(dateString, "dd/MM/yy h:mm tt", CultureInfo.InvariantCulture);
            }
            return DateTime.ParseExact("01/01/01", "dd/MM/yy", CultureInfo.InvariantCulture);
            // "2/22/2015 9:54:02 AM"
            // "08/01/21, 11:33 am "
        }

        private Import InsertImportData()
        {
            var res = m_importRepository.InsertImportData(new Import() { FileName = "test", ImportedBy = "Admin", ImpotedDateTime = DateTime.Now, ImportStatusId = 2 });
            return res;
        }

        private Import UpdateImportData(int id, int statusId)
        {
            var res = m_importRepository.UpdateImportData(new Import() { Id = id, FileName = "test", ImportedBy = "Admin", ImpotedDateTime = DateTime.Now, ImportStatusId = statusId });
            return res;
        }
    }
}
