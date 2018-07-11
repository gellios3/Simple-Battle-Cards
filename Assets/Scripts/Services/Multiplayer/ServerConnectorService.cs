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
        [Inject]
        public DisonnectedFromServerSignal DisonnectedFromServerSignal { get; set; }

        /// <summary>
        /// Server connected signal
        /// </summary>
        [Inject]
        public ServerConnectedSignal ServerConnectedSignal { get; set; }

        /// <summary>
        /// Register user handler
        /// </summary>
        [Inject]
        public GetLobbyPlayersHandler GetLobbyPlayersHandler { get; set; }

        /// <summary>
        /// Get enemy turn handler
        /// </summary>
        [Inject]
        public GetEnemyTurnHandler GetEnemyTurnHandler { get; set; }

        /// <summary>
        /// Connect to server
        /// </summary>
        /// <param name="url"></param>
        /// <param name="port"></param>
        public void Connect(string url, int port)
        {
            _client = new NetworkClient();
            _client.Connect(url, port);
            _client.RegisterHandler(MsgType.Connect, msg => { ServerConnectedSignal.Dispatch(); });
            _client.RegisterHandler(MsgType.Disconnect, mas => { DisonnectedFromServerSignal.Dispatch(); });
            RegisterHandlers(new List<IServerMessageHandler> {GetEnemyTurnHandler, GetLobbyPlayersHandler});
        }

        /// <summary>
        /// Disconect fom server
        /// </summary>
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

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="msg"></param>
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

        /// <summary>
        /// Register handlers
        /// </summary>
        /// <param name="handlers"></param>
        public void RegisterHandlers(IEnumerable<IServerMessageHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                _client.RegisterHandler(handler.MessageType, handler.Handle);
            }
        }
    }
}