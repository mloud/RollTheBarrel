using UnityEngine;
using System.Collections;

public abstract class CollectibleBase : MonoBehaviour 
{

	public enum State
	{
		ForCollect = 0,
		Collecting

	}

	public State CurrentState { get; protected set; }

	public abstract void Collect(Transform collectTo);

	protected virtual void Update () {}

	public bool CanCollect()
	{
		return CurrentState == State.ForCollect;
	}

}
