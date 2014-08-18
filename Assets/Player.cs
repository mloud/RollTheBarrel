﻿using UnityEngine;
using System.Collections.Generic;


public class Player : MonoBehaviour 
{
	[SerializeField]
	Rigidbody barrelRigidBody;

	[SerializeField]
	Elephant elephant;

	[SerializeField]
	Transform mouthTr;

	[SerializeField]
	Transform ropeStartTr;


	private List<Act.Action> Actions { get; set; }

#if UNITY_EDITOR
	ContactPoint? _contactPoint;
	Vector3 _speed;
#endif

	void Awake()
	{
		Actions = new List<Act.Action>();
	}

	public void DoAction()
	{
		for (int i = Actions.Count - 1; i >= 0; --i)
		{
			if (Actions[i].IsActive)
			{
				Actions[i].Stop();
				break;
			}
			else
			{
				Actions[i].Do();
				break;
			}
		}
	}

	public Vector2 Position()
	{
		return barrelRigidBody.transform.position;
	}


	public Vector2 Speed()
	{
		return barrelRigidBody.velocity;
	}


	public void OnCustomTriggerEnter(Collider collider)
	{
		if (collider.tag == "Bonus")
		{
			Game.Instance.OnBonusCollision(collider.gameObject.GetComponent<Bonus>());
		}
		else if (collider.tag == "Finish")
		{
			Game.Instance.OnGameFinished();
		}
		else if (collider.tag == "Obstacle")
		{
			Game.Instance.OnGameFinished();
		}
		else if (collider.tag == "Collectible")
		{
			CollectibleBase colectible = collider.gameObject.GetComponent<Collectible>();

			if (colectible.CanCollect())
			{
				colectible.Collect(mouthTr);
			}
		}
		else if (collider.tag == "Hook")
		{
			Act.Action action = new Act.RopeAction(ropeStartTr, barrelRigidBody, collider.gameObject.GetComponent<Hook>());

			AddAction(action);
		}
		else if (collider.tag == "Terrain")
		{

		}

	}

	public void OnCustomTriggerExit(Collider collider)
	{
		if (collider.tag == "Hook")
		{
			RemoveAction(collider.gameObject);
		}
	}

	public void OnCustomCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.tag == "Terrain")
		{
			ResolveTerrainContact(collision);
		}
	}


	void Start ()
	{
	
	}
	
	void Update ()
	{

		for (int i = 0; i < Actions.Count; ++i)
		{
			Actions[i].Update();
		}


		 if (_contactPoint != null)
		{
			Debug.DrawRay(_contactPoint.Value.point, _contactPoint.Value.normal * 10, Color.white);
			Debug.DrawRay(_contactPoint.Value.point, new Vector3(_speed.x, _speed.y,0) * 1, Color.yellow);

		}

	}


	void RemoveAction(GameObject actionSubject)
	{
		Actions.RemoveAll(x=>x.ActionSubject == actionSubject && !x.IsActive);
		Debug.Log("Player.RemoveAction() " + Actions.Count.ToString());

	}

	void AddAction(Act.Action action)
	{
		int index = Actions.FindIndex(x=>x.ActionSubject == action.ActionSubject);

		if (index == -1)
		{
			Actions.Add (action);
			Debug.Log("Player.AddAction() " + action.ToString() + " " + Actions.Count.ToString());
		}
	}


	void ResolveTerrainContact(Collision collision)
	{

		for (int i = 0; i < collision.contacts.Length; ++i) 
		{
			ContactPoint cp = collision.contacts[i];

		
#if UNITY_EDITOR
			_contactPoint = cp;
			_speed = new Vector2(collision.relativeVelocity.x, collision.relativeVelocity.y);
#endif

			float dot = Vector2.Dot(collision.relativeVelocity.normalized, cp.normal.normalized);
			float speedMag =  collision.relativeVelocity.magnitude;
			float hitParam = dot * speedMag; 

			Debug.Log("Terrain hit: dot = " + dot.ToString() + " speed = " + speedMag.ToString() + " hit param = " + hitParam.ToString());


			if (hitParam > Game.Instance.HitParamGameOver)
			{
				Game.Instance.OnGameFinished();
			}
	
		}
		
	}


}
