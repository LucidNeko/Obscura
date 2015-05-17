using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

	public float rotationSpeed = 10f;
	public float moveSpeed = 6f;
	public float jumpForce = 15f;
	public float gravityMultiplier = 4f;
	public float groundRayLength = 0.2f;

	private Rigidbody _body;
	private Animator _anim;

	private bool _isGrounded; 
	private Vector3 _groundNormal;

	private float _turnAmount;
	private float _forwardAmount;

	private Vector3 _lastGround;
	private Vector3 _fallingVelocity = Vector3.zero;

	// Use this for initialization
	void Start() {
		_body = GetComponent<Rigidbody>();
		_anim = GetComponent<Animator>();

		//freeze rigidbody rotation
		_body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}

	//move should always be normalazied with magnitude <= 1
	public void Move(Vector3 move, bool jump) {

		CheckOnGround ();
		
		//If we are actually moving
		if (move != Vector3.zero) {
			//look in direction of move
			_body.MoveRotation (Quaternion.Slerp (_body.rotation, Quaternion.LookRotation (move), Time.deltaTime * rotationSpeed));

			//covert //global/camera move vector into local space
			move = transform.InverseTransformDirection (move);

			//project on a plane so that we walk slower up slopes.
			move = Vector3.ProjectOnPlane (move, _groundNormal);

			//angle between x and z
			_turnAmount = Mathf.Atan2 (move.x, move.z); 

			//if we are on flat ground it will be 1, if on a upwards slop it will be < 1. creating a slower move up slopes
			_forwardAmount = move.z;

			if(!_isGrounded) {
				_body.MovePosition (transform.position + transform.forward * _forwardAmount * moveSpeed * Time.deltaTime);
			}
			_body.MovePosition (Vector3.Lerp(_body.position, _body.position + _body.transform.forward * _forwardAmount * moveSpeed, Time.deltaTime));
		} else {
			//reset animator properties
			_turnAmount = 0f;
			_forwardAmount = 0f;
		}

		if (_isGrounded) {
			HandleJump(jump);
		} else {
			FallFaster();
			FallBackToEarth ();
		}


		UpdateAnimatorProperties();
	}

	private void CheckOnGround() {
		RaycastHit info;
		if(Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out info, groundRayLength)) {
			if(_isGrounded == false) {
				_lastGround = transform.position;
			}

			if(_fallingVelocity.y < -50) {
				FallDamage();
				_fallingVelocity = Vector3.zero;
			}

			_groundNormal = info.normal;
			_isGrounded = true;
		} else {
			_fallingVelocity = _body.velocity;
			_groundNormal = Vector3.up;
			_isGrounded = false;
		}
	}

	private void FallFaster() {
		//Add additional gravitational force
		//subtract gravity, so that a 2x multiplier just ads on one mroe lot of gravity.
		_body.AddForce ((Physics.gravity * gravityMultiplier) - Physics.gravity); 
	}

	private void HandleJump(bool jump) {
		if (jump && _isGrounded) {
			_body.velocity = new Vector3(_body.velocity.x, jumpForce, _body.velocity.z);
			_isGrounded = false;
		}
	}

	private void UpdateAnimatorProperties() {
		_anim.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
		_anim.SetFloat("Turn", _turnAmount, 0.1f, Time.deltaTime);
		_anim.SetBool("OnGround", _isGrounded);
		if (!_isGrounded) {
			_anim.SetFloat ("Jump", _body.velocity.y);
		} else {
			_anim.SetFloat ("Jump", 0f);
		}
	}

	private void FallBackToEarth() {
		if (transform.position.y < -30) {
			_body.MovePosition(_lastGround + Vector3.up*30);
			_body.velocity = Vector3.Scale(_body.velocity, new Vector3(0, 1, 0));
		}
	}

	private void FallDamage() {
		Debug.Log ("ouch");
		StartCoroutine (Ouch (2));
	}

	IEnumerator Ouch(float duration) {
		float t = 0;
		while(t < 1) {
			t += 0.25f; //speed
			Vector3 scale = transform.localScale;
			scale.y = Mathf.Lerp(1, 0.1f, t);
			transform.localScale = scale;
			yield return null;
		}

		yield return new WaitForSeconds (0.25f);

		t = 0;
		while (t < 1) {
			t += 0.1f; //speed
			Vector3 scale = transform.localScale;
			scale.y = Mathf.Lerp(0.1f, 1, t);
			transform.localScale = scale;
			yield return null;
		}
	}
}
