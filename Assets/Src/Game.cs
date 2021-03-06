﻿using UnityEngine;
using System.Collections;


public interface IGame
{
	void Restart();

	void OnBonusCollision(Bonus bonus);
	void OnLevelFinished();
	void Pause();
	void UnPause();

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
	public float maxRotation;

	[SerializeField]
	public float specialForceMax;

	[SerializeField]
	public float specialAirForce;


	[SerializeField]
	float gravity;

	[SerializeField]
	public float HitParamGameOver;



	public static Game Instance { get { return _instance; } }

	public UI UI { get; private set; }
	public Player Player { get; private set; }
	public ParallaxController Parallax { private get; set; }
	public InputController InputController { private set; get; }
	public LevelStatistic LevelStatistic { private set; get; }
	public float CameraZ { get; set;} 

	private static Game _instance;

	private float? _rotationSpeed;

	private bool _resetingRotation;

	public void DoPlayerAction()
	{
		Player.DoAction();
	}

	public void RotateScene(Const.Direction dir)
	{
		_resetingRotation = false;
		_rotationSpeed = (dir == Const.Direction.Right ? 1 : -1) * rotationSpeed;
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

	public void Pause()
	{
		Time.timeScale = 0;
	}

	public void UnPause()
	{
		Time.timeScale = 1.0f;
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
		// Find UI
		UI = GameObject.FindObjectOfType<UI>();

		// Create Level Statistic
		LevelStatistic = new LevelStatistic();

		// Create InputController
		GameObject inputControllerGo = new GameObject("__InputController__");
		inputControllerGo.transform.parent = transform;
		InputController = inputControllerGo.AddComponent<InputController>();
	}

	void Start ()
	{
		UnPause();
		LevelStatistic.StartTime = Time.time;
	}
	
	void Update ()
	{
		// keep gravity
		Physics.gravity = -Camera.main.transform.up * gravity;

		UpdateCameraRotation();


		//paralax
		Vector2 speed = Player.Speed();
		Parallax.SetPlayerSpeed(speed);
	}

	private void UpdateCameraRotation()
	{
		// titlting
		if (_rotationSpeed != null)
		{
			
			sceneRoot.Rotate(0,0, _rotationSpeed.Value * Time.deltaTime * 10.0f);
			
			CameraZ = sceneRoot.eulerAngles.z;
			
			if (CameraZ > 180)
			{
				CameraZ = -(360 - CameraZ);
			}
			if (CameraZ > maxRotation || CameraZ < -maxRotation)
			{
				CameraZ = Mathf.Sign(CameraZ) * maxRotation;
				Vector3 eulerRot = sceneRoot.eulerAngles;
				eulerRot.z = CameraZ;
				sceneRoot.eulerAngles = eulerRot;
			}
			
			
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
	}

	public void OnBonusCollision(Bonus bonus)
	{
		bonus.FlyUp();
		LevelStatistic.Bonus++;
	}

	public void OnCollectibleCollision(CollectibleBase collectible)
	{
		LevelStatistic.Collectibles++;
	}

	public void OnLevelFailed()
	{
		Pause();
		UI.ShowDialog("DialogGameFinish", "Failed");
	}

	public void OnLevelFinished()
	{
		Pause();
		UI.ShowDialog("DialogGameFinish", "Finished");
	}



}
