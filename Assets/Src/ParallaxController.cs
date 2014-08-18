using UnityEngine;
using System.Collections.Generic;

public class ParallaxController : MonoBehaviour 
{
	[SerializeField]
	List<ParallaxLayer> parallaxLayers;



	Vector2 StartPayerPos;
	Vector2 StartPos;

	public void SetPlayerSpeed(Vector2 speed)
	{
		for (int i = 0; i < parallaxLayers.Count; ++i)
		{
			parallaxLayers[i].SetPlayerSpeed(speed);
		}
	}

	void Start () 
	{
		StartPayerPos = Game.Instance.Player.Position();
	}
	
	void Update () 
	{
		Vector2 distance = (Game.Instance.Player.Position() - StartPayerPos);
		
		for (int i = 0; i < parallaxLayers.Count; ++i)
		{
			parallaxLayers[i].Set(distance);
		}
	}


}
