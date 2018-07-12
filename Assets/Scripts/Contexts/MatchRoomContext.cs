using Client;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Services.Multiplayer;
using Signals.multiplayer;
using UnityEngine;

namespace Contexts
{
    public class MatchRoomContext : MVCSContext
    {
        public MatchRoomContext(MonoBehaviour view) : base(view)
        {
            _instance = this;
        }

        public MatchRoomContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
            _instance = this;
        }

        private static MatchRoomContext _instance;

        public static T Get<T>()
        {
            return _instance.injectionBinder.GetInstance<T>();
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Unbind the default EventCommandBinder and rebind the SignalCommandBinder
        /// </summary>
        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        /// <summary>
        /// Override Start so that we can fire the StartSignal 
        /// </summary>
        /// <returns></returns>
        public override IContext Start()
        {
            base.Start();

//            var startSignal = injectionBinder.GetInstance<CreateNewGameSignal>();
//            var startSignal = injectionBinder.GetInstance<LoadGameSignal>();
//            startSignal.Dispatch();

            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Ovverade Bindings map
        /// </summary>
        protected override void mapBindings()
        {
            // init Signals
            injectionBinder.Bind<ConnectToServerSignal>().ToSingleton(); 
            injectionBinder.Bind<ServerConnectedSignal>().ToSingleton();
            injectionBinder.Bind<DisonnectedFromServerSignal>().ToSingleton();
            
            
            // init models

            //Bind Services
            injectionBinder.Bind<ServerConnectorService>().ToSingleton();

            // Init comands

            // Init mediators
        }
    }
}