using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {

	[SerializeField]
	float force;

	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}


	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			collider.gameObject.rigidbody.AddForce(transform.up * force, ForceMode.Impulse);
		}
	}
}
