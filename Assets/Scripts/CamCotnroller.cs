using System;
using UnityEngine;

public class CamCotnroller : MonoBehaviour
{
	[SerializeField] private PlayerStatusController playerStatusController = null;
	[SerializeField] private LayerMask layerMask;

	private Camera cam;

	public event Action<Hexbehaviour> GridCellSelected;

	private void Awake()
	{
		cam = Camera.main;
	}

	void Update()
	{
		if (!playerStatusController.Alive || !playerStatusController.CanMove)
			return;

		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		var gridSelected = Physics.Raycast(ray, out RaycastHit hit, layerMask);

		if (gridSelected && Input.GetKeyDown(KeyCode.Mouse0))
			if (hit.transform.parent.TryGetComponent<Hexbehaviour>(out Hexbehaviour hexbehaviour))
				GridCellSelected?.Invoke(hexbehaviour);
	}
}