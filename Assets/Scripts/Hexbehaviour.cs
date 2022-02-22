using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Hexbehaviour : MonoBehaviour
{
	[SerializeField] private GameObject fineHexObject = null;
	[SerializeField] private GameObject brokenHexObject = null;

	public Hexbehaviour[] NeighborHexes => neighborHexes;

	[SerializeField] private Hexbehaviour[] neighborHexes = new Hexbehaviour[6];

	public int row, elementInRow;

	public void SetHexInfo(int row, int elementInRow)
	{
		this.row = row;
		this.elementInRow = elementInRow;
	}

	public void SetNeighborInfo(List<List<Hexbehaviour>> gridInfo)
	{
		//Offset of grip
		var moduloValue = row % 2;

		var rowCheckCondition = RowCheckCondition(moduloValue, gridInfo);
		var rowModifier = RowModifier(moduloValue);

		//Upper row
		if (row > 0)
		{
			if (rowCheckCondition)
				neighborHexes[0] = gridInfo[row - 1][elementInRow + rowModifier];


			neighborHexes[1] = gridInfo[row - 1][elementInRow];
		}

		//Middle row
		if (elementInRow > 0)
			neighborHexes[2] = gridInfo[row][elementInRow - 1];

		if (elementInRow + 1 < gridInfo[row].Count)
			neighborHexes[3] = gridInfo[row][elementInRow + 1];

		//Down row
		if (row + 1 < gridInfo.Count)
		{
			if (rowCheckCondition)
				neighborHexes[4] = gridInfo[row + 1][elementInRow + rowModifier];


			neighborHexes[5] = gridInfo[row + 1][elementInRow];
		}
	}

	public void BrokeHex()
	{
		fineHexObject.SetActive(false);
		brokenHexObject.SetActive(true);
	}

	public void BrokeAround()
	{
		BrokeHex();
		foreach (var hexes in neighborHexes)
		{
			hexes?.BrokeHex();
		}
	}

	public bool CheckHex(Hexbehaviour hexToCheck)
	{
		if (hexToCheck == null)
			return false;

		foreach (var hex in neighborHexes)
		{
			if (hexToCheck == hex)
				return true;
		}

		return false;
	}

	private bool RowCheckCondition(int moduloResult, List<List<Hexbehaviour>> gridInfo)
	{
		return moduloResult == 0 ? elementInRow > 0 : elementInRow + 1 < gridInfo[row].Count;
	}

	private int RowModifier(int moduloResult)
	{
		return moduloResult == 0 ? -1 : 1;
	}

	private void OnDrawGizmos()
	{
		if (Selection.activeGameObject != transform.gameObject)
			return;

		foreach (var hex in neighborHexes)
		{
			Gizmos.color = Color.green;

			if (hex != null)
				Gizmos.DrawSphere(hex.transform.position, 0.5f);
		}
	}
}
