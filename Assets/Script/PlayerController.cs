using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float step = 1f;
	public float halfTerrainWidth = 10f;
	public float halfTerrainHeight = 5f;

	public void MoveForward() { Move(Vector3.forward); }
	public void MoveBack()    { Move(Vector3.back);    }
	public void MoveLeft()    { Move(Vector3.left);    }
	public void MoveRight()   { Move(Vector3.right);   }

	private Vector3 newPos;
	
	public void Move(Vector3 direction)
	{
		newPos = transform.position + direction * step;
		if (newPos.x >  halfTerrainWidth ) newPos.x =  halfTerrainWidth;
		if (newPos.x < -halfTerrainWidth ) newPos.x = -halfTerrainWidth;
		if (newPos.z >  halfTerrainHeight) newPos.z =  halfTerrainHeight;
		if (newPos.z < -halfTerrainHeight) newPos.z = -halfTerrainHeight;

		transform.position = newPos;
	}
}
