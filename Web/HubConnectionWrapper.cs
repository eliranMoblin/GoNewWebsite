using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Entities;
using Microsoft.AspNet.SignalR.Client;

namespace Web
{
    public class HubConnectionWrapper : IDisposable
    {
        private readonly string _url;
        readonly string _hubName;

        private HubConnection _hubConnection;
        private IHubProxy _hubProxy;

        internal HubConnectionWrapper(string url, string hubName)
        {
            _url = url;
            _hubName = hubName;
        }

        public async Task StartAsync()
        {
            try
            {
                _hubConnection = new HubConnection(_url);
                _hubProxy = _hubConnection.CreateHubProxy(_hubName);
                await _hubConnection.Start();

            }
            catch (HttpRequestException e)
            {
            }
        }

        public void Start()
        {
            try
            {
                _hubConnection = new HubConnection(_url);
                _hubProxy = _hubConnection.CreateHubProxy(_hubName);
                var task = _hubConnection.Start();
                Task.WaitAll(task);

            }
            catch (HttpRequestException e)
            {
            }
        }


        internal async Task Invoke<T>(string method, Action<T> onProgress)
        {

            await _hubProxy.Invoke(method, onProgress);
        }

        internal async Task Invoke(string method, params object[] args)
        {
            await _hubProxy.Invoke(method, args);
        }


        internal async Task TryInvokeAsync(string method, params object[] args)
        {
            var connected = _hubConnection.EnsureReconnecting();
            if (connected)
            {
                await _hubProxy.Invoke(method, args);
            }
        }

        internal void TryInvoke(string method, params object[] args)
        {
            var connected = _hubConnection.EnsureReconnecting();
            if (connected)
            {
                _hubProxy.Invoke(method, args).Wait();
            }
        }

        public void Dispose()
        {
            _hubConnection.Stop();
            _hubConnection.Dispose();
        }
    }

    public class NotificationHubClient : HubConnectionWrapper
    {
        public NotificationHubClient(string url)
            : base(url, "NotificationHub")
        {
        }

        //public async Task SendNotificationAsync(Notification notification)
        //{
        //    await TryInvokeAsync("SendNotification", notification);
        //}

        //public void SendNotification(Notification notification)
        //{
        //    TryInvoke("SendNotification", notification);
        //}
    }



}