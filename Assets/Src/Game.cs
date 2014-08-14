using UnityEngine;
using System.Collections;


public interface IGame
{
	void Restart();

	void OnBonusCollision(Bonus bonus);
	void OnGameFinished();

}

public class Game : MonoBehaviour, IGame
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
	public InputController InputController { private set; get; }
	public LevelStatistic LevelStatistic { private set; get; }

	private static Game _instance;

	private float? _rotationSpeed;

	private bool _resetingRotation;


	public void RotateScene(Const.Direction dir)
	{
		_resetingRotation = false;
		_rotationSpeed = (dir == Const.Direction.Right ? -1 : 1) * rotationSpeed;
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

	public void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}


	void Awake()
	{
		_instance = this;

		Init ();
	}

	void Init()
	{
		// Find Player 
		Player = GameObject.FindObjectOfType<Player>();
		// Find Parallax 
		Parallax = GameObject.FindObjectOfType<ParallaxController>();

		// Create Level Statistic
		LevelStatistic = new LevelStatistic();

		// Create InputController
		GameObject inputControllerGo = new GameObject("__InputController__");
		inputControllerGo.transform.parent = transform;
		InputController = inputControllerGo.AddComponent<InputController>();
	}

	void Start ()
	{
		LevelStatistic.StartTime = Time.time;
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


	public void OnBonusCollision(Bonus bonus)
	{
		bonus.FlyUp();
		LevelStatistic.CollectedBonus++;
	}

	public void OnGameFinished()
	{
	
	}



}
