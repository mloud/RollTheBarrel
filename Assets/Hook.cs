using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour
{
	[SerializeField]
	public Joint Joint;

	[SerializeField]
	public float Length;

	[SerializeField]
	public LineRenderer LineRenderer;

	private Transform FromTr { get; set; }
	private Transform ToTr { get; set; }


	public void InitRope(Transform fromTr)
	{
		LineRenderer.SetVertexCount(2);

		FromTr = fromTr;
		ToTr = transform;
	}

	public void HideRope()
	{
		LineRenderer.SetVertexCount(0);
		FromTr = null;
	}


	
	void Update ()
	{
		if (FromTr != null)
		{
			Vector3 pos1 = FromTr.position;
			Vector3 pos2 = ToTr.position;

			pos1.z = transform.position.z;
			pos2.z = transform.position.z;

			LineRenderer.SetPosition(0, pos1);
			LineRenderer.SetPosition(1, pos2);
		}
	}

#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(gameObject.transform.position, Length);
	}
#endif

}
