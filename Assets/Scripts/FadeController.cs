using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeController : MonoBehaviour
{
	[SerializeField] private Image fadeImage = null;
	void Start()
	{
		// fadeImage.color = Color.black;
		TurnFade(true, true);
		TurnFade(false);
	}

	public void TurnFade(bool value, bool instant = false)
	{
		fadeImage.DOFade(value ? 1 : 0, instant ? 0 : 1);
	}
}
