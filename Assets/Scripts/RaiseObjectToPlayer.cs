using UnityEngine;
using System.Collections;

public class RaiseObjectToPlayer : MonoBehaviour {

	public Transform m_Target;
	public float m_YOffset = -10f;
	public float m_HowClose = 5f;
	public float m_Speed = 5f;

	private Vector3 m_UpPosition;
	private Vector3 m_DownPosition;

	// Use this for initialization
	void Start () {
		if (m_Target == null) {
			m_Target = GameObject.FindGameObjectWithTag("Player").transform;
		}

		m_UpPosition = transform.position;
		m_DownPosition = m_UpPosition + new Vector3 (0, m_YOffset, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (HowClose () < m_HowClose) {
			if(transform.position == m_DownPosition) {
				StartCoroutine(MoveTo (m_UpPosition, m_Speed));
			}
		} else {
			if(transform.position == m_UpPosition) {
				StartCoroutine(MoveTo (m_DownPosition, m_Speed));
			}
		}
	}

	/* Returns how close the object is to the target on the XZ plane */
	private float HowClose() {
		Vector3 objectPos = Vector3.Scale (transform.position, new Vector3 (1, 0, 1));
		Vector3 targetPos = Vector3.Scale (m_Target.position, new Vector3 (1, 0, 1));
		return Vector3.Distance(objectPos, targetPos);
	}

	IEnumerator MoveTo(Vector3 dest, float speed) {
		Vector3 start = transform.position;
		float t = 0;
		do {
			t += speed*Time.deltaTime;
			transform.position = Vector3.Lerp(start, dest, t);
			yield return null;
		} while(t <= 1);
	}
}
