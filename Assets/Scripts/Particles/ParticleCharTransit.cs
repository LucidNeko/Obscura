using UnityEngine;
using System.Collections;

public class ParticleCharTransit : MonoBehaviour {


	private ParticleSystem ps;

	void Start () {
		ps = this.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			ps.Play();
		}
	}
}
