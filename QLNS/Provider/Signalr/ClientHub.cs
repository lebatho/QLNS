using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;

namespace QLNS.Provider.Signalr
{
    public class ClientHub : Hub
    {
        private readonly ILogger<ClientHub> _logger;
        public ClientHub(ILogger<ClientHub> logger)
        {
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            try
            {
                var feature = Context.Features.Get<IHttpConnectionFeature>();
                if (feature != null)
                {
                    SendFirstMessage(Context.ConnectionId, Context.ConnectionId);
                }
                _logger.LogError("CONNECTED");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return base.OnConnectedAsync();
        }

        public void SendMessage(string user, MessageModel message)
        {
            Clients.Client(user).SendAsync("ReceiveMessage", message).ConfigureAwait(false);
        }

        public void SendFirstMessage(string user, string message)
        {
            Clients.Client(user).SendAsync("FirstMessage", message).ConfigureAwait(false);
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogError("DISCONNECTED");
            return base.OnDisconnectedAsync(exception);
        }
    }

    public class MessageModel
    {
        public string LocalIpAddress { get; set; }
        public string RemoteIpAddress { get; set; }
        public string ConnectionId { get; set; }
        public string RemotePort { get; set; }
        public string LocalPort { get; set; }
    }
}
