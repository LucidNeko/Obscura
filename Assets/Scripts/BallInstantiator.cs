using UnityEngine;
using System.Collections;

public class BallInstantiator : MonoBehaviour {

	public GameObject m_Object;
	public float m_NumBalls = 100;

	// Use this for initialization
	void Start () {
		StartCoroutine (Spawn ());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator Spawn() {
		for(int i = 0; i < m_NumBalls; i++) {
			Instantiate(m_Object, transform.position, transform.rotation);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
