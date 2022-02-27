using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
	[Header("Clips Bank")]
	[SerializeField] private AudioClip deathClip = null;
	[SerializeField] private AudioClip bokeIceClip = null;
	[SerializeField] private AudioClip endingSongClip = null;
	[SerializeField] private List<AudioClip> snowFootsClipList = null;
	[Header("Scene References")]
	[SerializeField] private AudioSource globalAudioSource = null;
	[SerializeField] private AudioSource localAudioSourcePrefab = null;
	[SerializeField] private List<AudioMixerGroup> audioMixers = null;

	[Header("Event References")]
	[SerializeField] private Raycasting camController = null;

	private void Start()
	{
		camController.beakHex += (position) => PlaySoundWithDelay(deathClip, position, 1, 0.3f, 1);
		camController.beakHex += (position) => PlaySoundWithDelay(bokeIceClip, position, 3);
		camController.footstepSound += (position) => PlaySoundWithDelay(snowFootsClipList[Random.Range(0, snowFootsClipList.Count)], position, 0.7f, 0, 2);
	}

	public void PlaySoundWithDelay(AudioClip clip, Vector3 spawnPosition, float length = -1, float delay = 0, int mixerVariant = 0)
	{
		var audioSource = Instantiate(localAudioSourcePrefab, spawnPosition, Quaternion.identity);
		audioSource.outputAudioMixerGroup = audioMixers[mixerVariant];
		audioSource.clip = clip;
		Sequence audioSequence = DOTween.Sequence()
			.AppendInterval(delay)
			.AppendCallback(() => audioSource.Play())
			.AppendInterval(length == -1 ? 0 : length)
			.AppendCallback(() =>
				{
					if (length != -1)
						audioSource.Stop();
				}
			);

		Destroy(audioSource, 10);
	}

}
