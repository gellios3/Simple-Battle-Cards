using Models.Miltiplayer;
using strange.extensions.command.impl;
using Services.Multiplayer;
using UnityEngine;

namespace Commands.Multiplayer
{
    public class ServerConectedCommand : Command
    {
        /// <summary>
        /// Server connector service
        /// </summary>
        [Inject]
        public ServerConnectorService ServerConnectorService { get; set; }

        /// <summary>
        /// Execute event add log
        /// </summary>
        public override void Execute()
        {
            Debug.Log("ServerConectedCommand");
            ServerConnectorService.Send(MsgStruct.RegisterPlayer, new RegisterPlayerMessage
            {
                Id = 1,
                Name = "test "+Random.Range(0,10)
            });
        }
    }
}