using strange.extensions.command.impl;
using UnityEngine;

namespace Commands.GameArena
{
    public class EndTurnCommand : Command
    {
        public override void Execute()
        {
            Debug.Log("EndTurnCommand");
        }
    }
}