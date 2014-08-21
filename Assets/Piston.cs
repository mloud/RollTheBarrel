using UnityEngine;
using System.Collections.Generic;

public class Piston : MonoBehaviour
{

	public enum TriggerType
	{
		GameStart = 0,
		CustomTrigger
	}

	public Trigger ActualTrigger;

	public enum Mode
	{
		Once,
		Looping
	}


	public TriggerType Trigger;

	public Mode CurrentMode;

	public float Distance;

	public Vector2 Direction;

	public float StopDelay; // just when looping
	
	public float Speed;

	public float StartOffset;


	private enum State
	{
		Stopped,
		Moving
	}
	
	State CurrentState { get; set; }
	float Timer { get; set; }
	bool Disabled { get; set; }


	private Vector3? DstPosition;

	public void Start ()
	{
		// register to trigger
		if (ActualTrigger)
		{
			ActualTrigger.OnTriggerEnterDelegates += OnConnectedTriggerEnter;
			ActualTrigger.OnTriggerExitDelegates += OnConnectedTriggerExit;
		}

		// in stop state 
		Timer = -1;
		CurrentState = State.Stopped;

		// schedule start
		if (Trigger == TriggerType.GameStart)
		{
			Timer = Time.time + StartOffset;
		}
	}


	void OnDestroy()
	{
		if (ActualTrigger)
		{
			// unregister from trigger
			ActualTrigger.OnTriggerEnterDelegates -= OnConnectedTriggerEnter;
			ActualTrigger.OnTriggerExitDelegates -= OnConnectedTriggerExit;
		}
	}

	void EnterLiftState()
	{
		DstPosition = transform.position + new Vector3(Direction.x, Direction.y).normalized * Distance;

		CurrentState = State.Moving;
	}


	void EnterStopState()
	{
		transform.position = DstPosition.Value;

		CurrentState =  State.Stopped;
	

		// schedule next lift
		if (CurrentMode == Mode.Looping)
		{
			Timer = Time.time + StopDelay;
		}
		// stay stopped
		else
		{
			Timer = -1;
			Disabled = true; // disable - no other triggering possible
		}

	}

	void Update () 
	{
		switch (CurrentState)
		{
		case State.Stopped:
			UpdateStopState();
			break;
			
		case State.Moving:
			UpdateLiftState();
			break;
		}
	}
	
	private void UpdateLiftState()
	{
		transform.position += Time.deltaTime * Speed * (DstPosition.Value - transform.position).normalized;
			
		if ( (transform.position - DstPosition.Value).sqrMagnitude < 0.1f * 0.1f)
		{
				EnterStopState();
				Direction *= -1;
		}
	}
	
	private void UpdateStopState()
	{
		if (Timer != -1 && Time.time > Timer)
		{
			// Start lifting
			EnterLiftState();
		}
	}
	

	void OnConnectedTriggerEnter(Trigger trigger, Collider collider)
	{
		if (Disabled)
			return;

		if (CurrentState == State.Stopped)
		{
			Timer = Time.time;
			Disabled = true;
		}
	}
	
	void OnConnectedTriggerExit(Trigger trigger, Collider collider)
	{
		if (Disabled)
			return;

	}
}
