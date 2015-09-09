using UnityEngine;
using System.Collections;

public class ColorPalette : MonoBehaviour {

	public Color[] colorPalette;

	public Color GetRandomColor()
	{
		return colorPalette[Random.Range (0, colorPalette.Length)];
	}
}
