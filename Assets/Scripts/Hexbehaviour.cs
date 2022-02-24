using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class Hexbehaviour : MonoBehaviour
{
	[Header("Scene References")]
	[SerializeField] private GameObject fineHexObject = null;
	[SerializeField] private GameObject brokenHexObject = null;
	[SerializeField] private Mesh gizmoDrowMesh = null;

	[Header("Hex properties")]
	[SerializeField] private int row = 0;
	[SerializeField] private int elementInRow = 0;
	[SerializeField] private Hexbehaviour[] neighborHexes = new Hexbehaviour[6];
	[SerializeField] private bool correctRoute = true;

	public Hexbehaviour[] NeighborHexes => neighborHexes;
	public bool CorrectRoute => correctRoute;

	public void SetHexInfo(int row, int elementInRow)
	{
		this.row = row;
		this.elementInRow = elementInRow;
	}

	public void SetNeighborInfo(List<HexGrid> gridInfo)
	{
		//Offset of grip
		var moduloValue = row % 2;

		var rowCheckCondition = RowCheckCondition(moduloValue, gridInfo);
		var rowModifier = RowModifier(moduloValue);

		//Upper row
		if (row > 0)
		{
			if (rowCheckCondition)
				neighborHexes[0] = gridInfo[row - 1].gridElementList[elementInRow + rowModifier];


			neighborHexes[1] = gridInfo[row - 1].gridElementList[elementInRow];
		}

		//Middle row
		if (elementInRow > 0)
			neighborHexes[2] = gridInfo[row].gridElementList[elementInRow - 1];

		if (elementInRow + 1 < gridInfo[row].gridElementList.Count)
			neighborHexes[3] = gridInfo[row].gridElementList[elementInRow + 1];

		//Down row
		if (row + 1 < gridInfo.Count)
		{
			if (rowCheckCondition)
				neighborHexes[4] = gridInfo[row + 1].gridElementList[elementInRow + rowModifier];


			neighborHexes[5] = gridInfo[row + 1].gridElementList[elementInRow];
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
			DOTween.Sequence().AppendInterval(UnityEngine.Random.Range(0, 0.08f))
								.AppendCallback(() => hexes?.BrokeHex());
		}
	}

	public bool CheckHex()
	{
		if (!correctRoute)
		{
			BrokeAround();
			return true;
		}

		return false;
	}

	public bool isAvailble(Hexbehaviour hexToCheck)
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

	private bool RowCheckCondition(int moduloResult, List<HexGrid> gridInfo)
	{
		return moduloResult == 0 ? elementInRow > 0 : elementInRow + 1 < gridInfo[row].gridElementList.Count;
	}

	private int RowModifier(int moduloResult)
	{
		return moduloResult == 0 ? -1 : 1;
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if (correctRoute)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(transform.position, 0.7f);
		}

		if (Selection.activeGameObject != transform.gameObject)
			return;

		foreach (var hex in neighborHexes)
		{
			Gizmos.color = Color.green;

			if (hex != null)
				Gizmos.DrawSphere(hex.transform.position, 0.3f);
		}
	}
#endif
}
