using UnityEngine;
using System.Collections;


public class PopUpMessages : MonoBehaviour {

	public GameObject movementText;
	public Animator message;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

		if (Input.anyKeyDown) {
			message.SetBool("isHidden", true);
			//movementText.SetActive(false);
		}


	}
}
