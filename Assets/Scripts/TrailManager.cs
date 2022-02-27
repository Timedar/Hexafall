using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TrailManager : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField] private GameObject trail = null;
	[SerializeField] private List<Hexbehaviour> correctTrailWay = new List<Hexbehaviour>();
	[SerializeField, Range(0.01f, 1)] private float speedOfTrail = 0.3f;
	[SerializeField] private float heightOfTrace = 1;
	[SerializeField] private Button btn;
	private GridManager gridManager;

	public event Action ghostFoxFinishedRun;

	public void SetSpeedOfFox(float speed)
	{
		Debug.Log("SetSpeedFox");
		speedOfTrail = speed;
	}
	private void Start()
	{
		InitParameters();

		trail.transform.position = gridManager.StartPoint.transform.position;

		StartCoroutine(TrailPath());
	}

	private void InitParameters()
	{
		gridManager = FindObjectOfType<GridManager>();

	}

	private IEnumerator TrailPath()
	{
		foreach (var item in correctTrailWay)
		{
			trail.transform.DOMove(item.transform.position + Vector3.up * heightOfTrace, speedOfTrail).SetEase(Ease.Linear);
			trail.transform.LookAt(item.transform.position);
			yield return new WaitForSeconds(speedOfTrail);
		}

		trail.gameObject.SetActive(false);
		ghostFoxFinishedRun.Invoke();
		btn.gameObject.SetActive(false);
		yield return null;

	}
}
