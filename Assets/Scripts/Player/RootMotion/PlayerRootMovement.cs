using UnityEngine;
using System.Collections;

[RequireComponent(typeof (PlayerRootController))]
public class PlayerRootMovement : MonoBehaviour {

	private static Vector3 CULL_Y = new Vector3(1, 0, 1);

	private PlayerRootController _controller;
	private Transform _camera;

	// Use this for initialization
	void Start() {
		_camera = Camera.main.transform;
		_controller = GetComponent<PlayerRootController>();
	}

	void FixedUpdate() {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		bool jump = Input.GetButton("Jump");

		Vector3 forward = Vector3.Scale(_camera.forward, CULL_Y).normalized;
		Vector3 move = v * forward + h * _camera.right;
		move.Normalize ();

		_controller.Move(move, jump);
	}

	//This code might jump better..
	//	private Vector3 mMove;
	//	private bool mJump;
	//	
	//	// Update is called once per frame
	//	void Update() {
	//		if(!mJump) {
	//			mJump = Input.GetButtonDown("Jump");
	//		}
	//	}
	//
	//	void FixedUpdate() {
	//		float h = Input.GetAxis("Horizontal");
	//		float v = Input.GetAxis("Vertical");
	//
	//		Vector3 forward = Vector3.Scale(mCam.forward, new Vector3(1, 0, 1)).normalized;
	//		mMove = v * forward + h * mCam.right;
	//
	//		mController.Move(mMove, mJump);
	//		mJump = false;
	//	}

}
