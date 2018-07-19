using Commands.Multiplayer;
using Mediators;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Services;
using Services.GameArena;
using Services.Multiplayer;
using Signals.MainMenu;
using UnityEngine;
using View;

namespace Contexts
{
    public class MainMenuContext : MVCSContext
    {
        public MainMenuContext(MonoBehaviour view) : base(view)
        {
            _instance = this;
        }

        public MainMenuContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
            _instance = this;
        }

        private static MainMenuContext _instance;

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
        /// Ovverade Bindings map
        /// </summary>
        protected override void mapBindings()
        {
            // init Signals
            injectionBinder.Bind<StartOnlineGameSignal>().ToSingleton().CrossContext();

            // init models          

            // Init sevises
            injectionBinder.Bind<NetworkPlayerService>().ToSingleton().CrossContext();
            injectionBinder.Bind<GameStateService>().ToSingleton().CrossContext();

            // Init comands
            commandBinder.Bind<StartOnlineGameSignal>().To<StartOnlineGameCommand>().Once();

            // Init mediators
            mediationBinder.Bind<MainMenuView>().To<MainMenuMediator>();
        }
    }
}