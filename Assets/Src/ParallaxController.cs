using UnityEngine;
using System.Collections.Generic;

public class ParallaxController : MonoBehaviour 
{
	[SerializeField]
	List<ParallaxLayer> parallaxLayers;


	public void SetPlayerSpeed(Vector2 speed)
	{
		for (int i = 0; i < parallaxLayers.Count; ++i)
		{
			parallaxLayers[i].SetPlayerSpeed(speed);
		}
	}

	void Start () 
	{}
	
	void Update () 
	{}


}
