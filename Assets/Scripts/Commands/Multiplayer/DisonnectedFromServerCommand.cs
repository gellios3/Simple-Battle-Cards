using strange.extensions.command.impl;
using UnityEngine;

namespace Commands.Multiplayer
{
    public class DisonnectedFromServerCommand : Command
    {
        /// <summary>
        /// Execute conect to server
        /// </summary>
        public override void Execute()
        {
            Debug.Log("DisonnectedFromServerCommand");
        }
    }
}