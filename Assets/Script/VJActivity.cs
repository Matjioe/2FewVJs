using UnityEngine;
using System.Collections;

public class VJActivity : MonoBehaviour
{
	public MyGameManager gameManager;
	public float messageDuration = 4.0f;

	private float ellapsedTime = 0f;
	private bool messageShown = false;

	public virtual void OnStartActivity()
	{
		ellapsedTime = 0f;
		gameManager.ActivateActivityMessage(true);
		messageShown = false;
	}

	public virtual void OnUpdateActivity()
	{
		if (messageShown == false)
		{
			ellapsedTime += Time.deltaTime;
			if (ellapsedTime > messageDuration)
			{
				gameManager.ActivateActivityMessage(false);
				messageShown = true;
				ellapsedTime = 0f;
			}
		}
	}
}
