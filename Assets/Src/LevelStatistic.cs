
using UnityEngine;
using System.Text;
using System;

public class LevelStatistic
{
	public float StartTime { get; set; }
	public float Bonus { get; set; }
	public float Collectibles { get; set; }

	public float Distance { get; set; }
	public float TotalDistance { get; set; }


	public float GetDuration()
	{
		return Time.time - StartTime;
	}
	
	public string GetDurationFormated()
	{
		StringBuilder strBuilder = new StringBuilder();
		
		TimeSpan span = TimeSpan.FromSeconds(GetDuration());
		
		int minutes = span.Minutes;
		int seconds = span.Seconds;
		
		if (minutes < 10)
			strBuilder.Append("0");
		strBuilder.Append(minutes.ToString());
		strBuilder.Append(":");
		if (seconds < 10)
			strBuilder.Append("0");
		strBuilder.Append(seconds.ToString());
		
		return strBuilder.ToString(); 
	}
}
