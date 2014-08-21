using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


namespace GEd
{

	public class GameEditor : EditorWindow
	{
		public enum ItemType
		{
			KillerPiston,
			Trigger,
			Jumper,
			Wheel,
			Bonus,
			Peanut
		}


		private class Config
		{
			public bool CenterToCreatedItem;
		}


		private delegate GameObject CreateDelegate();


		private class Item
		{
			public CreateDelegate Delegate { get; set; }
			public string Name { get; set; }
		}
		private static List<Item> ItemDb;


		private static void InitItemDb()
		{
			ItemDb = new List<Item>();

			// Killer Piston
			Item item = new Item();
			item.Name = ItemType.KillerPiston.ToString();
			item.Delegate = AddPiston;

			ItemDb.Add(item);

			// Trigger
			item = new Item();
			item.Name = ItemType.Trigger.ToString();
			item.Delegate = AddTrigger;
			
			ItemDb.Add(item);

			// Jumper
			item = new Item();
			item.Name = ItemType.Jumper.ToString();
			item.Delegate = AddJumper;
			
			ItemDb.Add(item);


			// Wheel
			item = new Item();
			item.Name = ItemType.Wheel.ToString();
			item.Delegate = AddWheel;
			
			ItemDb.Add(item);


			// Bonus
			item = new Item();
			item.Name = ItemType.Bonus.ToString();
			item.Delegate = AddBonus;
			
			ItemDb.Add(item);


			// Bonus
			item = new Item();
			item.Name = ItemType.Peanut.ToString();
			item.Delegate = AddPeanut;
			
			ItemDb.Add(item);
		}


		private static Config Cfg { get; set; }


		// Add menu named "My Window" to the Window menu
		[MenuItem ("Editor/ToolBox")]
		static void Init ()
		{
			// Get existing open window or if none, make a new one:
			GameEditor window = (GameEditor)EditorWindow.GetWindow (typeof (GameEditor));
			window.title = "GameEditor";

			if (Cfg == null)
			{
				Cfg = new Config();
			}
		
			if (ItemDb == null)
			{
				InitItemDb();
			}
		}


		[MenuItem("Editor/Add/Piston", false, 1000)]
		static GameObject AddPiston()
		{
			GameObject go = Instantiate(Resources.Load("Elements/Obstacles/KillerPiston")) as GameObject;
		
			PostProcess(go);

			return go;
		}

		[MenuItem("Editor/Add/Trigger", false, 1000)]
		static GameObject AddTrigger()
		{
			GameObject go = Instantiate(Resources.Load("Elements/Obstacles/Trigger")) as GameObject;

			PostProcess(go);

			return go;
		}

		[MenuItem("Editor/Add/Jumper", false, 1000)]
		static GameObject AddJumper()
		{
			GameObject go = Instantiate(Resources.Load("Elements/Obstacles/Jumper")) as GameObject;
		
			PostProcess(go);

			return go;
		}

		[MenuItem("Editor/Add/Wheel", false, 1000)]
		static GameObject AddWheel()
		{
			GameObject go = Instantiate(Resources.Load("Elements/Obstacles/Wheel")) as GameObject;

			PostProcess(go);

			return go;
		}

		[MenuItem("Editor/Add/Bonus", false, 1000)]
		static GameObject AddBonus()
		{
			GameObject go = Instantiate(Resources.Load("Elements/Collectibles/Bonus")) as GameObject;
			
			PostProcess(go);

			return go;
		}

		[MenuItem("Editor/Add/Peanut", false, 1000)]
		static GameObject AddPeanut()
		{
			GameObject go = Instantiate(Resources.Load("Elements/Collectibles/CollectiblePeanut")) as GameObject;
			
			PostProcess(go);

			return go;
		}


		void OnGUI ()
		{
			if (ItemDb != null)
			{
				for (int i = 0; i < ItemDb.Count; ++i)
				{
					if (GUILayout.Button(ItemDb[i].Name))
					{
						CreateItem(ItemDb[i]);
					}
				}
			
				GUILayout.Space(30);
				Cfg.CenterToCreatedItem = GUILayout.Toggle(Cfg.CenterToCreatedItem, " Center to new item");
			}

		}

		private static void CreateItem(Item item)
		{
			GameObject go = item.Delegate();

			if (Cfg.CenterToCreatedItem)
			{
				SceneView.lastActiveSceneView.AlignViewToObject(go.transform);
			}
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