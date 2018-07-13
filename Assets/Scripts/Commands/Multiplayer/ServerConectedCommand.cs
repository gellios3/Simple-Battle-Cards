using Models.Miltiplayer;
using strange.extensions.command.impl;
using Services.Multiplayer;

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
        /// Network player service
        /// </summary>
        [Inject]
        public NetworkPlayerService NetworkPlayerService { get; set; } 

        /// <summary>
        /// Execute event add log
        /// </summary>
        public override void Execute()
        {
            // Register player on server
            ServerConnectorService.Send(MsgStruct.SendPlayer, new RegisterPlayerMessage
            {
                Id = NetworkPlayerService.NetworkLobbyPlayer.Id,
                Name = NetworkPlayerService.NetworkLobbyPlayer.Name,
            });
        }
    }
}