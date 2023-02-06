using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementController : MonoBehaviour
{
	[SerializeField] private GridManager gridManager = null;
	[SerializeField] private TrailManager trailManager = null;
	[SerializeField] private CamCotnroller camCotnroller = null;
	[SerializeField] private PlayerAnimatorController animatorController = null;

	private Animator playerAnimator;
	private Hexbehaviour currentHex;
	private bool canMove = false;

	public event Action<Vector3> footstepSound;

	private void Start()
	{
		currentHex = gridManager.StartPoint;
		transform.position = currentHex.transform.position;
		playerAnimator = GetComponentInChildren<Animator>();
		currentHex.ShowNearbyHexes(true);
		var sound = FindObjectOfType<SoundManager>();
		camCotnroller.GridCellSelected += TryMove;

		// sound.camController = this;
		// sound.Init();

		if (trailManager == null)
		{
			Debug.LogWarning("No Ghost trail!", this.gameObject);
			canMove = true;
			return;
		}

		trailManager.ghostFoxFinishedRun += () => canMove = true;
	}

	private void TryMove(Hexbehaviour gridCell)
	{
		if (currentHex.IsAvailble(gridCell))
		{
			Move(gridCell);
			ChangeNearbyHexesVisibility(gridCell);
		}
	}

	private void Move(Hexbehaviour hexbehaviour)
	{
		transform.DOMove(hexbehaviour.transform.position, 1).OnStart(() => animatorController.PlayMoveAnim(true)).OnComplete(() => animatorController.PlayMoveAnim(false));
		transform.LookAt(hexbehaviour.transform.position);
		footstepSound?.Invoke(transform.position);
	}

	private void ChangeNearbyHexesVisibility(Hexbehaviour hexbehaviour)
	{
		currentHex.ShowNearbyHexes(false);
		currentHex = hexbehaviour;
		currentHex.ShowNearbyHexes(true);
	}

	private void OnDrawGizmos()
	{
		if (currentHex == null)
			return;

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(currentHex.transform.position, 0.6f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.6f);

		Gizmos.color = Color.green;
		ShowNearbyHexesGizmo();
	}

	private void ShowNearbyHexesGizmo()
	{
		foreach (var hex in currentHex.NeighborHexes)
		{
			Gizmos.color = Color.green;
			if (hex != null)
				Gizmos.DrawSphere(hex.transform.position, 0.5f);
		}
	}
}