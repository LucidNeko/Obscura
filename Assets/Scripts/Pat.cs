using UnityEngine;
using System.Collections;

public class Pat : MonoBehaviour {

	private NavMeshAgent nav;

	public Transform target1, target2, target3, target4, playerTarget;

	public int currentDest;

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		currentDest = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (CheckReachedDest (currentDest)) {
			currentDest++;
			if(currentDest==5) currentDest=1;
		}
		if(currentDest == 1) nav.SetDestination(target1.position);
		if (currentDest == 2) nav.SetDestination(target2.position);
		if (currentDest == 3) nav.SetDestination(target3.position);
		if (currentDest == 4) nav.SetDestination(target4.position);

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
