using UnityEngine;
using System.Collections;

public class Push : MonoBehaviour {

	GameObject m_Player;
	Animator m_Animator;

	// Use this for initialization
	void Start () {
		m_Player = GameObject.FindGameObjectWithTag ("Player");
		m_Animator = m_Player.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	void OnCollisionEnter(Collision collision) {
//		if(collision.gameObject == m_Player) {
//			m_Animator.SetBool("Push", true);
//		}
//	}
//
//	void OnCollisionStay(Collision collision) {
//		if(collision.gameObject == m_Player) {
//			
//		}
//	}
//
//	void OnCollisionExit(Collision collision) {
//		if(collision.gameObject == m_Player) {
//			m_Animator.SetBool("Push", false);
//		}
//	}

	void OnTriggerEnter(Collider other) {
//		if(other.gameObject == m_Player) {
//			m_Animator.SetBool("Push", true);
//		}
//		GetHit (other);
	}

	void OnTriggerExit(Collider other) {
//		if(other.gameObject == m_Player) {
//			m_Animator.SetBool("Push", false);
//		}
	}

	private void GetHit(Collider other) {
		if (m_Animator.GetCurrentAnimatorStateInfo (m_Animator.GetLayerIndex ("Hit Layer")).IsName ("mc_attack")) {
			Vector3 direction = other.transform.position - transform.position;
			direction = Vector3.Scale(direction, new Vector3(1, 0, 1));
			
			Debug.Log (direction);
			if(direction.x >= direction.z) {
				direction.z = 0f;
			} else {
				direction.x = 0f;
			}

			direction.Normalize();

//			Debug.Log (direction);

			transform.position = transform.position + direction;
		}
	}

}
