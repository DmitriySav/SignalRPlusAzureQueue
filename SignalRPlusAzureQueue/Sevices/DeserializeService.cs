using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AzureStorageApi;
using AzureStorageApi.MessageClasses;
using Microsoft.AspNet.SignalR;

namespace SignalRPlusAzureQueue.Sevices
{
    public class MessageHandler
    {
        private IHubContext Context { get; set; }

        public MessageHandler(IHubContext context)
        {
            Context = context;
        }
        public Dictionary<MessageEnum, Action<string>> dict = new Dictionary<MessageEnum, Action<string>>
        {
          //{ MessageEnum.UserMessage, (x) =>
          //      {
          //          var userMessage = JsonConvert.DeserializeObject<UserMessage>(x);
          //          usermessagelist.Add(userMessage);
          //      }
          // },
          //          { MessageEnum.UmpireMessage, (x) =>
          //              {
          //                  var umpireMessage = JsonConvert.DeserializeObject<UmpireMessage>(x);
          //                  umpireMessageList.Add(umpireMessage);
          //              }
          //          },
          //          { MessageEnum.CoachMessage, (x) =>
          //              {
          //                  var coachMessage = JsonConvert.DeserializeObject<CoachMessage>(x);
          //                  coachMessageList.Add(coachMessage);
          //              }
          //          }
            };

    }
}
