using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.WebPages;
using AzureStorageApi;
using AzureStorageApi.Api;
using AzureStorageApi.Interfaces;
using SignalRPlusAzureQueue.Interfaces;

namespace SignalRPlusAzureQueue.Readers
{
    /// <summary>
    /// Queue reader work with azure storage service
    /// </summary>
    public class MessageEventHandler
    {
        public IAzureStorage AzureQueueStorage { get; set; }


        public MessageEventHandler()
        {
            var db = new AppContext();
            var connectionString = db.ConnectionStrings.SingleOrDefault(item => item.Environment == (byte)Environment.Development);
            AzureQueueStorage = new AzureStorageQueueApi(connectionString.Value, connectionString.Name);
        }
        public MessageEventHandler(IAzureStorage azureStorage)
        {
            AzureQueueStorage = azureStorage;
        }
        public Dictionary<MessageEnum, Action<string>> deserializeCondition = new Dictionary<MessageEnum, Action<string>>
        {
            //{MessageEnum.UserMessage, (x) =>
            //    {
            //        var userMessage = JsonConvert.DeserializeObject<UserMessage>(x);
            //        usermessagelist.Add(userMessage);
            //    }

            //},
            //{ MessageEnum.UmpireMessage, (x) =>
            //    {
            //        var umpireMessage = JsonConvert.DeserializeObject<UmpireMessage>(x);
            //        umpireMessageList.Add(umpireMessage);
            //    }
            //},
            //{ MessageEnum.CoachMessage, (x) =>
            //    {
            //        var coachMessage = JsonConvert.DeserializeObject<CoachMessage>(x);
            //        coachMessageList.Add(coachMessage);
            //    }
            //}
        };
        /// <summary>
        /// Event OnGetMessage
        /// </summary>
        //public event ReaderEventHandler OnGetMessage;

        //event ReaderEventHandler IQueueReader.OnGetMessage
        //{
        //    add { if (OnGetMessage == null) { OnGetMessage += value; } }
        //    remove { OnGetMessage -= value; }
        //}

        /// <summary>
        /// Get number of items in queue
        /// </summary>
        /// <returns>int number</returns>


        /// <summary>
        /// Get message from queue
        /// Invoke OnGetMessage event
        /// </summary>
        //public void GetMessage()
        //{
        //    var message = _azureQueueStorageService.GetMessage();
        //    if (!message.IsEmpty())
        //    {
        //        OnGetMessage?.Invoke(message);
        //    }

        //}

    }
}