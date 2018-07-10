using strange.extensions.command.impl;
using UnityEngine;

namespace Commands.Multiplayer
{
    public class ServerConectedCommand : Command
    {
        /// <summary>
        /// Execute event add log
        /// </summary>
        public override void Execute()
        {
            Debug.Log("ServerConectedCommand");
        }
    }
}