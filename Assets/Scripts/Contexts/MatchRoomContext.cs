using Commands.Multiplayer;
using Handlers;
using Mediators;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Services.Multiplayer;
using Signals.multiplayer;
using UnityEngine;
using View.Multiplayer;

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
            var startSignal = injectionBinder.GetInstance<ConnectToServerSignal>();
            startSignal.Dispatch();

            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Ovverade Bindings map
        /// </summary>
        protected override void mapBindings()
        {
            // init Signals
            injectionBinder.Bind<ServerConnectedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<DisonnectedFromServerSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<GetEnemyTurnSignal>().ToSingleton().CrossContext();
            
            // init models

            //Bind Services
            injectionBinder.Bind<ServerConnectorService>().ToSingleton().CrossContext();
            injectionBinder.Bind<GetEnemyTurnHandler>().ToSingleton().CrossContext();
            
            // Init comands
            commandBinder.Bind<ConnectToServerSignal>().To<ConectToServerCommand>();
            commandBinder.Bind<ServerConnectedSignal>().To<ServerConectedCommand>().Once();

            // Init mediators
            mediationBinder.Bind<MathRoomView>().To<MathRoomMediator>();
        }
    }
}