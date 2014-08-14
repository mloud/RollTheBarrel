using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour
{
	[SerializeField]
	TextMesh txtTime;

	
	[SerializeField]
	TextMesh txtBonus;

	void Start () 
	{
	
	}
	

	void Update ()
	{
		txtTime.text = Game.Instance.LevelStatistic.GetDurationFormated();
		txtBonus.text = Game.Instance.LevelStatistic.CollectedBonus.ToString();
	}
}
