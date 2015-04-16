using UnityEngine;
using System.Collections;

[RequireComponent(typeof (PlayerController))]
public class PlayerMovement : MonoBehaviour {

	private PlayerController mController;
	private Transform mCam;
	private Vector3 mMove;
	private bool mJump;


	// Use this for initialization
	void Start() {
		mCam = Camera.main.transform;
		mController = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update() {
		if(!mJump) {
			mJump = Input.GetButtonDown("Jump");
		}
	}

	void FixedUpdate() {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Vector3 forward = Vector3.Scale(mCam.forward, new Vector3(1, 0, 1)).normalized;
		mMove = v * forward + h * mCam.right;



		mController.Move(mMove, mJump);
		mJump = false;
	}

	void LateUpdate() {

	}

}
