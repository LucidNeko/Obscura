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

	private Vector3 _cameraDefaultPosition;

	private Vector3 _oldPos, _oldTargetPos;

	private float distanceFromPivot = 0;

	// Use this for initialization
	void Awake () {
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
		_camera = GetComponentInChildren<Camera>().transform;
		_pivot = _camera.parent;
		_defaultPitch = _pivot.eulerAngles.x;
		_defaultDistance = _camera.localPosition.z;
		_cameraDefaultPosition = _camera.localPosition;

		_oldPos = transform.position;
		_oldTargetPos = target.position;
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.F)) {
			Camera.main.GetComponent<CameraControls>().Shake(5, 0.5f);
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float horizontal = Input.GetAxis ("Horizontal");
		float camJoyX = Input.GetAxis ("Joy X");
		float camJoyY = Input.GetAxis ("Joy Y");

		HandlePosition ();
		HandleOrientation (horizontal, camJoyX, camJoyY, true, false);
		HandleCameraClipping(); //causing camera spazz

		distanceFromPivot = _camera.localPosition.z;
//		Debug.Log (distanceFromPivot);
	}

	private void HandlePosition() {
		//set rig base to be targets position
//		transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * trackingSpeed);
		transform.position = SuperSmoothLerp (_oldPos, _oldTargetPos, target.position, Time.time, trackingSpeed);

	     _oldPos = transform.position;
	     _oldTargetPos = target.position;
	}

	private void HandleOrientation(float horizontal, float camJoyX, float camJoyY, bool invertX, bool invertY) {
		camJoyX -= horizontal/2f; // pivot around camera when walking sideways // divide 2 is based on how far camera is away

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
		int layerMask = LayerMask.GetMask ("Camera Collision");
		float radius = 0.2f;

		RaycastHit info;
		if (Physics.SphereCast (_pivot.position, radius, (_camera.position - _pivot.position).normalized, out info, -_defaultDistance, layerMask)) {
			Vector3 point = info.point + info.normal*radius; //0.2f!! as in spherecast.

			float length = Vector3.Magnitude(_pivot.position - point) - radius;

			Vector3 p = _camera.localPosition;
			p.z = Mathf.Lerp(p.z, Mathf.Min(-1, -length), Time.deltaTime * zoomSpeed);
			_camera.localPosition = p;

			Debug.DrawLine(_pivot.position, info.point, Color.black);
			Debug.DrawLine(info.point, info.point + info.normal*2, Color.blue);
			Debug.DrawLine(_pivot.position, point, Color.green);
//			Debug.DrawLine(_pivot.position, _pivot.position + dir*info.distance, Color.black);
//			Debug.Log ("hit");
		} else {
			Vector3 pos = _camera.localPosition;
			pos.z = Mathf.Lerp(pos.z, _defaultDistance, Time.deltaTime * zoomSpeed);
			_camera.localPosition = pos;
//			Debug.Log ("no-hit");
			Debug.DrawLine(_pivot.position, _camera.position);
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
