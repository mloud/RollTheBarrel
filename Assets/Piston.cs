using UnityEngine;
using System.Collections.Generic;

public class Piston : MonoBehaviour
{

	public enum TriggerType
	{
		GameStart = 0,
		CustomTrigger
	}

	public Trigger TriggerObject;


	public enum Mode
	{
		Once,
		Switching
	}


	public TriggerType Trigger;

	public Mode CurrentMode;

	public float Distance;

	public Vector2 Direction;

	public float StopDelayA; // just when looping
	public float StopDelayB; // just when looping

	
	public float SpeedAB;
	public float SpeedBA;


	public float StartOffset;


	protected enum State
	{
		StoppedA,
		StoppedB,
		MovingAB,
		MovingBA
	}
	
	protected State CurrentState { get; set; }
	protected float Timer { get; set; }
	protected bool Disabled { get; set; }


	protected Vector3? DstPosition;

	public virtual void Start ()
	{
		// register to trigger
		if (TriggerObject)
		{
			TriggerObject.OnTriggerEnterDelegates += OnConnectedTriggerEnter;
			TriggerObject.OnTriggerExitDelegates += OnConnectedTriggerExit;
		}

		// in stop state 
		Timer = -1;
		CurrentState = State.StoppedA;

		// schedule start
		if (Trigger == TriggerType.GameStart)
		{
			Timer = Time.time + StartOffset;
		}
	}


	protected virtual void OnDestroy()
	{
		if (TriggerObject)
		{
			// unregister from trigger
			TriggerObject.OnTriggerEnterDelegates -= OnConnectedTriggerEnter;
			TriggerObject.OnTriggerExitDelegates -= OnConnectedTriggerExit;
		}
	}

	protected virtual void EnterLiftState()
	{
		Vector3 dstVector = new Vector3(Direction.x, Direction.y).normalized * Distance;

		if (CurrentState == State.StoppedA)
		{
			CurrentState = State.MovingAB;
			DstPosition = transform.position + dstVector;
		}
		else if (CurrentState == State.StoppedB)
		{
			CurrentState = State.MovingBA;
			DstPosition = transform.position - dstVector;
		}
	}


	protected virtual void EnterStopState()
	{
		transform.position = DstPosition.Value;

		if (CurrentState == State.MovingAB)
		{
			CurrentState =  State.StoppedB;
		}
		else if (CurrentState == State.MovingBA)
		{
			CurrentState = State.StoppedA;
		}

		// schedule next lift
		if (CurrentMode == Mode.Switching)
		{
			float stopDelay = CurrentState == State.StoppedA ? StopDelayA : StopDelayB;

			Timer = Time.time + stopDelay;
		}
		// stay stopped
		else
		{
			Timer = -1;
			Disabled = true; // disable - no other triggering possible
		}

	}

	protected virtual void Update () 
	{

		switch (CurrentState)
		{
		case State.StoppedA:
			UpdateStopState();
			break;
	
		case State.StoppedB:
			UpdateStopState();
			break;

		case State.MovingAB:
			UpdateLiftState();
			break;
	
		case State.MovingBA:
			UpdateLiftState();
		break;

		}
	}
	
	protected virtual void UpdateLiftState()
	{
		float speed = CurrentState == State.MovingAB ? SpeedAB : SpeedBA;
		transform.position += Time.deltaTime * speed * (DstPosition.Value - transform.position).normalized;
			
		if ( (transform.position - DstPosition.Value).sqrMagnitude < 0.1f * 0.1f)
		{
			EnterStopState();
		}
	}
	
	protected virtual void UpdateStopState()
	{
		if (Timer != -1 && Time.time > Timer)
		{
			// Start lifting
			EnterLiftState();
		}
	}
	

	protected virtual void OnConnectedTriggerEnter(Trigger trigger, Collider collider)
	{
		if (Disabled)
			return;

		if (CurrentState == State.StoppedA || CurrentState == State.StoppedB)
		{
			Timer = Time.time;

			if (CurrentMode == Mode.Once)
			{
				Disabled = true;
			}
		}
	}
	
	protected virtual void OnConnectedTriggerExit(Trigger trigger, Collider collider)
	{
		if (Disabled)
			return;

	}
}
