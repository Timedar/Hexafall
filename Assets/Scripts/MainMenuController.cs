using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	[SerializeField] private Image fadeImage = null;
	[SerializeField] private CanvasGroup creditsCanvasGroup = null;
	[SerializeField] private float fadeTime = 0.3f;
	public void TurnCredits(bool value)
	{
		creditsCanvasGroup.DOFade(value ? 1 : 0, fadeTime).OnComplete(() =>
		{
			creditsCanvasGroup.interactable = value;
			creditsCanvasGroup.blocksRaycasts = value;
		});
	}

	public void LoadGame()
	{
		fadeImage.DOFade(true ? 1 : 0, fadeTime).OnComplete(() => SceneController.LoadScene(Levels.Lvl1, 0));
	}
}
