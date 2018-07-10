using System.Collections.Generic;
using Handlers;
using Interfaces;
using Signals.multiplayer;
using UnityEngine;
using UnityEngine.Networking;

namespace Services.Multiplayer
{
    public class ServerConnectorService : IServerConnector
    {
        /// <summary>
        /// Network client
        /// </summary>
        private NetworkClient _client;

        /// <summary>
        /// Disonnected from server signal
        /// </summary>
        [Inject] public DisonnectedFromServerSignal DisonnectedFromServerSignal { get; set; }

        /// <summary>
        /// Server connected signal
        /// </summary>
        [Inject] public ServerConnectedSignal ServerConnectedSignal { get; set; }
        
        /// <summary>
        /// Get enemy turn handler
        /// </summary>
        [Inject] public GetEnemyTurnHandler GetEnemyTurnHandler { get; set; }

        public void Connect(string url, int port)
        {
            _client = new NetworkClient();
            _client.Connect(url, port);
            _client.RegisterHandler(MsgType.Connect, msg => { ServerConnectedSignal.Dispatch(); });
            RegisterHandlers(new List<IServerMessageHandler> {GetEnemyTurnHandler});
        }

        public void DisconectFromServer()
        {
            if (_client != null)
            {
                _client.Disconnect();
                DisonnectedFromServerSignal.Dispatch();
            }
            else
            {
                Debug.LogError("You should connect to server first");
            }
        }

        public void Send(short msgId, MessageBase msg)
        {
            if (_client != null && _client.isConnected)
            {
                _client.Send(msgId, msg);
            }
            else
            {
                Debug.LogError("You should connect to server first");
            }
        }

        public void RegisterHandlers(IEnumerable<IServerMessageHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                _client.RegisterHandler(handler.MessageType, handler.Handle);
            }
        }
    }
}