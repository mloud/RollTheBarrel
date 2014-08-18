using UnityEngine;
using System.Collections;


namespace Act
{
	public abstract class Action 
	{
		public GameObject ActionSubject { get; protected set; }

		public bool IsActive { get; protected set; }

		public abstract void Do();
		public abstract void Stop();
		public abstract void Update();
	}
}
