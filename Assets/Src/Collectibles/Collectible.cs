using UnityEngine;
using System.Collections;

public class Collectible : CollectibleBase 
{

	protected Transform CollectTo { get; set; }

	public override void Collect(Transform collectTo)
	{
		CurrentState = State.Collecting;
		CollectTo = collectTo;
	}



	protected override void Update ()
	{
		if (CurrentState == State.Collecting)
		{
			transform.position = Vector3.Lerp(transform.position, CollectTo.position, Time.deltaTime * 20.0f);

			if ( (transform.position - CollectTo.position).sqrMagnitude < 1.0f * 1.0f)
			{
				Destroy (gameObject);
			}
		}
	}
}
