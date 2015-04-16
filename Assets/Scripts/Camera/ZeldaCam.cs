using UnityEngine;
using System.Collections;

public class ZeldaCam : MonoBehaviour {

	public Transform target;
	public Vector3 cameraOffset;

	// Use this for initialization
	void Awake () {
		if (target == null) {
			target = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TargetPoint>().gameObject.transform;
		}



	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = target.position + cameraOffset;
		transform.LookAt (target);
	}

}
