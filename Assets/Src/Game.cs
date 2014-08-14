using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	[SerializeField]
	Transform sceneRoot;

	[SerializeField]
	float resetRotationSpeed;

	[SerializeField]
	float rotationSpeed;

	[SerializeField]
	float maxRotation;



	public static Game Instance { get { return _instance; } }

	public Player Player { get; private set; }
	public ParallaxController Parallax { private get; set; }

	private static Game _instance;

	private float? _rotationSpeed;

	private bool _resetingRotation;


	public void RotateScene(int dir)
	{
		_resetingRotation = false;
		_rotationSpeed = dir * rotationSpeed;
	}

	public void ResetRotation()
	{
		_resetingRotation = true;

		float dist360 = Mathf.Abs(360 - sceneRoot.eulerAngles.z);
		float dist0 = Mathf.Abs(sceneRoot.eulerAngles.z);

		_rotationSpeed = resetRotationSpeed;

		if (dist360 > dist0)
		{
			_rotationSpeed *= -1;
		}

	}

	void Awake()
	{
		_instance = this;

		Player = GameObject.FindObjectOfType<Player>();
		Parallax = GameObject.FindObjectOfType<ParallaxController>();
	}

	void Start ()
	{
	
	}
	
	void Update ()
	{

		// titlting
		if (_rotationSpeed != null)
		{

			sceneRoot.Rotate(0,0, _rotationSpeed.Value * Time.deltaTime * 10.0f);


			// check for zero
			if (_resetingRotation)
			{
				if (Mathf.Abs(sceneRoot.eulerAngles.z) < 1.0f)
				{
					_rotationSpeed = null;
					_resetingRotation = false;
				}
			}

		}

		//paralax
		Vector2 speed = Player.Speed();
		Parallax.SetPlayerSpeed(speed);


	}



}
