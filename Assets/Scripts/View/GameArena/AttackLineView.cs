using strange.extensions.mediation.impl;
using Signals.GameArena;
using UnityEngine;

namespace View.GameArena
{
    public class AttackLineView : EventView
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private LineRenderer _cursor;
        [SerializeField] private float _prefixX = 0.3f;
        [SerializeField] private float _prefixY = 0.4f;

        /// <summary>
        /// Battle
        /// </summary>
        [Inject]
        public InitAttackLineSignal InitAttackLineSignal { get; set; }

        private const float PiDiv180 = Mathf.PI / 180;

        public void SetLineposition(Vector3 startPos, Vector3 endPos)
        {
            _line.SetPosition(0, startPos);
            _line.SetPosition(1, new Vector3(endPos.x, endPos.y, 0));

            //Congrats, you made it really hard on yourself.
            var angle = AngleBetweenVector2(new Vector2(startPos.x, startPos.y - 0.1f), endPos);

            if (angle < 0)
            {
                _line.SetPosition(0, startPos);
                _line.SetPosition(1, startPos);
                InitAttackLineSignal.Dispatch(false);
            }

//            _cursor.SetPosition(0, Rotate(endPos, new Vector3(
//                endPos.x - _prefixX, endPos.y - _prefixY, 0
//            ), 0f));
//            _cursor.SetPosition(1, new Vector3(endPos.x, endPos.y, 0));
//            _cursor.SetPosition(2, Rotate(endPos, new Vector3(
//                endPos.x + _prefixX, endPos.y - _prefixY, 0
//            ), 0f));
        }

        private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
        {
            var diference = vec2 - vec1;
            var sign = vec2.y < vec1.y ? -1.0f : 1.0f;
            return Vector2.Angle(Vector2.right, diference) * sign;
        }
//
//
//        private Vector3 Rotate(Vector3 pivotPoint, Vector3 pointToRotate, float angle)
//        {
//            var nx = pointToRotate.x - pivotPoint.x;
//            var ny = pointToRotate.y - pivotPoint.y;
//            angle = -angle * PiDiv180;
//            return new Vector3(
//                Mathf.Cos(angle) * nx - Mathf.Sin(angle) * ny + pivotPoint.x,
//                Mathf.Sin(angle) * nx + Mathf.Cos(angle) * ny + pivotPoint.y, 0);
//        }

        public void SetActive(bool isActive)
        {
            _line.gameObject.SetActive(isActive);
//            _cursor.gameObject.SetActive(isActive);
        }
    }
}