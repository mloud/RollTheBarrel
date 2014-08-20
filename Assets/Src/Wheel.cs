using UnityEngine;
using System.Collections;

public class Wheel : MonoBehaviour {

	[SerializeField]
	float speed;

	void Start ()
	{
	
	}
	
	void Update () 
	{
		//transform.Rotate(new Vector3(0,0,speed * Time.deltaTime), Space.Self);
		//rigidbody.AddTorque(new Vector3(0, 0, speed), ForceMode.Force);
		rigidbody.angularVelocity = new Vector3(0, 0, speed);
	}
}
