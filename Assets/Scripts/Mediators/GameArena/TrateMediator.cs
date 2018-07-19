using Models.Arena;
using View.DeckItems;

namespace Mediators.GameArena
{
    public class TrateMediator : TargetMediator<TrateView>
    {
        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        public override void OnRegister()
        {
            View.OnStartDrag += view => { view.IsDragable = view.Side == BattleArena.ActiveSide; };
        }
    }
}