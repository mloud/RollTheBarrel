using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour
{
	[SerializeField]
	GameObject dialogGameFinishPrefab;

	public void ShowDialog(string name, string text)
	{
		if (name == "DialogGameFinish")
		{
			GameObject go = Instantiate(dialogGameFinishPrefab) as GameObject;
			DialogLevelFinished dlgScript = go.GetComponent<DialogLevelFinished>();
			dlgScript.SetText(text);
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.zero;
		}
	}



}
