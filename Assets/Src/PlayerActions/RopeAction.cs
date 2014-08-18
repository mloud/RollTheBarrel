using UnityEngine;
using System.Collections;


namespace Act
{
	public class RopeAction : Action 
	{
		private Transform RopeStartTr { get; set; }
		private Rigidbody RigidBody { get; set; }

		private Hook Hook { get; set; }

		enum State
		{
			Inactive,
			Shifting,
			Swinging
		}

		private State CurrentState { get; set; }

		public RopeAction(Transform ropeStartTr, Rigidbody rigidBody, Hook hook)
		{
			RopeStartTr = ropeStartTr;
			Hook = hook;
			RigidBody = rigidBody;

			ActionSubject = Hook.gameObject;
		}


		public override void Do()
		{
			IsActive = true;

			CurrentState = State.Shifting;

			Game.Instance.StartCoroutine(FireRopeCoroutine());
		}

		public override void Stop()
		{
			Hook.Joint.connectedBody = null;

			Hook.HideRope();

			IsActive = false;
		}


		private IEnumerator FireRopeCoroutine()
		{
			while ( (RigidBody.transform.position - Hook.transform.position).sqrMagnitude > Hook.Length * Hook.Length)
			{
				yield return 0;
			}

			Hook.Joint.connectedBody = RigidBody;

			Hook.InitRope(RopeStartTr);

			CurrentState = State.Swinging;

		}

		public override void Update()
		{
			if (CurrentState == State.Shifting)
			{
				RigidBody.velocity = (Hook.gameObject.transform.position - RigidBody.position).normalized * 30.0f;

			}
		
		}


	}
}
