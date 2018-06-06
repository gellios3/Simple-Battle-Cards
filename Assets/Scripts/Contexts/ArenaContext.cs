using Commands;
using Models;
using Models.Arena;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Signals;
using Signals.Arena;
using UnityEngine;
using View;
using View.Arena;

namespace Contexts
{
    public class ArenaContext : MVCSContext
    {
        public ArenaContext(MonoBehaviour view) : base(view)
        {
            _instance = this;
        }

        public ArenaContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
            _instance = this;
        }

        private static ArenaContext _instance;

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

            var startSignal = injectionBinder.GetInstance<InitArenaSignal>();
            startSignal.Dispatch();

            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Ovverade Bindings map
        /// </summary>
        protected override void mapBindings()
        {
            injectionBinder.Bind<InitArenaSignal>().ToSingleton();
            injectionBinder.Bind<ArenaInitializedSignal>().ToSingleton();

            injectionBinder.Bind<Arena>().ToSingleton();
            injectionBinder.Bind<PlayerCpuBehavior>().ToSingleton();
            injectionBinder.Bind<BattleArena>().ToSingleton();
            injectionBinder.Bind<ArenaGameManager>().ToSingleton();

            commandBinder.Bind<InitArenaSignal>().To<InitArenaCommand>();

            mediationBinder.Bind<GameArenaView>().To<GameArenaMediator>();
        }
    }
}