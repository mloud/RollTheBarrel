using UnityEngine;
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

	[SerializeField]
	SphereCollider trunkSphereCollider;

	private List<Act.Action> Actions { get; set; }



#if UNITY_EDITOR
	ContactPoint? _contactPoint;
	Vector3 _speed;
#endif

	Vector3 _specialForce;
	private bool OnTerrain { get; set; }

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

#if UNITY_EDITOR
	public float GetTrunkSize()
	{
		//Vector3 center = polyMesh.keyPoints[0] + (polyMesh.keyPoints[1] - polyMesh.keyPoints[0]) * 0.5f;

		//return (center - polyMesh.keyPoints[2]).magnitude;
		return trunkSphereCollider.radius * 2;
	}
#endif



	public Vector2 Position()
	{
		return barrelRigidBody.transform.position;
	}


	public Vector2 Speed()
	{
		return barrelRigidBody.velocity;
	}

	public bool OnTheGround()
	{
		if (OnTerrain)
			return true;

		if (Actions.Count > 0 && Actions[Actions.Count - 1].IsActive && Actions[Actions.Count - 1].GetType() == typeof(Act.RopeAction))
			return true;

		return false;
	}

	public void OnCustomTriggerEnter(Collider collider)
	{
		if (collider.tag == "Bonus")
		{
			Game.Instance.OnBonusCollision(collider.gameObject.GetComponent<Bonus>());
		}
		else if (collider.tag == "Finish")
		{
			Game.Instance.OnLevelFinished();
		}
		else if (collider.tag == "Obstacle")
		{
			Game.Instance.OnLevelFailed();
		}
		else if (collider.tag == "Collectible")
		{
			CollectibleBase colectible = collider.gameObject.GetComponent<Collectible>();

			if (colectible.CanCollect())
			{
				colectible.Collect(mouthTr);
				Game.Instance.OnCollectibleCollision(colectible);
			}
		}
		else if (collider.tag == "Hook")
		{
			Act.Action action = new Act.RopeAction(ropeStartTr, barrelRigidBody, collider.gameObject.GetComponent<Hook>());

			AddAction(action);
		}
		else if (collider.tag == "Lift")
		{
			OnTerrain = true;
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
			OnTerrain = true;
			ResolveTerrainContact(collision);
		}
		else if (collision.collider.gameObject.tag == "Lift")
		{
			OnTerrain = true;
		}
		else if (collision.collider.gameObject.tag == "Obstacle")
		{
			Game.Instance.OnLevelFailed();
		}
	}

	public void OnCustomCollisionExit(Collision collision)
	{
		if (collision.collider.gameObject.tag == "Terrain")
		{
			OnTerrain = false;
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

		Actions.RemoveAll(x=> !x.IsActive && x.RemoveWhenFinished);
		
#if UNITY_EDITOR
		if (_contactPoint != null)
		{
			Debug.DrawRay(_contactPoint.Value.point, _contactPoint.Value.normal * 10, Color.white);
			Debug.DrawRay(_contactPoint.Value.point, new Vector3(_speed.x, _speed.y,0) * 1, Color.yellow);
		}

		Debug.DrawRay(barrelRigidBody.transform.position, _specialForce * 20.0f, Color.red);

#endif

	}

	void FixedUpdate()
	{
		// special force on the ground
		if (OnTheGround())
		{
			_specialForce = Camera.main.transform.right * Mathf.Sign (Game.Instance.CameraZ);
			float forceMag =  Mathf.Abs(Game.Instance.CameraZ) / Game.Instance.maxRotation;
			_specialForce *= forceMag * Game.Instance.specialForceMax;
			barrelRigidBody.AddForce(_specialForce, ForceMode.Force);
		}
		// wind force in the air
		else
		{
			Debug.Log (Mathf.Abs(barrelRigidBody.velocity.x).ToString());
			if (Mathf.Abs(barrelRigidBody.velocity.x) > 1.0f)
			{
				_specialForce = new Vector3(-Mathf.Sign(barrelRigidBody.velocity.x) * Game.Instance.specialAirForce, 0,0);
				barrelRigidBody.AddForce(_specialForce);
			}
		}
	}


	private void RemoveAction(GameObject actionSubject)
	{
		for (int i = 0; i < Actions.Count; ++i)
		{
			if (Actions[i].IsActive)
			{
				Actions[i].RemoveWhenFinished = true;
			}
		}

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
				Game.Instance.OnLevelFailed();
			}
	
		}
	}




}
