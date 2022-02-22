using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
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
				hexbehaviour?.BrokeAround();
			}
		}
	}
}
