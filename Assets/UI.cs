using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{
	[SerializeField]
	GameObject dialogGameFinishPrefab;

	public void ShowDialog(string name)
	{
		if (name == "DialogGameFinish")
		{
			GameObject go = Instantiate(dialogGameFinishPrefab) as GameObject;
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.zero;
		}
	}



}
