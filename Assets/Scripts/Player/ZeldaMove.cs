using UnityEngine;
using System.Collections;

public class ZeldaMove : MonoBehaviour {

	public Transform _camera;

	private Rigidbody _body;

	private float _horizontal;
	private float _vertical;
	private bool _jump;

	// Use this for initialization
	void Awake () {
		if (_camera == null) {
			_camera = Camera.main.transform;
		}

		_body = GetComponent<Rigidbody>();

		_body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}
	
	// Update is called once per frame
	void Update () {
		_jump = Input.GetButtonDown ("Jump");

		Debug.Log (GetRelativeMove (_camera, Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")));
	}

	void FixedUpdate() {
		_horizontal = Input.GetAxis ("Horizontal");
		_vertical = Input.GetAxis ("Vertical");

	if (_horizontal != 0 || _vertical != 0) {
			Vector3 dir = GetRelativeMove (_camera, _horizontal, _vertical);
			Quaternion look = Quaternion.LookRotation (dir, Vector3.up);
			_body.MoveRotation (look);
			_body.MovePosition (_body.position + _body.transform.forward * 6f * Time.deltaTime);
		}
	}

	private Vector3 GetRelativeMove(Transform camera, float horizontal, float vertical) {
		return (Vector3.Scale(camera.forward, new Vector3 (1, 0, 1)).normalized) * vertical + camera.right * horizontal; 
	}
}
