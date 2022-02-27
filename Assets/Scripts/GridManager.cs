using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using DG.Tweening;

[System.Serializable]
public class HexGrid
{
	[HideInInspector] public string name;
	public List<Hexbehaviour> gridElementList = new List<Hexbehaviour>();

	public HexGrid(int rowNumber)
	{
		this.name = $"Row {rowNumber}";
	}
}

public class GridManager : MonoBehaviour
{
	[Header("Parameter")]
	[SerializeField] private Camera mainCamera;
	[SerializeField] private Hexbehaviour gridObject = null;
	[SerializeField] private float offsetX = 1.7f;
	[SerializeField] private Vector2 offsetRow = new Vector2(0.85f, 1.45f);
	[SerializeField, Range(1, 50)] private int elementsInRow = 4;
	[SerializeField, Range(1, 50)] private int rows = 4;

	[Header("Elements in rows and column's")]
	[SerializeField] private List<HexGrid> hexGridList = new List<HexGrid>();

	public List<HexGrid> HexGridList => hexGridList;
	public Hexbehaviour StartPoint => startPoint;
	public Hexbehaviour EndingPoint => endingPoint;

	private Vector3 lastGrid;
	private static Hexbehaviour startPoint;
	private static Hexbehaviour endingPoint;

	private void Awake()
	{
		foreach (var grid in hexGridList)
		{
			foreach (var hex in grid.gridElementList)
			{
				hex.gameObject.SetActive(false);
			}
		}

		startPoint = hexGridList[hexGridList.Count - 1].gridElementList.First(x => x.CorrectRoute);
		startPoint.gameObject.SetActive(true);

		endingPoint = hexGridList[0].gridElementList.First(x => x.CorrectRoute);
	}


#if UNITY_EDITOR
	public void GenerateHexMap()
	{
		// SetUpCamToGrid();

		while (this.transform.childCount > 0)
			DestroyImmediate(transform.GetChild(0).gameObject);

		lastGrid = Vector3.zero;

		hexGridList.Clear();

		for (int j = 1; j < rows + 1; j++)
		{
			hexGridList.Add(new HexGrid(j - 1));

			var container = CreateContainerForRow(j);
			for (int i = 0; i < elementsInRow; i++)
			{
				Hexbehaviour singleGrid = PrefabUtility.InstantiatePrefab(gridObject as Hexbehaviour, container.transform) as Hexbehaviour;

				hexGridList[j - 1].gridElementList.Add(singleGrid);

				singleGrid.SetHexInfo(j - 1, i);

				singleGrid.name = i.ToString();

				singleGrid.transform.localPosition = lastGrid + Vector3.right * offsetX;

				lastGrid = singleGrid.transform.localPosition;
			}

			lastGrid = Vector3.zero + new Vector3(offsetRow.x * (j % 2), offsetRow.y * j, 0);
		}

		foreach (var row in hexGridList)
		{
			foreach (var gridElement in row.gridElementList)
			{
				gridElement.SetNeighborInfo(hexGridList);
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
