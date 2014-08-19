using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour 
{
	[SerializeField]
	public float Speed;


	private Vector2 _playerSpeed;


	public void SetPlayerSpeed(Vector2 speed)
	{
		_playerSpeed = speed;
	}

	void Start ()
	{}


	void Update ()
	{
		//renderer.material.mainTextureOffset = new Vector2(Speed * Time.time * -_playerSpeed.x, 0);
		//renderer.material.mainTextureOffset =  new Vector2(Random.Range(0,1),0);

	}


	public void Set(Vector2 distance)
	{
		renderer.material.mainTextureOffset = (distance / 100) * Speed;
	}
}
