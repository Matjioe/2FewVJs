using UnityEngine;
using System.Collections;

public class ExpressionEvaluator : MonoBehaviour {

	public GameObject body;
	public float scale = 1.0f;

	public void SetExpressionValue1(float val)
	{
		scale = val;
	}

	public void Compute()
	{
		body.transform.localScale = Vector3.one * scale;
	}
}
