using Commands.Multiplayer;
using Handlers;
using Mediators;
using Mediators.Multiplayer;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Services.Multiplayer;
using Signals.multiplayer;
using Signals.MainMenu;
using UnityEngine;
using View.Multiplayer;

namespace Contexts
{
    public class NetworkLobbyContext : MVCSContext
    {
        public NetworkLobbyContext(MonoBehaviour view) : base(view)
        {
            _instance = this;
        }

        public NetworkLobbyContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
            _instance = this;
        }

        private static NetworkLobbyContext _instance;

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

        /// <inheritdoc />
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
        /// Override Bindings map
        /// </summary>
        protected override void mapBindings()
        {
            // init Signals
            injectionBinder.Bind<ServerConnectedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<DisconnectedFromServerSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PingPlayerIdToServerSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<GetEnemyTurnSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<ShowLobbyPlayersSignal>().ToSingleton();

            // init models

            //Bind Services
            injectionBinder.Bind<ServerConnectorService>().ToSingleton().CrossContext();

            // Bind Handlers
            injectionBinder.Bind<GetEnemyTurnHandler>().ToSingleton().CrossContext();
            injectionBinder.Bind<GetLobbyPlayerHandler>().ToSingleton().CrossContext();
            injectionBinder.Bind<RemoveLobbyPlayerHandler>().ToSingleton().CrossContext();

            // Init commands
            commandBinder.Bind<ConnectToServerSignal>().To<ConectToServerCommand>();
            commandBinder.Bind<PingPlayerIdToServerSignal>().To<PingPlayerIdToServerCommand>();
            commandBinder.Bind<ServerConnectedSignal>().To<ServerConectedCommand>().Once();

            // Init mediators
            mediationBinder.Bind<NetwokLobbyView>().To<NetworkLobbyMediator>();
            mediationBinder.Bind<StatusItemView>().To<StatusItemMediator>();
        }
    }
}