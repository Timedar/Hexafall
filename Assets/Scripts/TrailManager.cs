using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private GameObject trail = null;
	[SerializeField] private List<Hexbehaviour> correctTrailWay = new List<Hexbehaviour>();
	[SerializeField, Range(0.01f, 1)] private float speedOfTrail = 0.3f;
	[SerializeField] private float heightOfTrace = 1;
	private GridManager gridManager;
	private WaitForSeconds waitForSeconds;

	private void Start()
	{
		InitParameters();

		trail.transform.position = gridManager.StartPoint.transform.position;

		foreach (var row in gridManager.HexGridList)
		{
			correctTrailWay.AddRange(row.gridElementList.Where(hex => hex.CorrectRoute).ToList());
		}

		correctTrailWay.Reverse();
		StartCoroutine(TrailPath());

	}

	private void InitParameters()
	{
		gridManager = GetComponent<GridManager>();
		waitForSeconds = new WaitForSeconds(speedOfTrail);
	}

	private IEnumerator TrailPath()
	{
		foreach (var item in correctTrailWay)
		{
			trail.transform.DOMove(item.transform.position + Vector3.up * heightOfTrace, 0.3f).SetEase(Ease.Linear);
			yield return waitForSeconds;
		}
		yield return null;
	}
}
