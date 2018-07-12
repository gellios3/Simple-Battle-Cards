using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
