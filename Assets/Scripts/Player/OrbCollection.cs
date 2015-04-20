using UnityEngine;
using System.Collections;

public class OrbCollection : MonoBehaviour {

	PlayerInfoGUI gui;

	// Use this for initialization
	void Start () {
		gui = GetComponent<PlayerInfoGUI> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
		if (other.tag == "Orb") {
			other.gameObject.SetActive(false);
			gui.AddHealth(20);


		}
	}
}
