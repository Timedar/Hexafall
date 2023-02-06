using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using DG.Tweening;

public class PlayerDirectorControlller : MonoBehaviour
{
	[SerializeField] private GameStatusController gameStatusController = null;
	[SerializeField] private GameObject enviroGameobject = null;
	[SerializeField] private CinemachineVirtualCamera foxFocusCam = null;
	[SerializeField] private CinemachineVirtualCamera glassBallFocusCam = null;
	[SerializeField] private FadeController fadeController = null;

	public event Action startFinaLevelEvent;
	private void Start()
	{
		gameStatusController.endGame += () => EndingSequence().Play();
		startFinaLevelEvent?.Invoke();
	}

	private Sequence EndingSequence()
	{
		return DOTween.Sequence()
					.AppendInterval(1.5f)
					.AppendCallback(() => foxFocusCam.Priority = 17)
					.AppendInterval(8)
					.AppendCallback(() => enviroGameobject.SetActive(true))
					.AppendCallback(() => glassBallFocusCam.Priority = 20)
					.AppendInterval(15)
					.AppendCallback(() => fadeController.TurnFade(true))
					.AppendInterval(3)
					.AppendCallback(() => SceneController.LoadScene(Levels.MainMenu, 0));
	}
}
