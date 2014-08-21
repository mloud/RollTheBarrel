using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


namespace GEd
{

	public class EditorMenu : EditorWindow
	{

		[MenuItem("Editor/Add/Piston", false, 1000)]
		static void AddPiston()
		{
			GameObject go = Instantiate(Resources.Load("Elements/Obstacles/KillerPiston")) as GameObject;
		
			PostProcess(go);
		}

		[MenuItem("Editor/Add/Trigger", false, 1000)]
		static void AddTrigger()
		{
			GameObject go = Instantiate(Resources.Load("Elements/Obstacles/Trigger")) as GameObject;

			PostProcess(go);
		}

		[MenuItem("Editor/Add/Jumper", false, 1000)]
		static void AddJumper()
		{
			GameObject go = Instantiate(Resources.Load("Elements/Obstacles/Jumper")) as GameObject;
		
			PostProcess(go);
		}

		[MenuItem("Editor/Add/Wheel", false, 1000)]
		static void AddWheel()
		{
			GameObject go = Instantiate(Resources.Load("Elements/Obstacles/Wheel")) as GameObject;

			PostProcess(go);
		}

		[MenuItem("Editor/Add/Bonus", false, 1000)]
		static void AddBonus()
		{
			GameObject go = Instantiate(Resources.Load("Elements/Collectibles/Bonus")) as GameObject;
			
			PostProcess(go);
		}

		[MenuItem("Editor/Add/Peanut", false, 1000)]
		static void AddPeanut()
		{
			GameObject go = Instantiate(Resources.Load("Elements/Collectibles/CollectiblePeanut")) as GameObject;
			
			PostProcess(go);
		}


		private static void PostProcess(GameObject go)
		{
			PolyMesh polymesh = go.GetComponent<PolyMesh>();

			if (polymesh != null)
			{
				polymesh.BuildMesh();
			}
		}

		
	}

}