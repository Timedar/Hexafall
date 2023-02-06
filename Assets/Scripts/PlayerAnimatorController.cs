using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
	[Header("Animator bool names")]
	[SerializeField] private string finishedGame = "mapFinished";
	[SerializeField] private string fallAniamation = "Grounded";
	[SerializeField] private string movementAnimation = "Move";

	[Header("References")]
	[SerializeField] private Animator playerAnimator = null;

	public void PlayMoveAnim(bool value) => PlayAnimation(movementAnimation, value);
	public void PlayFallanim() => PlayAnimation(fallAniamation, true);
	public void PlayEndGameAnimn() => PlayAnimation(finishedGame, true);

	private void PlayAnimation(string animName, bool value)
	{
		playerAnimator.SetBool(animName, value);
	}
}