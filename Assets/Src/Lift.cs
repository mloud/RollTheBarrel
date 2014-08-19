using UnityEngine;
using System.Collections;

public class Lift : MonoBehaviour
{

	[SerializeField]
	bool Automatic;

	[SerializeField]
	float StopDelay;

	[SerializeField]
	float Speed;

	[SerializeField]
	float Distance;

	[SerializeField]
	Vector2 Direction;



	private enum State
	{
		Stopped,
		Lifting
	}

	State CurrentState { get; set; }
	float Timer { get; set; }

	private Vector3? DstPosition;

	void Start ()
	{
		CurrentState = State.Stopped;
	}
	
	void FixedUpdate () 
	{
		switch (CurrentState)
		{
		case State.Stopped:
			UpdateStopState();
			break;

		case State.Lifting:
			UpdateLiftState();
			break;
		}
	}

	private void UpdateLiftState()
	{
		transform.position = Vector3.Lerp(transform.position, DstPosition.Value, Time.deltaTime * Speed);

		//rigidbody.AddForce(Direction.normalized * Speed, ForceMode.Force);
		//rigidbody.velocity = Direction.normalized * Speed;

		if ( (transform.position - DstPosition.Value).sqrMagnitude < 0.1f * 0.1f)
		{
			//rigidbody.velocity = Vector3.zero;
			transform.position = DstPosition.Value;
			Timer = Time.time + StopDelay;
			Direction *= -1;

			CurrentState = State.Stopped;
		}
	}

	private void UpdateStopState()
	{
		if (Time.time > Timer)
		{
			// Start lifting
			//rigidbody.velocity = Vector3.zero;
			DstPosition = transform.position + new Vector3(Direction.x, Direction.y).normalized * Distance;
			CurrentState = State.Lifting;
		}
	}


#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Vector3 dstPos = transform.position +  new Vector3(Direction.x, Direction.y).normalized * Distance;
		Gizmos.DrawLine(transform.position, dstPos);
		Gizmos.DrawSphere(dstPos, 0.5f);
	}
#endif

}
