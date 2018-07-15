using Commands.GameArena;
using Mediators.GameArena;
using Models.Arena;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Services;
using Signals;
using Signals.GameArena;
using UnityEngine;
using View.GameArena;

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
            // init Signals
            injectionBinder.Bind<AddHistoryLogToViewSignal>().ToSingleton();
            injectionBinder.Bind<InitAllDecksSignal>().ToSingleton();  
            injectionBinder.Bind<ShowManaSignal>().ToSingleton();

            // Init comands
            commandBinder.Bind<InitNewGameSignal>().To<InitNewGameCommand>();
            commandBinder.Bind<InitBattleTurnSignal>().To<InitBattleTurnCommand>();
            commandBinder.Bind<AddHistoryLogSignal>().To<AddHistoryLogCommand>();
            commandBinder.Bind<InitManaSignal>().To<InitManaCommand>();

            // init models
            injectionBinder.Bind<Arena>().ToSingleton();
            injectionBinder.Bind<BattleArena>().ToSingleton();

            // Init sevises
            injectionBinder.Bind<StateService>().ToSingleton();
            injectionBinder.Bind<BattleTurnService>().ToSingleton();

            // Init mediators
            mediationBinder.Bind<HistoryLogView>().To<HistoryLogMediator>();
            mediationBinder.Bind<SideDecksView>().To<SideDecksMediator>();
            mediationBinder.Bind<ManaView>().To<ManaMediator>();
        }
    }
}