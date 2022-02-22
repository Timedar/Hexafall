using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	[Header("Parameter")]
	[SerializeField] private Camera mainCamera;
	[SerializeField] private Hexbehaviour gridObject = null;
	[SerializeField] private float offsetX = 1.7f;
	[SerializeField] private Vector2 offsetRow = new Vector2(0.85f, 1.45f);
	[SerializeField, Range(4, 50)] private int elementsInRow = 4;
	[SerializeField, Range(4, 50)] private int rows = 4;

	private List<List<Hexbehaviour>> GridElementList = new List<List<Hexbehaviour>>();
	private Vector3 lastGrid;

	private event Action spawn;

#if UNITY_EDITOR
	public void GenerateHexMap()
	{
		SetUpCamToGrid();

		while (this.transform.childCount > 0)
			DestroyImmediate(transform.GetChild(0).gameObject);

		lastGrid = Vector3.zero;

		GridElementList.Clear();

		for (int j = 1; j < rows + 1; j++)
		{
			GridElementList.Add(new List<Hexbehaviour>());

			var container = CreateContainerForRow(j);
			for (int i = 0; i < elementsInRow; i++)
			{
				Hexbehaviour singleGrid = PrefabUtility.InstantiatePrefab(gridObject as Hexbehaviour, container.transform) as Hexbehaviour;

				GridElementList[j - 1].Add(singleGrid);

				singleGrid.SetHexInfo(j - 1, i);

				singleGrid.name = i.ToString();

				singleGrid.transform.localPosition = lastGrid + Vector3.right * offsetX;

				lastGrid = singleGrid.transform.localPosition;
			}

			lastGrid = Vector3.zero + new Vector3(-offsetRow.x * (j % 2), offsetRow.y * j, 0);
		}

		foreach (var row in GridElementList)
		{
			foreach (var gridElement in row)
			{
				gridElement.SetNeighborInfo(GridElementList);
			}
		}

		Debug.Log("Done Generating");
	}
#endif

	private void SetUpCamToGrid()
	{
		mainCamera.orthographicSize = elementsInRow * 4 / 5;
		this.transform.localPosition = new Vector3((-elementsInRow), 0, rows * (rows > 6 ? 5.5f / 11 : 22 / 100));

	}
	private GameObject CreateContainerForRow(int row)
	{
		var holder = new GameObject();

		holder.transform.parent = this.transform;
		holder.transform.localEulerAngles = Vector3.zero;
		holder.transform.localPosition = Vector3.zero;

		holder.name = "Row" + row.ToString();

		return holder;
	}
}
