using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BloodTrailControllerScript : MonoBehaviour
{
	private Renderer rendererSnowTrail = null;
	void Start()
	{
		rendererSnowTrail = GetComponent<Renderer>();
		Invoke(nameof(HideBloodTrail), 2);
	}

	private void HideBloodTrail()
	{
		Debug.Log("start");
		float angle = 1;
		DOTween.To(() => angle, x => angle = x, 0, 2)
		.OnUpdate(() =>
		{
			rendererSnowTrail.material.SetFloat("BloodTrail", angle);
		});
	}
}
