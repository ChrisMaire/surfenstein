using UnityEngine;

public enum Trick
{
	Frontflip,
	Backflip,
	Kickflip,
	Handstand,
	Superman
}

public class Player : MonoBehaviour {
	public Rigidbody2D thisRigidbody;
	public BoxCollider2D BoardCollider;
	public float GroundedCheckDistance = 1.0f;
	
	//if this is set to false, speed will instead increase exponentially
	public bool LinearAcceleration = true;
	public float CurrentSpeed = 1.0f;
	public float Acceleration = 0.01f;
	public float MaxSpeed = 15.0f;

	private float initialSpeed;
	private float initialAccel;

	public float SpinSpeed = 10f;
	public Vector3 boostVelocity, jumpVelocity, additionalJump;
	public float gameOverY;
	public float JumpTimeTotal = 0.3f;
	public float JumpTimeCurrent = 0.0f;
	
	public AudioSource audio_jump; 
	public AudioSource audio_background;

	public Announcer announcer;
	public ScoreManager scoreManager;
	public Camera MainCam;

	public PhysicsMaterial2D PlayerMaterial;
	public PhysicsMaterial2D GroundMaterial;
	public LayerMask groundCheckMask;

	public float MaxY;

	public Vector3 Position
	{
		get{ return thisTransform.position; }
	}

	public bool IsJumping
	{
		get{return isJumping;}
	}
	public bool IsGrounded
	{
		get{return touchingGround;}
	}
	private bool touchingGround = false;
	private bool isJumping = false;
	private Vector3 startPosition;
	
	private bool doJump, holdJump;
	public Animator boardAnimator;
	public Animator playerAnimator;
	private Transform thisTransform;

	Vector3 extents;
	Vector3 lastPoint;

	public float CurrentSpin = 0.0f;
	public float LastAngle = 0.0f;
	void Awake(){
		extents = BoardCollider.size * 0.5f;
		thisTransform = transform;
	}

	void Start () {
		thisTransform = transform;
		thisRigidbody = rigidbody2D;
		thisRigidbody.isKinematic = true;
		
		startPosition = transform.localPosition;

		enabled = false;
		
		initialSpeed = CurrentSpeed;
		initialAccel = Acceleration;

		lastPoint = transform.TransformDirection(Vector3.forward);
		lastPoint.z = 0;
	}

	public void Reset () {
		thisTransform.localPosition = startPosition;
		thisTransform.localRotation = Quaternion.identity;

		thisRigidbody.angularVelocity = 0.0f;
		thisRigidbody.isKinematic = false;
		thisRigidbody.velocity = new Vector3(0,0,0);

		CurrentSpeed = initialSpeed;
		Acceleration = initialAccel;

		enabled = true;
	}

	void Update () {
		//again this math isn't ideal. that GroundedCheckDistance should by based on the sprite height.
		//or perhaps rather than raycasting there should be an OnCollisionEnter/Leave to set grounded?
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, GroundedCheckDistance + (extents.y),groundCheckMask);
		//im sure theres a faster way than checking Contains
		if (hit.collider != null && hit.collider.sharedMaterial == GroundMaterial)
		{
			boardAnimator.SetBool("Jumping", false);
			touchingGround = true;

			scoreManager.EndCombo();
		} 
		else 
		{
			touchingGround = false;
		}
		
		if(isJumping)
		{
			JumpTimeCurrent += Time.deltaTime;
		}
		
		if(touchingGround)
		{
			JumpTimeCurrent = 0.0f;
		}
		
		if (Input.GetButtonDown ("Jump")) {
			if (touchingGround) {
				doJump = true;
				isJumping = true;
				boardAnimator.SetBool("Jumping", true);
				//audio_jump.Play();
			} else {
			}
		} else if (Input.GetButtonUp ("Jump")) {
			isJumping = false;
			holdJump = false;
			boardAnimator.SetBool("Jumping", false);
		} else {
			if(isJumping){
				holdJump = true;
			}
			if(JumpTimeCurrent > JumpTimeTotal)
			{
				holdJump = false;
				isJumping = false;
			}
		}

