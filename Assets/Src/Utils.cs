
using System.Collections;
using UnityEngine;

public static class Utils
{
	public static Vector2 ComputeCircleTangent(Vector2 circleCenter, float circleRadius, Vector2 fromPoint)
	{

		Vector2 circleC = circleCenter - fromPoint;

		float tmp1 = circleC.x * circleC.x + circleC.y * circleC.y;
		float tmp2 = circleC.y * Mathf.Sqrt(tmp1 - circleRadius * circleRadius);
	
		float t1 = Mathf.Acos( (-circleRadius * circleC.x + tmp2 ) / tmp1);
		float t2 = Mathf.Acos( (-circleRadius * circleC.x - tmp2 ) / tmp1);

		float t3 = -t1;
		float t4 = -t2;


		Vector2 res1 = new Vector2(circleC.x + circleRadius * Mathf.Cos(t1),
		                           circleC.y + circleRadius * Mathf.Sin(t1));
		
		Vector2 res2 = new Vector2(circleC.x + circleRadius * Mathf.Cos(t2),
		                           circleC.y + circleRadius * Mathf.Sin(t2));
		

		return res1.y < res2.y ? res1 + fromPoint : res2 + fromPoint;
	}




}
