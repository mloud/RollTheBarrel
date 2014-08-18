using UnityEngine;
using System.Collections;

public class DialogLevelFinished : MonoBehaviour
{
	[SerializeField]
	TextMesh txt;

	public void SetText(string text)
	{
		txt.text = text;
	}
	

}
