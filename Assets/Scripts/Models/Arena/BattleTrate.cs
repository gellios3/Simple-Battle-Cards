using Models.ScriptableObjects;
using UnityEngine;

namespace Models.Arena
{
    public class BattleTrate : BattleItem
    {
        /// <summary>
        /// Defence
        /// </summary>
        public int Defence { get; private set; }

        /// <summary>
        /// Attack
        /// </summary>
        public int Health { get; private set; }

        /// <summary>
        /// Source trate
        /// </summary>
        public Trate SourceTrate { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trate"></param>
        public BattleTrate(Trate trate = null)
        {
            Status = BattleStatus.Wait;
            if (trate != null)
            {
                SourceTrate = trate;
                Defence = trate.Defence;
                Health = trate.Health;
            }
            else
            {
                Defence = Random.Range(0, 5);
                Health = Random.Range(0, 5);
            }
        }
    }
}