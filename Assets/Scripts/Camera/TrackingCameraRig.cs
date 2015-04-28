using UnityEngine;
using System.Collections;

public class TrackingCameraRig : MonoBehaviour {

	public Transform target;
	[Range(0, 30)] public float minTilt = 0f;
	[Range(30, 60)] public float maxTilt = 45f;
	public float zoomSpeed = 5f;
	public float trackingSpeed = 10f;
	public float yawSpeed = 20f;
	public float pitchSpeed = 30f;
	public float rotationSpeed = 4f;

	private Transform _camera;
	private Transform _pivot;
	private float _defaultPitch;
	private float _defaultDistance;

	private Vector3 _oldPos, _oldTargetPos;

	// Use this for initialization
	void Awake () {
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
		_camera = GetComponentInChildren<Camera>().transform;
		_pivot = _camera.parent;
		_defaultPitch = _pivot.eulerAngles.x;
		_defaultDistance = _camera.localPosition.z;

		_oldPos = transform.position;
		_oldTargetPos = target.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float horizontal = Input.GetAxis ("Horizontal");
		float camJoyX = Input.GetAxis ("Joy X");
		float camJoyY = Input.GetAxis ("Joy Y");

		HandlePosition ();
		HandleOrientation (horizontal, camJoyX, camJoyY, true, false);
		HandleCameraClipping();

	}

	private void HandlePosition() {
		//set rig base to be targets position
//		transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * trackingSpeed);
		transform.position = SuperSmoothLerp (_oldPos, _oldTargetPos, target.position, Time.time, trackingSpeed);

	     _oldPos = transform.position;
	     _oldTargetPos = target.position;
	}

	private void HandleOrientation(float horizontal, float camJoyX, float camJoyY, bool invertX, bool invertY) {
		camJoyX -= horizontal; // pivot around camera when walking sideways

		//invert if required
		camJoyX = invertX ? -camJoyX : camJoyX;
		camJoyY = invertY ? -camJoyY : camJoyY;

		//calculate pitch. if no camJoyY input resets pitch to default pitch.
		float pitch = camJoyY == 0 ? _defaultPitch : _pivot.eulerAngles.x + camJoyY * pitchSpeed;
		pitch = Mathf.Clamp (pitch, minTilt, maxTilt);

		//calculate yaw based on camJoyX
		float yaw = _pivot.eulerAngles.y + camJoyX * yawSpeed;

		//interpolate to the new orientation
		Quaternion newRotation = Quaternion.Euler (pitch, yaw, 0f);
		_pivot.rotation = Quaternion.Slerp(_pivot.rotation, newRotation, Time.deltaTime * rotationSpeed);
	}


	//shoots a ray from pivot to camera. sets the cameras z offset based on how far along the ray it managed to travel.
	private void HandleCameraClipping() {
		RaycastHit info;
		if (Physics.Raycast (_pivot.position, (_camera.position - _pivot.position).normalized, out info, -_defaultDistance)) {
			Vector3 pos = _camera.localPosition;
			pos.z = Mathf.Lerp(pos.z, Mathf.Min(-1, -info.distance), Time.deltaTime * zoomSpeed);
			_camera.localPosition = pos;
		} else {
			Vector3 pos = _camera.localPosition;
			pos.z = Mathf.Lerp(pos.z, _defaultDistance, Time.deltaTime * zoomSpeed);
			_camera.localPosition = pos;
		}
	}

	/** 
	 * Super Smooth Lerp code from:
	 * http://forum.unity3d.com/threads/how-to-smooth-damp-towards-a-moving-target-without-causing-jitter-in-the-movement.130920/
	 */
	private Vector3 SuperSmoothLerp(Vector3 x0, Vector3 y0, Vector3 yt, float t, float k) {
		Vector3 f = x0 - y0 + (yt - y0) / (k * t);
		return yt - (yt - y0) / (k*t) + f * Mathf.Exp(-k*t);
	}
}
