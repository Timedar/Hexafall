using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;

public class Raycasting : MonoBehaviour
{
	[SerializeField] private Transform player = null;
	[SerializeField] private Hexbehaviour currentHex = null;
	[SerializeField] private GridManager gridManager = null;
	private Camera cam;
	private Animator playerAnimator;

	public event Action boom;

	private void Start()
	{
		cam = Camera.main;
		currentHex = gridManager.StartPoint;
		player.transform.position = currentHex.transform.position;
		playerAnimator = player.GetComponentInChildren<Animator>();
	}

	void Update()
	{
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
					currentHex = hexbehaviour;

					if (currentHex.CheckHex())
					{
						var rig = player.GetComponentInChildren<Rigidbody>();
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

		foreach (var hex in currentHex.NeighborHexes)
		{
			Gizmos.color = Color.green;

			if (hex != null)
				Gizmos.DrawSphere(hex.transform.position, 0.5f);
		}
	}
}
