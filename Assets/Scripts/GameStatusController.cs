using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatusController : MonoBehaviour
{
	[SerializeField] private FadeController fadeController = null;
	[SerializeField] private GridManager gridManager = null;
	[SerializeField] private CamCotnroller camCotnroller = null;
	[SerializeField] private PlayerStatusController statusController = null;
	[SerializeField] private PlayerAnimatorController playerAnimatorController;

	private Hexbehaviour endGameHex = null;
	public event Action<Vector3> beakHex;
	public event Action endGame;

	private void Start()
	{
		camCotnroller.GridCellSelected += CheckGameCondition;
	}

	private void CheckGameCondition(Hexbehaviour currentHex)
	{
		if (endGameHex != null)
			if (endGameHex == currentHex)
			{
				statusController.ChangeCanMoveStatus(false);

				endGame.Invoke();

				currentHex.ShowNearbyHexes(false);

				playerAnimatorController.PlayEndGameAnimn();
			}

		if (currentHex == gridManager.EndingPoint)
		{
			if (SceneManager.GetActiveScene().buildIndex + 1 > 6)
			{
				fadeController.TurnFade(true);
				SceneController.LoadScene(Levels.MainMenu, 2);
				return;
			}

			SceneController.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, 2);
		}

		if (currentHex.CheckHex())
		{
			statusController.ChangeAliveStatus(false);

			beakHex?.Invoke(currentHex.transform.position);

			SceneController.LoadScene(SceneManager.GetActiveScene().buildIndex, 2);

			playerAnimatorController.PlayFallanim();

			var rig = playerAnimatorController.GetComponentInChildren<Rigidbody>();
			rig.isKinematic = false;
			rig.AddTorque(Vector3.forward * 15);
			rig.AddForce(Vector3.up * 5);
		}
	}
}