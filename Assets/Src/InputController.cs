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

	public void DoAction()
	{
		Game.Instance.DoPlayerAction();
	}

	void Update () 
	{

#if UNITY_IPHONE || UNITY_ANDROID
		for (int i = 0; i < Input.touchCount; ++i)
		{
			if (Input.touches[i].phase == TouchPhase.Began)
			{
				if (Input.touches[i].position.y > Screen.height * 0.3)
				{
					DoAction();
				}
				break;
			}
		}

#endif

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			RotateLeft();
		}
		else if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			ResetRotation();
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			DoAction();
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
