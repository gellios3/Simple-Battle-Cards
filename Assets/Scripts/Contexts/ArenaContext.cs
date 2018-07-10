using Commands;
using Models;
using Models.Arena;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Services;
using Signals;
using Signals.Arena;
using UnityEngine;
using View;

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

            var startSignal = injectionBinder.GetInstance<CreateNewGameSignal>();
//            var startSignal = injectionBinder.GetInstance<LoadGameSignal>();
            startSignal.Dispatch();

            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Ovverade Bindings map
        /// </summary>
        protected override void mapBindings()
        {
            // init models
            injectionBinder.Bind<JsonWorkService>().ToSingleton();
            injectionBinder.Bind<BattleTurnService>().ToSingleton();
            injectionBinder.Bind<Arena>().ToSingleton();
            injectionBinder.Bind<BattleArena>().ToSingleton();
            injectionBinder.Bind<LoadSaveGameService>().ToSingleton();

            // Init sevises
            injectionBinder.Bind<PlayerCpuBehaviorService>().ToSingleton();
            injectionBinder.Bind<GenarateGameSessionService>().ToSingleton();
            injectionBinder.Bind<StateService>().ToSingleton();

            // init Signals
            injectionBinder.Bind<CreateNewGameSignal>().ToSingleton();
            injectionBinder.Bind<MakeTurnSignal>().ToSingleton();
            injectionBinder.Bind<ArenaInitializedSignal>().ToSingleton();
            injectionBinder.Bind<EndGameSignal>().ToSingleton();
            injectionBinder.Bind<SaveLogSignal>().ToSingleton();
            injectionBinder.Bind<AddHistoryLogSignal>().ToSingleton();
            injectionBinder.Bind<SaveGameSignal>().ToSingleton();
            injectionBinder.Bind<LoadGameSignal>().ToSingleton();

            // Init comands
            commandBinder.Bind<EndGameSignal>().To<GameFinishedCommand>();
            commandBinder.Bind<CreateNewGameSignal>().To<GenerateNewGameCommand>();
            commandBinder.Bind<MakeTurnSignal>().To<MakeTurnCommand>();
            commandBinder.Bind<SaveLogSignal>().To<SaveLogCommand>();
            commandBinder.Bind<AddHistoryLogSignal>().To<AddLogCommand>();
            commandBinder.Bind<SaveGameSignal>().To<SaveGameCommand>();
            commandBinder.Bind<LoadGameSignal>().To<LoadGameCommand>();

            // Init mediators
            mediationBinder.Bind<GameArenaView>().To<GameArenaMediator>();
        }
    }
}