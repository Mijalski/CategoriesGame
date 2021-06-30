using CategoriesGameContracts.Contracts;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CategoriesGameClient.Clients
{
    public class GameHubClient
    {
        public const string HubUrl = "/gameHub";
        private HubConnection? _hubConnection;

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            if(_hubConnection is null || _hubConnection.State == HubConnectionState.Disconnected)
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(HubUrl)
                    .WithAutomaticReconnect(Enumerable.Range(0, 10).Select(x => TimeSpan.FromSeconds(x)).ToArray())
                    .Build();

                _hubConnection.On<string>("GameStarted", OnGameStartedAsync); 
                _hubConnection.On<string>("PlayerJoined", OnPlayerJoinedAsync);
                _hubConnection.On<SettingsDto>("GetSettings", OnSettingsReceivedAsync);

                await _hubConnection.StartAsync(cancellationToken);
            }
        }

        private void OnSettingsReceivedAsync(SettingsDto settings)
        {
            throw new NotImplementedException();
        }

        private async Task OnPlayerJoinedAsync(string userName)
        {
            throw new NotImplementedException();
        }

        private async Task OnGameStartedAsync(string gameCode)
        {
            throw new NotImplementedException();
        }
    }
}
