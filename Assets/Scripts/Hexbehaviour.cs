using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexbehaviour : MonoBehaviour
{
	[SerializeField] private GameObject fineHexObject = null;
	[SerializeField] private GameObject brokenHexObject = null;

	[SerializeField] private Hexbehaviour[] neighborHexes = new Hexbehaviour[6];

	public int row, elementInRow;

	public void SetHexInfo(int row, int elementInRow)
	{
		this.row = row;
		this.elementInRow = elementInRow;
	}

	public void SetNeighborInfo(List<List<Hexbehaviour>> gridInfo)
	{
		Debug.Log(row + " " + elementInRow);
		if (row > 0)
		{
			if (elementInRow > 0)
				neighborHexes[0] = gridInfo[row - 1][elementInRow - 1];

			if (elementInRow < gridInfo[row].Count)
				neighborHexes[1] = gridInfo[row - 1][elementInRow];
		}

		if (elementInRow > 0)
			neighborHexes[2] = gridInfo[row][elementInRow - 1];

		if (elementInRow + 1 < gridInfo[row].Count)
			neighborHexes[3] = gridInfo[row][elementInRow + 1];

		if (row + 1 < gridInfo.Count)
		{
			if (elementInRow > 0)
				neighborHexes[4] = gridInfo[row + 1][elementInRow - 1];

			if (elementInRow < gridInfo[row].Count)
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
}
