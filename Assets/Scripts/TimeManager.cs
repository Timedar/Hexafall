using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI textMeshComponent = null;
	private float timeToDisplay = 0;
	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	private void Update()
	{
		timeToDisplay += Time.deltaTime;

		float minutes = Mathf.FloorToInt(timeToDisplay / 60);
		float seconds = Mathf.FloorToInt(timeToDisplay % 60);
		textMeshComponent.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}
