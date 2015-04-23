using UnityEngine;
using System.Collections;

public class HintParticle : MonoBehaviour {


	private ParticleSystem ps;
	public GameObject player;
	public float playerDistThresh;

	void Start () {
		ps = GetComponent<ParticleSystem> ();
	}

	void FixedUpdate () {
		if (CheckDistanceToPlayer () <= playerDistThresh && SameHeightPlayer()) {
			renderer.enabled = true;
		}

		else{
			renderer.enabled = false;
		}
	}




	float CheckDistanceToPlayer(){
		return Vector3.Distance (player.transform.position, this.transform.position);
		
	}

	bool SameHeightPlayer(){
		return (this.transform.position.y - player.transform.position.y) < 0.1;
	}
}
