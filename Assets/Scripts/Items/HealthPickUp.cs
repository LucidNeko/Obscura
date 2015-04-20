using UnityEngine;
using System.Collections;

public class HealthPickUp : MonoBehaviour {


	public GameObject player;
	public float playerDistThresh;
	private bool moveTowardPlayer;
	

	void Start () {
		Rigidbody orb = GetComponent<Rigidbody> ();
		moveTowardPlayer = false;
	}
	

	void Update () {
		Vector3 rotation = new Vector3 (15, 30, 45)* Time.deltaTime;
		transform.Rotate (rotation);


		if (CheckDistanceToPlayer () < playerDistThresh || moveTowardPlayer) {
			moveTowardPlayer = true;
			float xMove = transform.position.x - player.transform.position.x;
			float zMove = transform.position.z - player.transform.position.z;
			float yMove = transform.position.y - player.transform.position.y;


			transform.position -= new Vector3 (xMove, yMove, zMove)*Time.deltaTime *3;




		}
	}


	float CheckDistanceToPlayer(){
		return Vector3.Distance (player.transform.position, this.transform.position);

	}

}
