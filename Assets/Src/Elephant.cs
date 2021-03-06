﻿using UnityEngine;
using System.Collections;

public class Elephant : MonoBehaviour
{
	[SerializeField]
	Transform barrel;

	[SerializeField]
	Transform trunk;

	[SerializeField]
	Transform trunkCenter;




	[SerializeField]
	Player player;

	private Vector3 barrelShift;

	void Start ()
	{
		// shift abobe barrel
		barrelShift = transform.position - barrel.position;
	}
	

	void Update () 
	{
		transform.eulerAngles = Vector3.zero;	
		transform.position = barrel.position + barrelShift;

		if (Mathf.Abs(barrel.rigidbody.velocity.x) > 0.1f)
		{
			Vector3 localScale = transform.localScale;

			localScale.x = barrel.rigidbody.velocity.x > 0 ? 1 : -1;

			transform.localScale = localScale;
		}

		// rotate to follow gravity vector

		transform.rotation = Camera.main.transform.rotation;


		trunk.position = trunkCenter.position;
		trunk.rotation = trunkCenter.rotation;
		trunk.localScale = trunkCenter.lossyScale;



	}


	void OnTriggerEnter(Collider collider)
	{
		player.OnCustomTriggerEnter(collider);
	}

}
