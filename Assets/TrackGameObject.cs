using UnityEngine;
using System.Collections;

public class TrackGameObject : MonoBehaviour {

	public Transform m_Target;

	private Vector3 m_Offset;

	void Start() {
		m_Offset = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = m_Offset + m_Target.position;
	}
}