		//handstand works indepent of jumping
		if(Input.GetButton("Handstand1") && Input.GetButton("Handstand2"))
		{
			playerAnimator.SetBool("Handstand",true);
		} else {
			playerAnimator.SetBool("Handstand",false);
		}
		
		if (Input.GetButton("Superman1") && Input.GetButton("Superman2"))
		{
			collider2D.enabled = false;
			playerAnimator.SetBool("Superman",true);
		} else {
			playerAnimator.SetBool("Superman",false);
		}

		if(!touchingGround)
		{
			if(Input.GetButton("Flip"))
			{
				boardAnimator.SetBool("DoFlip",true);
			} else {
				boardAnimator.SetBool("DoFlip",false);
			}

			//while in air, get angle to check for back/front flips
			Vector3 eulers = thisTransform.rotation.eulerAngles;
			float diff = LastAngle - eulers.z;
			//Debug.Log(diff);
			if((diff > 0 && diff < 180) || (diff < 0 && diff > -180))
			{
				CurrentSpin += diff;
			}
			
			//Debug.Log(diff + " " + CurrentSpinForward + " " + CurrentSpinBackward);
			if(CurrentSpin >= 345)
			{ 
				announcer.AnnounceFrontFlip();
				CurrentSpin = 0.0f;
			}
			if (CurrentSpin <= -345){
				announcer.AnnounceBackFlip();
				CurrentSpin = 0.0f;
			}
			LastAngle = eulers.z;

		} else {
			boardAnimator.SetBool("DoFlip",false);
			//playerAnimator.SetBool("Handstand",false);
			//playerAnimator.SetBool("Superman",false);
		}
		
	}
	
	void FixedUpdate () {
		if(doJump){
			thisRigidbody.AddForce (jumpVelocity);
			doJump = false;
		}
		
		if(holdJump)
		{
			thisRigidbody.AddForce (additionalJump);
		}
		// rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.VelocityChange);
		if (LinearAcceleration) {
			CurrentSpeed += Acceleration * Time.deltaTime;
		} else {
			CurrentSpeed *= 1 + Acceleration;
		}

		if(CurrentSpeed >= MaxSpeed)
			CurrentSpeed = MaxSpeed;

		/*
		Vector3 vel = Board.velocity;
		vel.x += CurrentSpeed;
		Board.velocity = vel;*/
		//pos.x += CurrentSpeed;
		//thisTransform.position = pos;
		thisRigidbody.velocity = new Vector2(CurrentSpeed,thisRigidbody.velocity.y); //.AddRelativeForce(thisTransform.forward * CurrentSpeed);
		Vector3 pos = thisTransform.position;
		
		if(pos.y > MaxY)
		{
			pos.y = MaxY;
			thisTransform.position = pos;
		}

		if(!touchingGround)
		{
			float spin = Input.GetAxis("Spin");
			if(spin != 0)
			{
				thisRigidbody.AddTorque(spin * SpinSpeed);
			}


		} else {

			CurrentSpin = 0.0f;
		}
	}

	public void DoTrick(Trick t)
	{
		switch(t)
		{
			case(Trick.Frontflip):
				announcer.AnnounceFrontFlip();
				break;
			case(Trick.Backflip):
				announcer.AnnounceFrontFlip();
				break;
			case(Trick.Handstand):
				announcer.AnnounceHandstand();
				break;
			case(Trick.Kickflip):
				announcer.AnnounceFrontFlip();
				break;
			default:
				Debug.Log("Unknown trick?!");
				break;
		}
		scoreManager.AddTrick(t);
	}

	private void GameOver () {
		//renderer.enabled = false;
		thisRigidbody.isKinematic = true;
		enabled = false;
		//audio_background.Stop ();
	}
}

