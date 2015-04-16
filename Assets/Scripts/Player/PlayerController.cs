using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

	public float groundRayLength = 0.2f;
	public float jumpForce = 15f;
	public float gravityMultiplier = 4f;

	private Rigidbody mBody;
	private CapsuleCollider mCapsule;
	private Animator mAnim;
	private float mCapsuleHeight;
	private Vector3 mCapsuleCenter;

	private bool mIsGrounded; 
	private Vector3 mGroundNormal;

	private float mTurnAmount;
	private float mForwardAmount;


	// Use this for initialization
	void Start() {
		mBody = GetComponent<Rigidbody>();
		mAnim = GetComponent<Animator>();
		mCapsule = GetComponent<CapsuleCollider>();
		mCapsuleHeight = mCapsule.height;
		mCapsuleCenter = mCapsule.center;

		//freeze rigidbody rotation
		mBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}

	//move should always be normalazied with magnitude <= 1
	public void Move(Vector3 move, bool jump) {

		if (move != Vector3.zero) {
			
//			transform.forward = move; //Vector3.Lerp (transform.forward, move, Time.deltaTime * 10f);
			mBody.MoveRotation(Quaternion.Slerp(mBody.rotation, Quaternion.LookRotation(move), Time.deltaTime*10));
			move = transform.InverseTransformDirection (move);

			CheckOnGround ();
			move = Vector3.ProjectOnPlane (move, mGroundNormal);
			mTurnAmount = Mathf.Atan2 (move.x, move.z); //angle between x and z
			mForwardAmount = move.z;

			//		float turnSpeed = 100f;
			//		transform.Rotate(0, mTurnAmount * turnSpeed * Time.deltaTime, 0);


			//		Debug.Log (move);

			mBody.MovePosition (transform.position + transform.forward * mForwardAmount * 6f * Time.deltaTime);
		}

		if (mIsGrounded) {
			HandleJump(jump);
		} else {
			FallFaster();
		}

		UpdateAnimatorProperties();
	}

	private void CheckOnGround() {
		RaycastHit info;
		if(Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out info, groundRayLength)) {
//		if(Physics.SphereCast(transform.position + (Vector3.up * 0.1f), 0.1f, Vector3.down, out info, groundRayLength)) {
			mGroundNormal = info.normal;
			mIsGrounded = true;
		} else {
			mGroundNormal = Vector3.up;
			mIsGrounded = false;
		}
	}

	private void FallFaster() {
		mBody.AddForce ((Physics.gravity * gravityMultiplier) - Physics.gravity); //subtract gravity, so that a 2x multiplier just ads on one mroe lot of gravity.

		//adjust ground check distance?
	}

	private void HandleJump(bool jump) {
		if (jump && mIsGrounded) {
			mBody.velocity = new Vector3(mBody.velocity.x, jumpForce, mBody.velocity.z);
			mIsGrounded = false;
		}
	}

	private void UpdateAnimatorProperties() {
		mAnim.SetFloat("Forward", mForwardAmount, 0.1f, Time.deltaTime);
		mAnim.SetFloat("Turn", mTurnAmount, 0.1f, Time.deltaTime);
		mAnim.SetBool("OnGround", mIsGrounded);
		if(!mIsGrounded) {
			mAnim.SetFloat("Jump", mBody.velocity.y);
		}
	}
}
