using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BloodTrailControllerScript : MonoBehaviour
{
	[SerializeField] private Material snowMaterial = null;
	void Start()
	{
		Invoke(nameof(HideBloodTrail), 2);
	}

	private void HideBloodTrail()
	{
		float bloodTextureAlfa = 1;
		DOTween.To(() => bloodTextureAlfa, x => bloodTextureAlfa = x, 0, 2)
		.OnUpdate(() =>
		{
			snowMaterial.SetFloat("_BloodTrail", bloodTextureAlfa);
		});
	}

	private void OnDestroy()
	{
		snowMaterial.SetFloat("_BloodTrail", 1);
	}
}
