using UnityEngine;

namespace Gamefield.Scripts.GameMechanic
{
	public class PlayerTurnMode : MonoBehaviour
	{
		[SerializeField]
		private int _turn=1;
		public int PlayerTurn;

		public PlayerTurnMode()
		{
			PlayerTurn=_turn;
		}
	}
}
