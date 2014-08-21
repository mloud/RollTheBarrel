using UnityEngine;
using System.Collections.Generic;

public class GatePiston : Piston
{

	void Awake()
	{
		CurrentMode = Mode.Switching;
		Trigger = TriggerType.CustomTrigger;
	}

	protected override void EnterStopState()
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
			// we are back at start - stay here
			if (CurrentState == State.StoppedA)
			{

			}
			else
			{
				Timer = Time.time + StopDelayB;
			}
		}
		// stay stopped
		else
		{
			Timer = -1;
			Disabled = true; // disable - no other triggering possible
		}
		
	}

	protected override void OnConnectedTriggerEnter(Trigger trigger, Collider collider)
	{

		if (CurrentState == State.StoppedA)
		{
			Timer = Time.time;
		}
	}
	
	protected override void OnConnectedTriggerExit(Trigger trigger, Collider collider)
	{
		if (Disabled)
			return;

	}
}
