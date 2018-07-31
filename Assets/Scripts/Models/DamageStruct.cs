using View.GameArena;
using View.GameItems;

namespace Models
{
    public struct DamageStruct
    {
        public BattleUnitView SourceCardView { get; set; }

        public BattleUnitView DamageCardView { get; set; }
    }
}