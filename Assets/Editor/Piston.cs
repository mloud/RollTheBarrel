using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


namespace GEd
{
	[CustomEditor(typeof(Piston))]
	public class PistonEditor : Editor
	{

		protected virtual void InitShared()
		{
			Piston myTarget = (Piston)target;
			
			
			myTarget.CurrentMode = (Piston.Mode)EditorGUILayout.EnumPopup("Mode", myTarget.CurrentMode);
			myTarget.Trigger = (Piston.TriggerType)EditorGUILayout.EnumPopup("TriggerType", myTarget.Trigger);
			
			
			if (myTarget.Trigger == Piston.TriggerType.CustomTrigger)
			{
				myTarget.TriggerObject = (Trigger)EditorGUILayout.ObjectField( myTarget.TriggerObject, typeof(Trigger) );
			}

			if (myTarget.CurrentMode == Piston.Mode.Switching)
			{
				myTarget.StopDelayA = EditorGUILayout.FloatField("StopDurationA", myTarget.StopDelayA);
				myTarget.StopDelayB = EditorGUILayout.FloatField("StopDurationB", myTarget.StopDelayB);
			}

		}


		#region Inspector GUI
		public override void OnInspectorGUI()
		{
			if (target == null)
				return;

			Piston myTarget = (Piston)target;

			InitShared();

			myTarget.Direction = EditorGUILayout.Vector2Field("Direction", myTarget.Direction);
			myTarget.Distance = EditorGUILayout.FloatField("Distance", myTarget.Distance);



			myTarget.SpeedAB = EditorGUILayout.FloatField("SpeedAB", myTarget.SpeedAB);
			myTarget.SpeedBA = EditorGUILayout.FloatField("SpeedBA", myTarget.SpeedBA);

			myTarget.StartOffset = EditorGUILayout.FloatField("StartDelay", myTarget.StartOffset);

			if (GUI.changed)
			{
				EditorUtility.SetDirty(target);
			}
		}
		
		#endregion
		

		
		#region Scene GUI
		
		protected void OnSceneGUI()
		{
			Piston myTarget = (Piston)target;
			
			
			Vector3 dstPos = myTarget.transform.position +  new Vector3(myTarget.Direction.x, myTarget.Direction.y).normalized * myTarget.Distance;

			Handles.DrawLine(myTarget.transform.position, dstPos);
			Handles.SphereCap(0, dstPos, Quaternion.identity, 1.0f);
		}
		#endregion
	}

	[CustomEditor(typeof(GatePiston))]
	public class GatePistonEditor : PistonEditor
	{
		protected override void InitShared()
		{
			GatePiston gatePiston = (GatePiston)target;
					
			gatePiston.TriggerObject = (Trigger)EditorGUILayout.ObjectField( gatePiston.TriggerObject, typeof(Trigger) );
		
			gatePiston.StopDelayB = EditorGUILayout.FloatField("StopDurationB", gatePiston.StopDelayB);
		}
		

	
	}
		

}