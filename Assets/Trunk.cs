using UnityEngine;
using System.Collections;

public class Trunk : MonoBehaviour
{
	[SerializeField]
	Player player;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Collectible"|| collider.gameObject.tag == "Hook")
		{
			player.OnCustomTriggerEnter(collider);
		}
	}

	void OnTriggerExit(Collider collider)
	{
		Debug.Log ("Exit: " + collider.gameObject.name);
		if (collider.gameObject.tag == "Collectible" || collider.gameObject.tag == "Hook")
		{
			player.OnCustomTriggerExit(collider);
		}
	}
}
