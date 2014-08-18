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

	[SerializeField]
	public GameObject ropePrefab;


	private Transform Rope { get; set;} 


	private Transform FromTr { get; set; }
	private Transform ToTr { get; set; }


	void Awake()
	{
		Rope = (Instantiate(ropePrefab) as GameObject).transform;
		Rope.gameObject.SetActive(false);
	}
	public void InitRope(Transform fromTr)
	{
		LineRenderer.SetVertexCount(2);

		FromTr = fromTr;
		ToTr = transform;

		Rope.gameObject.SetActive(true);

		UpdateRope();
	}

	public void HideRope()
	{
		LineRenderer.SetVertexCount(0);
		FromTr = null;
		Rope.gameObject.SetActive(false);
	}

	private void UpdateRope()
	{
		if (FromTr != null)
		{
			Vector3 ropeScale = Rope.localScale;
			ropeScale.y = (FromTr.position - ToTr.position).magnitude;
			Rope.localScale =  ropeScale;

			Vector3 ropePos = FromTr.position + (ToTr.position - FromTr.position) * 0.5f;
			ropePos.z = transform.position.z + 0.01f;
			Rope.transform.position = ropePos;

			Rope.eulerAngles += Quaternion.FromToRotation(Rope.up, ToTr.position -  FromTr.position).eulerAngles;
		}
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

		UpdateRope();
	}

#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(gameObject.transform.position, Length);
	}
#endif

}
