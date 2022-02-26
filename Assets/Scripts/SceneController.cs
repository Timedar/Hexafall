using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class SceneController
{

	public static void LoadScene(Levels level, float delay)
	{
		Sequence loadSequence = DOTween.Sequence()
			.AppendInterval(delay)
			.AppendCallback(() => SceneManager.LoadScene((int)level));
		loadSequence.Play();
	}
}
public enum Levels
{
	MainMenu,
	Lvl1,
	Lvl2,
	Lvl3,
	Lvl4,
	Lvl5,
	Lvl6
}