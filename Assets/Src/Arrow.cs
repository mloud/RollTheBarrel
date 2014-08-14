using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour 
{

	[SerializeField]
	Const.Direction direction;



	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

	void OnMouseDown()
	{
		Debug.Log ("Arrow.OnMouseDown() " + direction.ToString());
	
		if (direction == Const.Direction.Left)
		{
			Game.Instance.InputController.RotateLeft();
		}
		else
		{
			Game.Instance.InputController.RotateRight();
		}
	}

	void OnMouseUp()
	{
		Debug.Log ("Arrow.OnMouseUp() " + direction.ToString());

		Game.Instance.InputController.ResetRotation();
	}
}
