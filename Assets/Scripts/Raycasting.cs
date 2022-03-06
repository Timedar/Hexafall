using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class Raycasting : MonoBehaviour
{
	[SerializeField] private Transform player = null;
	[SerializeField] private Hexbehaviour currentHex = null;
	[SerializeField] private GridManager gridManager = null;
	[SerializeField] private Hexbehaviour endGameHex = null;
	[SerializeField] private FadeController fadeController = null;
	[SerializeField] private TrailManager trailManager = null;
	private Camera cam;
	private Animator playerAnimator;

	public event Action<Vector3> beakHex;
	public event Action<Vector3> footstepSound;
	public event Action endGame;
	public bool alive = true;
	public bool canMove = false;

	private void Start()
	{
		cam = Camera.main;
		currentHex = gridManager.StartPoint;
		player.transform.position = currentHex.transform.position;
		playerAnimator = player.GetComponentInChildren<Animator>();
		currentHex.ShowNearbyHexes(true);
		var sound = FindObjectOfType<SoundManager>();
		sound.camController = this;
		sound.Init();


		if (trailManager == null)
		{
			Debug.LogWarning("Trail manager is null on Raycast script! No Ghost trail!");
			canMove = true;
			return;
		}

		trailManager.ghostFoxFinishedRun += () => canMove = true;
	}

	void Update()
	{
		if (!alive || !canMove)
			return;

		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Grid")) && Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (hit.transform.CompareTag("Brockable"))
			{
				var hexbehaviour = hit.transform.parent.GetComponent<Hexbehaviour>();

				if (currentHex.isAvailble(hexbehaviour))
				{
					player.DOMove(hexbehaviour.transform.position, 1).OnStart(() => playerAnimator.SetBool("Move", true)).OnComplete(() => playerAnimator.SetBool("Move", false));
					player.LookAt(hexbehaviour.transform.position);
					footstepSound.Invoke(player.transform.position);

					currentHex.ShowNearbyHexes(false);
					currentHex = hexbehaviour;
					currentHex.ShowNearbyHexes(true);

					if (endGameHex != null)
						if (endGameHex == currentHex)
						{
							canMove = false;
							endGame.Invoke();
							currentHex.ShowNearbyHexes(false);
							playerAnimator.SetBool("mapFinished", true);
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
						alive = false;
						beakHex?.Invoke(currentHex.transform.position);
						SceneController.LoadScene(SceneManager.GetActiveScene().buildIndex, 2);
						var rig = player.GetComponentInChildren<Rigidbody>();
						playerAnimator.SetBool("Grounded", false);
						rig.isKinematic = false;
						rig.AddTorque(Vector3.forward * 15);
						rig.AddForce(Vector3.up * 5);
					}
				}
			}
		}
	}


	private void OnDrawGizmos()
	{

		if (currentHex == null)
			return;

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(currentHex.transform.position, 0.6f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(player.transform.position, 0.6f);

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
