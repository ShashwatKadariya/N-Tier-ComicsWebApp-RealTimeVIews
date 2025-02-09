using Microsoft.AspNetCore.SignalR;

namespace ComicsWeb.Hubs;

public class NotificationHub: Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }
}