using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.SignalR.Hubs
{
    public class ProductHub : Hub
    {
        // Hub'a dair bağlantı olayları (conneciton or disconnection). Örneğin loglama işlemleri yapabiliriz.
        // Hub'a bağlanan html bazlı bir client'sa ilk sayfa yüklenmesinde bağlantı kurulacaktır (OnConnectedAsync),
        // Sayfa yenileme işleminde ise önce bağlantıyı koparacak (OnDisconnectedAsync) ardından yeniden bağlantı kuracaktır. (OnConnectedAsync)

        // Hub'a yeni bir client bağlandığında çalışacak method.
        public override Task OnConnectedAsync()
        {
            // ConnectionId, Hub'a baglantı gerçekleştiren her bir Client için sistem tarafından verilen unique bir değerdir.
            string clientId = Context.ConnectionId;
            
            return base.OnConnectedAsync();
        }

        // Hub'a bağlı bir client'ın bağlantısı koptuğunda çalışacak method.
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
