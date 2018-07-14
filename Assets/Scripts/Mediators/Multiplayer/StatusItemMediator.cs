using UnityEngine;
using View.Multiplayer;

namespace Mediators.Multiplayer
{
    public class StatusItemMediator : TargetMediator<StatusItemView>
    {
        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
            Debug.Log("StatusItemMediator");
        }
    }
}