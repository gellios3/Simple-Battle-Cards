using Commands;
using Commands.Multiplayer;
using Mediators;
using Models.Arena;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Services;
using Services.Multiplayer;
using Signals;
using Signals.Arena;
using Signals.multiplayer;
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

        /// <summary>
        /// Override Start so that we can fire the StartSignal 
        /// </summary>
        /// <returns></returns>
        public override IContext Start()
        {
            base.Start();

//            var startSignal = injectionBinder.GetInstance<CreateNewGameSignal>();
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
            injectionBinder.Bind<StartOnlineGameSignal>().ToSingleton().CrossContext();
            
            // init models          
            
            // Init sevises
            injectionBinder.Bind<NetworkPlayerService>().ToSingleton().CrossContext();
         
            // Init comands
            commandBinder.Bind<StartOnlineGameSignal>().To<StartOnlineGameCommand>().Once();

            // Init mediators
            mediationBinder.Bind<MainMenuView>().To<MainMenuMediator>();
        }
    }
}