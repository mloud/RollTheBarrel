using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour 
{
	[SerializeField]
	Player player;

	void OnCollisionEnter(Collision collision)
	{
		player.OnCustomCollisionEnter(collision);
	}

}
