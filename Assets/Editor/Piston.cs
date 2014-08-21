using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


namespace GEd
{
	[CustomEditor(typeof(Piston))]
	public class PistonEditor : Editor
	{

		private Vector2 scrollPosition;
		#region Inspector GUI

		public override void OnInspectorGUI()
		{
			if (target == null)
				return;

			Piston myTarget = (Piston)target;

			myTarget.CurrentMode = (Piston.Mode)EditorGUILayout.EnumPopup("Mode", myTarget.CurrentMode);
			myTarget.Trigger = (Piston.TriggerType)EditorGUILayout.EnumPopup("TriggerType", myTarget.Trigger);

			if (myTarget.Trigger == Piston.TriggerType.CustomTrigger)
			{
				myTarget.ActualTrigger = (Trigger)EditorGUILayout.ObjectField( myTarget.ActualTrigger, typeof(Trigger) );
			}
			
			
			myTarget.Direction = EditorGUILayout.Vector2Field("Direction", myTarget.Direction);
			myTarget.Distance = EditorGUILayout.FloatField("Distance", myTarget.Distance);

			if (myTarget.CurrentMode == Piston.Mode.Looping)
			{
				myTarget.StopDelay = EditorGUILayout.FloatField("StopDuration", myTarget.StopDelay);
			}

			myTarget.Speed = EditorGUILayout.FloatField("Speed", myTarget.Speed);
			myTarget.StartOffset = EditorGUILayout.FloatField("StartDelay", myTarget.Speed);

			if (GUI.changed)
			{
				EditorUtility.SetDirty(target);
			}
		}
		
		#endregion
		

		
		#region Scene GUI
		
		void OnSceneGUI()
		{
			Piston myTarget = (Piston)target;
			
			
			Vector3 dstPos = myTarget.transform.position +  new Vector3(myTarget.Direction.x, myTarget.Direction.y).normalized * myTarget.Distance;

			Handles.DrawLine(myTarget.transform.position, dstPos);
			Handles.SphereCap(0, dstPos, Quaternion.identity, 1.0f);
		}
		#endregion



		
		#region Menu Items

		[MenuItem("Editor/Add/trigger", false, 1000)]
		static void AddTrigger()
		{
		}



		#endregion
	}
}