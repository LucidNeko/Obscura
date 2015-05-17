using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

	public GameObject m_Fire;
	public GameObject m_Piece;

	// Use this for initialization
	void Start () {
		m_Fire.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject == m_Piece) {
			m_Fire.SetActive (true);
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.gameObject == m_Piece) {
			m_Fire.SetActive(false);
		}
	}
}
