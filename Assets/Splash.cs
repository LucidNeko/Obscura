using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {


	public GameObject water;
	public GameObject splash;

	private Transform thisPosition;

	void Start () {
		thisPosition = this.transform;

	}
	

	void Update () {
	
	}




	void OnTriggerEnter(Collider other) {
		Instantiate(splash,thisPosition.position,Quaternion.LookRotation (Vector3.forward));
	}
}
