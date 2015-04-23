using UnityEngine;
using System.Collections;

public class Pat : MonoBehaviour {

	private NavMeshAgent nav;

	public Transform target1, target2, target3, target4;

	private int currentDest;

	private bool chasingPlayer;

	public GameObject player;

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		currentDest = 1;
		chasingPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(!chasingPlayer){																			//If not chasing player, follow patrol routine		.							
			if (CheckReachedDest (currentDest)) {													//If current destination reached.
				currentDest++;																								//Will always be speed 2 unless just killed player, will reset speed to 2, else will have no effect.
				if(currentDest==5) currentDest=1;
																						//Update the destiination to the next Destination in the patrol cycle.
			}
			if(currentDest == 1){
				nav.SetDestination(target1.position);
			}
			if (currentDest == 2){
				nav.SetDestination(target2.position);

			}
			if (currentDest == 3){
				nav.SetDestination(target3.position);

			}
			if (currentDest == 4){
				nav.SetDestination(target4.position);								//Set next position for patrol cycle, also keep moving toward that position.

			}


			chasingPlayer = CheckPlayerVisibleOrClose();													//Is the player current in this patrol characters field of view? update chasingPlayer boolean, will do nothing this iteration of Update();.
		}
		else{																						//Change from patrol cycle, to moving toward player.
			if(player.GetComponentInParent<PlayerAttributes>().GetHealth()<=0){								//Player has died, reset navigation.
				nav.SetDestination(target1.position);
				currentDest = 1;
				chasingPlayer = false;	
				print (nav.destination);
			}
			else{																					//Player is still alive, keep chasing.
				nav.SetDestination(player.transform.position);
			}


		}
	}

	bool CheckPlayerVisibleOrClose(){

		Vector3 rayDirection = player.transform.position - transform.position;
		RaycastHit hit = new RaycastHit ();
		if(rayDirection.magnitude<=3.0f) print ("player close");


		if (Physics.Raycast (transform.position, rayDirection, out hit)) {
			if(Mathf.Abs(Vector3.Distance(hit.transform.position,player.transform.position))<1){
				if(hit.distance<7){
					Debug.DrawRay(transform.position,rayDirection);	
					return true;
				}

			}

		}
		return false;
	
	}
		
	 

	bool CheckReachedDest(int dest){
		if(dest==1) return (Vector3.Distance(transform.position,target1.position)<1.0f);
		if(dest==2) return (Vector3.Distance(transform.position,target2.position)<1.0f);
		if(dest==3) return (Vector3.Distance(transform.position,target3.position)<1.0f);
		if(dest==4) return (Vector3.Distance(transform.position,target4.position)<1.0f);
		return false;
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Player"){
			collision.gameObject.GetComponent<PlayerAttributes>().RemoveHealth(30);
		}
		
	}
}
