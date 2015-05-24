using UnityEngine;
using System.Collections;

public class LiftPlatform : MonoBehaviour {

	public float height = 10f;
	public float speed = 2f;

	private bool canOperate = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Operate (other);
	}
	void OnTriggerStay(Collider other) {
		Operate (other);
	}


	private void Operate(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			if (canOperate) {
				StartCoroutine (Operate (other.gameObject));
			}
		}
	}

	IEnumerator Operate(GameObject target) {
		canOperate = false;

		Vector3 start = transform.position;
		Vector3 dest = start + Vector3.up * height;
		float t = 0;

		while ((t += Time.deltaTime * speed) < 1) {
			transform.position = Vector3.Lerp(start, dest, t);
			yield return null;
		}

		transform.position = dest;

		yield return new WaitForSeconds(5f);

		t = 0f;
		while ((t += Time.deltaTime * speed) < 1) {
			transform.position = Vector3.Lerp(dest, start, t);
			yield return null;
		}

		transform.position = start;

		canOperate = true;
	}
}
