using UnityEngine;
using System.Collections;

public class OldTrackingCameraRig : MonoBehaviour {

	public float trackingSpeed = 0.2f;
	[Range(1, 5)] public float turnSmoothing = 5f;
	public Transform target;



	private Transform mCam;
	private Transform mPivot;
	private Vector2 mRotation = Vector2.zero;
	private bool mMoving;

	private Vector3 velocity = Vector3.zero;



	// Use this for initialization
	void Awake() {
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
		mCam = GetComponentInChildren<Camera>().transform;
		mPivot = mCam.parent;
		mRotation.x = mPivot.eulerAngles.x;
		mRotation.y = mPivot.eulerAngles.y;

//		ZoomBy (5);
	}

	void Update() {
		Vector2 camSpeed = new Vector2(90, 45);
		float camResetSpeed = 2f;

		Vector2 rotBefore = new Vector2 (mRotation.x, mRotation.y);

		float h = Input.GetAxis ("Joy X");
		float v = Input.GetAxis ("Joy Y");

		Debug.Log (h + " " + v);

		mRotation += new Vector2 (-v*camSpeed.y, h*camSpeed.x) * Time.deltaTime;

//		if (Input.GetKey (KeyCode.J)) {
//			mRotation.y += camSpeed.x * Time.deltaTime;
//		}
//
//		if (Input.GetKey (KeyCode.L)) {
//			mRotation.y -= camSpeed.x * Time.deltaTime;
//		}
//
//		if (Input.GetKey (KeyCode.I)) {
//			mRotation.x += camSpeed.y * Time.deltaTime;
//		}
//
//		if (Input.GetKey (KeyCode.K)) {
//			mRotation.x -= camSpeed.y * Time.deltaTime;
//		}

		if (mRotation.Equals(rotBefore)) {
			mRotation = Vector2.Lerp(mRotation, new Vector2(30, mRotation.y), camResetSpeed * Time.deltaTime);
		}

	}

	
	// Update is called once per frame
	void LateUpdate() {
		TrackTarget();
//		SetRotation (mRotation.x, mRotation.y);

//		if (Input.GetKeyDown (KeyCode.LeftControl)) {
//			if(Cursor.lockState != CursorLockMode.Locked) {
//				Cursor.lockState = CursorLockMode.Locked; 
//				Cursor.visible = false;
//			} else {
//				Cursor.lockState = CursorLockMode.None;
//				Cursor.visible = true;
//			}
//		}
	}

	private void TrackTarget() {
//		
//		transform.position = Vector3.Slerp(transform.position, target.position, trackingSpeed);
//		if((transform.position - target.position).magnitude < 0.1f) {
//			mMoving = false;
//		} else {
//			transform.position = target.position;
//			mMoving = true;
//		}

		transform.position = target.position;

		Quaternion behindPlayer = Quaternion.LookRotation (target.forward, Vector3.up);
		Quaternion cameraRotation = Quaternion.Euler (mRotation.x, mRotation.y, 0f);

		
		Quaternion targetRotation = behindPlayer * cameraRotation;
		mPivot.rotation = Quaternion.Slerp (mPivot.rotation, targetRotation, Time.deltaTime * turnSmoothing);

//		if(mMoving) {
//		}

//		Vector3 targetPos = Vector3.Slerp (targetLastPostition, target.position, 0.9f);
//		transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, trackingSpeed);

//		if((transform.position - target.position).magnitude >= 0.1f) {
//			transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, trackingSpeed);
//		}

	}

	private void HandleRotation()
	{

//		Quaternion targetRotation = Quaternion.Euler (mCameraTilt, target.eulerAngles.y, 0f);
//		if(Quaternion.Angle (targetRotation, transform.rotation) >= 1) {
//			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * turnSmoothing);
//		}
	}

	private void SetRotation(float dx, float dy) {
		mPivot.eulerAngles = new Vector3 (dx, dy, 0f);
	}

	private void ZoomBy(float amount) {
		mCam.position += mCam.forward * amount;
	}
}
