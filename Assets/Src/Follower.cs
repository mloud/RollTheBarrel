using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

	[SerializeField]
	Transform tranformToFollow;

	[SerializeField]
	float speed;

	void Start () 
	{
	
	}
	
	void Update () 
	{
		Vector3 newPos = Vector3.Lerp(transform.position, tranformToFollow.position, Time.deltaTime * speed);
		newPos.z = transform.position.z; // keep.z

		transform.position = newPos;
	}


}
