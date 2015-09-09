using UnityEngine;
using System.Collections;

public class AnimateMaterialColor : MonoBehaviour {
	
	public GameObject colorPalette;
	public float colorSwapFreq = 1.0f;
	public float randomFrequencyRange = 0.0f;

	private MeshRenderer meshR;
	private ColorPalette palette;
	private float timeEllapsedSinceLastColor = 0f;
	private float currentFreq;
	
	void Start () {
		meshR = GetComponent<MeshRenderer>();
		palette = colorPalette.GetComponent<ColorPalette>();
		currentFreq = colorSwapFreq;
	}
	
	void Update ()
	{
		if (timeEllapsedSinceLastColor > 1f / currentFreq)
		{
			meshR.material.color = palette.GetRandomColor();
			timeEllapsedSinceLastColor = 0f;
			if (randomFrequencyRange != 0f)
				currentFreq = colorSwapFreq + Random.Range(0, randomFrequencyRange);
		}

		timeEllapsedSinceLastColor += Time.deltaTime;
	}
}
