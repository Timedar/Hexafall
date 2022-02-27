using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerDirectorControlller : MonoBehaviour
{
	[SerializeField]
	private Raycasting camController = null;
	private void Start()
	{
		camController.endGame += () => GetComponent<PlayableDirector>().Play();
	}
}
