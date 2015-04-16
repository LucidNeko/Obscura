using UnityEngine;
using System.Collections;

public class RailCameraRig : MonoBehaviour {

	//tracks the target. target is set to the "Player" tagged game object if not specified
	public Transform target;
	public float cameraSmoothing = 1f;

	private Vector3 mRailStartPos;
	private Vector3 mRailEndPos;
	private Vector3 mRailDirection;
	private float mRailLength;

	private Transform mCam;
	private Transform mPivot;

	private Vector3 mCameraVelocity = Vector3.zero;

	// Use this for initialization
	void Awake () {
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}

		mRailStartPos = transform.position;
		mRailEndPos = GetComponentInChildren<RailCameraEnd> ().transform.position;
		mRailDirection = (mRailEndPos - mRailStartPos).normalized;
		mRailLength = (mRailEndPos - mRailStartPos).magnitude;

		mCam = GetComponentInChildren<Camera> ().transform;
		mPivot = mCam.parent;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float t;


		Vector3 toTarget = target.position - transform.position;
		Vector3 projectedPoint = transform.position + Vector3.Project (toTarget, mRailDirection); // Vector3.Project doc for example

		if (Vector3.Angle (mRailDirection, (projectedPoint - mRailStartPos).normalized) == 180) {
			projectedPoint = mRailStartPos;
		} else if ((projectedPoint - mRailStartPos).magnitude > mRailLength) {
			projectedPoint = mRailEndPos;
		}

		transform.position = projectedPoint;
//		transform.position = Vector3.Lerp(transform.position, projectedPoint, Time.deltaTime * cameraSmoothing);
//		transform.position = Vector3.SmoothDamp(transform.position, projectedPoint, ref mCameraVelocity, Time.deltaTime * cameraSmoothing);

//		Quaternion targetRotation = Quaternion.LookRotation (target.position - mPivot.position);
//		mPivot.rotation = Quaternion.Slerp (mPivot.rotation, targetRotation, Time.deltaTime * cameraSmoothing);

		mPivot.LookAt (target, Vector3.up);
	}
}
