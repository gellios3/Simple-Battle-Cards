using Models.Arena;
using strange.extensions.mediation.impl;
using Signals.GameArena;
using UnityEngine;
using UnityEngine.UI;

namespace View.GameArena
{
    public class AttackLineView : EventView
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private Image _headImage;

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public InitAttackLineSignal InitAttackLineSignal { get; set; }


        /// <summary>
        /// Init mana signal
        /// </summary>
        [Inject]
        public BattleArena BattleArena { get; set; }

        /// <summary>
        /// Set attack line position 
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        public void SetLineposition(Vector3 startPos, Vector3 endPos)
        {
            endPos = new Vector3(endPos.x, endPos.y, 0);
            _line.SetPosition(0, startPos);
            _line.SetPosition(1, endPos);
            _headImage.transform.position = endPos;

            const float padding = 0.4f;

            if (BattleArena.ActiveSide == BattleSide.Player)
            {
                // get rotate angle
                var angle = AngleBetweenVector2(new Vector2(startPos.x, startPos.y - padding), endPos);
                // rotate cursor
                _headImage.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                // check attack line area
                if (!(angle < 0) || BattleArena.AttackUnit == null || !BattleArena.AttackUnit.HasAttack)
                    return;
                InitAttackLineSignal.Dispatch(false);
            }
            else
            {
                // get rotate angle
                var angle = AngleBetweenVector2(new Vector2(startPos.x, startPos.y + padding), endPos);
                // rotate cursor
                _headImage.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                // check attack line area
                if (!(angle > 0) || BattleArena.AttackUnit == null || !BattleArena.AttackUnit.HasAttack)
                    return;
                InitAttackLineSignal.Dispatch(false);
            }
        }

        /// <summary>
        /// Get angel two vectors
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
        {
            var diference = vec2 - vec1;
            var sign = vec2.y < vec1.y ? -1.0f : 1.0f;
            return Vector2.Angle(Vector2.right, diference) * sign;
        }

        /// <summary>
        /// Set active status
        /// </summary>
        /// <param name="isActive"></param>
        public void SetActive(bool isActive)
        {
            // toggle system cursor
            Cursor.visible = !isActive;
            // reset global positions
            var resetPos = new Vector3(-1, -1, -100);
            _line.SetPosition(0, resetPos);
            _line.SetPosition(1, resetPos);
            _headImage.transform.position = resetPos;
            // toogle line and head image active
            _line.gameObject.SetActive(isActive);
            _headImage.gameObject.SetActive(isActive);
            // set has attack status
            if (BattleArena.AttackUnit != null)
            {
                BattleArena.AttackUnit.HasAttack = isActive;
            }
        }
    }
}