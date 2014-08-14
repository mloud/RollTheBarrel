using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour 
{
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Game.Instance.RotateScene(+1);
		}
		else if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			Game.Instance.ResetRotation();
		}


		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Game.Instance.RotateScene(-1);
		}
		else if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			Game.Instance.ResetRotation();
		}
	}
}
