using UnityEngine;
using System.Collections;

public class TrackingCameraRig : MonoBehaviour {

	public Transform target;
	[Range(0, 30)] public float minTilt = 0f;
	[Range(30, 60)] public float maxTilt = 45f;

	private Transform mCam;
	private Transform mPivot;
	private float mDefaultAngleX;

	private Vector2 mInput = Vector2.zero;

	// Use this for initialization
	void Awake () {
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
		mCam = GetComponentInChildren<Camera>().transform;
		mPivot = mCam.parent;
		mDefaultAngleX = mPivot.eulerAngles.x;
	}

	void Update() {
		mInput.x = Input.GetAxis ("Joy X");
		mInput.y = Input.GetAxis ("Joy Y");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = target.transform.position;

		float xRot = mInput.y == 0 ? mDefaultAngleX : mPivot.eulerAngles.x + mInput.y * 30f;
		xRot = Mathf.Clamp (xRot, minTilt, maxTilt);

		Quaternion cameraRotation = Quaternion.Euler (xRot, mPivot.eulerAngles.y + -mInput.x * 20f, 0f);
		mPivot.rotation = Quaternion.Slerp(mPivot.rotation, cameraRotation, Time.deltaTime * 4f);

//		mCam.localPosition = new Vector3 (0, 0, -10);

		if (Input.GetKey (KeyCode.LeftShift)) {
			foreach (Collider enemy in Physics.OverlapSphere(target.position, 10)) {
				Debug.Log (enemy.gameObject.transform.position);
				if (enemy.gameObject.GetComponent<Targetable> ()) {
					Quaternion look = Quaternion.LookRotation (enemy.gameObject.transform.position - mPivot.position, Vector3.up);
					mPivot.rotation = Quaternion.Slerp (mPivot.rotation, look, Time.deltaTime * 4f);
					//				mCam.localPosition = new Vector3 (0, 0, -2);
					break;
				}
			}
		}
	}
}
