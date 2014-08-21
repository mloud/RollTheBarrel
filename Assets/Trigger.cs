using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour 
{

	public delegate void OnTriggerEnterDelegate(Trigger trigger, Collider collider);
	public delegate void OnTriggerExitDelegate(Trigger trigger, Collider collider);

	public OnTriggerEnterDelegate OnTriggerEnterDelegates;
	public OnTriggerExitDelegate OnTriggerExitDelegates;


	void OnTriggerEnter(Collider collider)
	{
		OnTriggerEnterDelegates(this, collider);
	}
	
	void OnTriggerExit(Collider collider)
	{
		OnTriggerExitDelegates(this, collider);
	}

}
