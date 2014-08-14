using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour 
{
	public void RotateLeft()
	{
		Game.Instance.RotateScene(Const.Direction.Left);
	}
	
	public void RotateRight()
	{
		Game.Instance.RotateScene(Const.Direction.Right);
	}
	
	public void ResetRotation()
	{
		Game.Instance.ResetRotation();
	}

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			RotateLeft();
		}
		else if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			ResetRotation();
		}


		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			RotateRight();
		}
		else if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			ResetRotation();
		}
	}

}
