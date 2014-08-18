using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour
{
	[SerializeField]
	TextMesh txtTime;
	
	[SerializeField]
	TextMesh txtBonus;

	[SerializeField]
	TextMesh txtCollectible;



	void Start () 
	{
	
	}
	

	void Update ()
	{
		txtTime.text = Game.Instance.LevelStatistic.GetDurationFormated();
		txtBonus.text = Game.Instance.LevelStatistic.Bonus.ToString();
		txtCollectible.text = Game.Instance.LevelStatistic.Collectibles.ToString();
	}
}
