using UnityEngine;
using System.Collections.Generic;

public class Piston : MonoBehaviour
{

	public enum TriggerType
	{
		GameStart = 0,
		CustomTrigger
	}

	[SerializeField]
	List<Trigger> Triggers;


	public enum Mode
	{
		Once,
		Looping
	}

	[SerializeField]
	TriggerType trigger;

	[SerializeField]
	Mode mode;


	[SerializeField]
	float distance;

	[SerializeField]
	Vector2 direction;


	[SerializeField]
	float stopDelay; // just when looping
	
	[SerializeField]
	float speed;

	[SerializeField]
	float startOffset;


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
		for (int i = 0; i < Triggers.Count; ++i)
		{
			Triggers[i].OnTriggerEnterDelegates += OnConnectedTriggerEnter;
			Triggers[i].OnTriggerExitDelegates += OnConnectedTriggerExit;
		}

		// in stop state 
		Timer = -1;
		CurrentState = State.Stopped;

		// schedule start
		if (trigger == TriggerType.GameStart)
		{
			Timer = Time.time + startOffset;
		}
	}


	void OnDestroy()
	{
		// unregister from trigger
		for (int i = 0; i < Triggers.Count; ++i)
		{
			Triggers[i].OnTriggerEnterDelegates -= OnConnectedTriggerEnter;
			Triggers[i].OnTriggerExitDelegates -= OnConnectedTriggerExit;
		}

	}

	void EnterLiftState()
	{
		DstPosition = transform.position + new Vector3(direction.x, direction.y).normalized * distance;

		CurrentState = State.Moving;
	}


	void EnterStopState()
	{
		transform.position = DstPosition.Value;

		CurrentState =  State.Stopped;
	

		// schedule next lift
		if (mode == Mode.Looping)
		{
			Timer = Time.time + stopDelay;
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
		transform.position += Time.deltaTime * speed * (DstPosition.Value - transform.position).normalized;
			
		if ( (transform.position - DstPosition.Value).sqrMagnitude < 0.1f * 0.1f)
		{
				EnterStopState();
				direction *= -1;
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


	#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Vector3 dstPos = transform.position +  new Vector3(direction.x, direction.y).normalized * distance;
		Gizmos.DrawLine(transform.position, dstPos);
		Gizmos.DrawSphere(dstPos, 0.5f);
	}
	#endif
	
}
