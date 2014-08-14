using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour 
{
	[SerializeField]
	Rigidbody barrelRigidBody;


	public Vector2 Speed()
	{
		return barrelRigidBody.velocity;
	}


	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}
}
