using UnityEngine;
using System.Collections;

public class Elephant : MonoBehaviour
{

	[SerializeField]
	Transform barrel;

	private Vector3 barrelShift;

	void Start ()
	{
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
	}
}
