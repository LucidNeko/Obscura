using UnityEngine;
using System.Collections;

public class NPCIdleScript : MonoBehaviour {

	private Animator m_Animator;

	private bool m_Set = false;

	// Use this for initialization
	void Start () {
		m_Animator = GetComponent<Animator> ();
		m_Animator.SetTrigger ("Idle 3");
	}
	
	// Update is called once per frame
	void Update () {
		RollNextAnim ();
	}

	void OnEnable() {
		m_Set = false;
		if (m_Animator) {
			m_Animator.SetTrigger ("Idle 1");
		}
	}

	void RollNextAnim() {
		float roll = Random.value;
		if (!m_Set && m_Animator.IsInTransition (m_Animator.GetLayerIndex ("Base Layer"))) {
			if (roll <= 0.25f) {
				m_Animator.SetTrigger ("Idle 1");
			} else if (roll <= 0.5f) {
				m_Animator.SetTrigger ("Idle 2");
			} else if (roll <= 0.75f) {
				m_Animator.SetTrigger ("Idle 3");
			} else if (roll <= 1f) {
				m_Animator.SetTrigger ("Idle 4");
			}
			m_Set = true;
		}
		
		if (!m_Animator.IsInTransition (m_Animator.GetLayerIndex ("Base Layer"))) {
			m_Set = false;
		}
	}

	void OnAnimatorMove() {
		Vector3 move = m_Animator.deltaPosition;
		move.y = 0f;
		transform.position = transform.position + move;
	}
}
