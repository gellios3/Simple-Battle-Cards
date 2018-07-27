using Models.Arena;
using View.GameItems;

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
            View.OnStartDrag += view => { view.CanDraggable = view.Side == BattleArena.ActiveSide; };
        }
    }
}