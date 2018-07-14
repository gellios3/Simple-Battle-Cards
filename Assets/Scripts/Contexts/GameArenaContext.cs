using Commands;
using Commands.GameArena;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Signals.GameArena;
using UnityEngine;

namespace Contexts
{
    public class GameArenaContext : MVCSContext
    {
        public GameArenaContext(MonoBehaviour view) : base(view)
        {
            _instance = this;
        }

        public GameArenaContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
            _instance = this;
        }

        private static GameArenaContext _instance;

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

            var startSignal = injectionBinder.GetInstance<InitNewGameSignal>();
            startSignal.Dispatch();

            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Ovverade Bindings map
        /// </summary>
        protected override void mapBindings()
        {
            // Init comands
            commandBinder.Bind<InitNewGameSignal>().To<InitNewGameCommand>();
            commandBinder.Bind<InitBattleTurnSignal>().To<InitBattleTurnCommand>();
        }
    }
}