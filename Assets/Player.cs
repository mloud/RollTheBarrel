using UnityEngine;
using System.Collections.Generic;


public class Player : MonoBehaviour 
{
	[SerializeField]
	Rigidbody barrelRigidBody;

	[SerializeField]
	Elephant elephant;

	[SerializeField]
	Transform mouthTr;


	private List<CollectibleBase> CollectibleList { get; set; }

	void Awake()
	{
		CollectibleList = new List<CollectibleBase>();
	}

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
		else if (collider.tag == "Collectible")
		{
			CollectibleBase colectible = collider.gameObject.GetComponent<Collectible>();

			if (colectible.CanCollect())
			{
				colectible.Collect(mouthTr);
			}
		}

	}

	public void OnColliderExit(Collider collider)
	{

	}

	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}
}
