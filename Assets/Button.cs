using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		if (GetComponent<Collider>() == null)
		{
			gameObject.AddComponent<BoxCollider>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnMouseDown()
	{
		OnButtonClick(gameObject.name);
	}


	private static void OnButtonClick(string name)
	{
		if (name == "BtnRestart")
		{
			Game.Instance.Restart();
		}
	}
}
