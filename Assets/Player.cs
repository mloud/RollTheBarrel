using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour 
{
	[SerializeField]
	Rigidbody barrelRigidBody;

	[SerializeField]
	Elephant elephant;

	public Vector2 Speed()
	{
		return barrelRigidBody.velocity;
	}


	public void OnColliderHit(Collider collider)
	{
		if (collider.tag == "Bonus")
		{
			Game.Instance.OnBonusCollision(collider.gameObject.GetComponent<Bonus>());
		}
		else if (collider.tag == "Finish")
		{
			Game.Instance.OnGameFinished();
		}
		else if (collider.tag == "Obstacle")
		{
			Game.Instance.OnGameFinished();
		}
	}

	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}
}
