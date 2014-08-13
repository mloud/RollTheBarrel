using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour 
{

	public enum Type
	{
		Left,
		Right
	}

	[SerializeField]
	Type type;



	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

	void OnMouseDown()
	{
		Debug.Log ("Arrow.OnMouseDown() " + type.ToString());
	
		if (type == Type.Left)
		{
			Game.Instance.RotateScene(+1);
		}
		else
		{
			Game.Instance.RotateScene(-1);
		}
	}

	void OnMouseUp()
	{
		Debug.Log ("Arrow.OnMouseUp() " + type.ToString());

		Game.Instance.ResetRotation();
	}
}
