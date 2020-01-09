using Interfaces;
using Models.Multiplayer.Messages;
using Signals.multiplayer;
using UnityEngine.Networking;

namespace Handlers
{
//    public class GetEnemyTurnHandler : IServerMessageHandler
//    {
//        /// <summary>
//        /// Message type
//        /// </summary>
//        public short MessageType => MsgStruct.EnemyTurnResponse;
//
//        /// <summary>
//        /// Room list data
//        /// </summary>
//        [Inject]
//        public GetEnemyTurnSignal GetEnemyTurnSignal { get; set; }
//
//        public void Handle(NetworkMessage msg)
//        {
//            var regularMsg = msg.ReadMessage<EnemyTurnMessage>();
//            if (regularMsg != null)
//            {
////                UpdateRegularGameDataSignal.Dispatch(new BaseRegularGame
////                {
////                    CurrentPlayers = regularMsg.CurrentPlayers,
////                    Id = regularMsg.Id,
////                    MaxPlayers = regularMsg.MaxPlayers,
////                    Name = regularMsg.Name,
////                    Price = regularMsg.Price
////                });
//            }
//        }
//    }
}