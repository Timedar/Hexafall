using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeController : MonoBehaviour
{
	[SerializeField] private Image fadeImage = null;
	[SerializeField] private float fadeDuration = 1f;

	void Start()
	{
		SetupFadeOnGameInit();
	}

	private void SetupFadeOnGameInit()
	{
		TurnFade(true, true);
		TurnFade(false);
	}

	public void TurnFade(bool value, bool instant = false)
	{
		fadeImage.DOFade(value ? 1 : 0, instant ? 0 : fadeDuration);
	}
}
