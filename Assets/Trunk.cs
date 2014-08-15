using UnityEngine;
using System.Collections;

public class Trunk : MonoBehaviour
{
	[SerializeField]
	Player player;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Collectible")
		{
			player.OnColliderHit(collider);
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.tag == "Collectible")
		{
			player.OnColliderExit(collider);
		}
	}
}
