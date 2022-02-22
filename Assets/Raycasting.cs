using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Raycasting : MonoBehaviour
{
	[SerializeField] private Transform player = null;
	[SerializeField] private Hexbehaviour currentHex = null;

	private Camera cam;

	private void Awake()
	{
		cam = Camera.main;
	}

	void Update()
	{
		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit) && Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (hit.transform.CompareTag("Brockable"))
			{
				var hexbehaviour = hit.transform.parent.GetComponent<Hexbehaviour>();

				if (currentHex.CheckHex(hexbehaviour))
				{
					player.DOMove(hexbehaviour.transform.position, 1);
					currentHex = hexbehaviour;
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
