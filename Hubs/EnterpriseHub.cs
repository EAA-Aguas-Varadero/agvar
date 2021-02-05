using BlazorApp2.Shared;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Server.Hubs
{
    public class EnterpriseHub: Hub
    {
        public static Dictionary<string, Enterprise> Enterprises = new Dictionary<string, Enterprise>();
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            var guid = Guid.NewGuid() + "-" + Guid.NewGuid();
            await Clients.Caller.SendAsync("AuthChallenge", guid);
        }

        public async Task AuthChallenge(string name,string response)
        {
            if (System.IO.File.Exists($"AppData\\Enterprise\\{name}.json"))
            {
                var data = JsonConvert.DeserializeObject<Enterprise>(await System.IO.File.ReadAllTextAsync($"AppData\\Enterprise\\{name}.json"));
                if (response == data.PrivateKey)
                {
                    Enterprises.Add(Context.ConnectionId, data);
                }
            }
        }

        public async Task GetRecibos(object data)
        {
            
        }
    }


}
