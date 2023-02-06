using System;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
	public bool Alive => alive;
	public bool CanMove => canMove;

	private bool alive = true;
	private bool canMove = true;

	public void ChangeCanMoveStatus(bool value) => canMove = value;
	public void ChangeAliveStatus(bool value) => alive = value;
}