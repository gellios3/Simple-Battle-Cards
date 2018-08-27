using Models.Arena;
using View.GameItems;

namespace Mediators.GameItems
{
    public class CardMediator : TargetMediator<CardView>
    {

        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// On register mediator
        /// </summary>
        public override void OnRegister()
        {
          
            View.OnStartDrag += view =>
            {
                if (view.Side != BattleArena.ActiveSide)
                {
                    view.CanDraggable = false;
                }
                else
                {
                    view.CanDraggable = view.Card.Status != BattleStatus.Sleep;
                }
            };   
        }
    }
}