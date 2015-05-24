using UnityEngine;
using System.Collections;

public class GetShoved : MonoBehaviour {

	public float m_Force = 10f;

	Rigidbody m_body;

	// Use this for initialization
	void Start () {
		m_body = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag ("Player")) {
			Shove (collision.contacts [0].normal);
		}
	}

	void OnCollisionStay(Collision collision) {
		if (collision.gameObject.CompareTag ("Player")) {
			Shove (collision.contacts [0].normal);
		}
	}

	private void Shove(Vector3 direction) {
		m_body.AddForce(direction * m_Force, ForceMode.Impulse);
	}
}
