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
        private readonly IGenericRepository<Import> m_genericRepository;
        public ImportMessageService(IGenericRepository<Import> genericRepository)
        {
            m_genericRepository = genericRepository;
        }
        public List<Import> GetImports()
        {
            return this.m_genericRepository.GetData(new Import() { });
        }

        public void InsertImports()
        {
            var chats = new List<RawData>();

            foreach(var item in GetChatsFromFile())
            {
                var chat = new RawData()
                {
                    ImportId = 3,
                    Chat = GetChatMesseage(item),
                    ChatDateTime = GetChatDateTime(item)
                };

                chats.Add(chat);
            }
        }

        private string[] GetChatsFromFile()
        {
            string[] lines = System.IO.File.ReadAllLines(@"D:\Work\WhatsAppAnalytics\WhatsAppChatwithMounica.txt");

            return lines;
        }

        private string GetChatMesseage(string message)
        {
            return message.Substring(message.IndexOf('-')).Trim();
        }

        private DateTime GetChatDateTime(string message)
        {
            var dateString = message.Split("-")[0].Replace(",","").ToUpper().Trim();
            return DateTime.ParseExact(dateString, "dd/MM/yy h:mm tt", CultureInfo.InvariantCulture);
            // "2/22/2015 9:54:02 AM"
            // "08/01/21, 11:33 am "
        }
    }
}
