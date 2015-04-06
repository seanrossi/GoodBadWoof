using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

	private Transform thisTransform;
	public AudioClip dieSfx;
	protected Vector3 spawnPoint;
	public Animator anim;
	short direction = 1;
	protected short moveDir = 0;
	protected bool onGround = false;
	protected bool isDead = false;
	int atk = 0;
	short yVel, yAcc, xVel, xAcc, jumpVel, jumpTime, slide;
	protected Vector3 movement;
	private RaycastHit hit;
	private int groundMask;
	// Use this for initialization

	protected void Awake()
	{
		spawnPoint = transform.position;
	}
	protected void Start () {

		anim = GetComponent<Animator>();
		thisTransform = transform;
		//xa.isRight = false;
		anim.SetBool ("isWalking", false);
		groundMask = LayerMask.NameToLayer("Ground");
		yVel = 0;
		slide = 0;
		//animation.Stop();
	}
	// Use this for initialization
	public void OnEnable()
	{
		transform.localScale = new Vector3( 1f, 1f, 1f);
		isDead = false;
		anim.SetBool("isWalking", true );
		yVel = 0;
		transform.position = spawnPoint;
	}
	
	// Update is called once per frame
	protected void FixedUpdate () {
		if( !global_script.isPaused )
		{
		if( onGround == false ) 
		{
			movement = new Vector3(moveDir*.2f, yVel ,0f);
			if( yVel > -12 )
				yVel--;
		}
		//onGround = false;
		
		movement *= Time.deltaTime*1;
		
		thisTransform.Translate(movement.x ,movement.y, 0f);
		UpdateRaycasts();
		}
	}

	protected virtual void OnTriggerEnter2D( Collider2D collision )
	{
		if( collision.gameObject.CompareTag( "Hammer" ) )
		{
			//audio.PlayOneShot(dieSfx, 1f );
			isDead = true;
		}
	}

	void UpdateRaycasts()
	{
		//Debug.Log ("Inside UpdateRaycasts");
		//Raycast constants
		//int mask = LayerMask.NameToLayer( "Ground" );
		int mask = 1 << LayerMask.NameToLayer( "Ground" ); 
		Ray rayRight = new Ray( new Vector3( transform.position.x + .5f, transform.position.y, transform.position.z), Vector3.down ); //Detect objects below player
		Ray rayLeft = new Ray( new Vector3( transform.position.x - .5f, transform.position.y, transform.position.z), Vector3.down ); //Detect objects below player
		RaycastHit hitInformation;
		float distance = .3f;
		
		bool objectBelow = ( Physics.Raycast ( rayRight, out hitInformation, distance, mask) 
		                    || Physics.Raycast ( rayLeft, out hitInformation, distance, mask ) );
		
		if( objectBelow == true ) 
		{
			onGround = true;
			//Readjust player to top of playform
			Vector2 newPos = new Vector2( transform.position.x, hitInformation.point.y + distance );
			transform.position = newPos;
			//Debug.Log ("Hit something");
			yVel = 0;
		}
		else
			onGround = false;

		Ray rayTop = new Ray( new Vector3( transform.position.x, transform.position.y + distance, transform.position.z ), Vector3.right );
		Ray rayBottom = new Ray( new Vector3( transform.position.x, transform.position.y - .01f, transform.position.z ), Vector3.right );
		
		bool objectRight = ( Physics.Raycast ( rayTop, out hitInformation, 1f, mask) 
		                    || Physics.Raycast ( rayBottom, out hitInformation, 1f, mask ) );
		
		if( objectRight == true )
		{
			Vector2 newPos = new Vector2( hitInformation.point.x - 1f, transform.position.y );
			transform.position = newPos;
		}
		
		rayTop = new Ray( new Vector3( transform.position.x, transform.position.y + distance, transform.position.z ), Vector3.left );
		rayBottom = new Ray( new Vector3( transform.position.x, transform.position.y - .01f, transform.position.z ), Vector3.left );
		
		bool objectLeft = ( Physics.Raycast ( rayTop, out hitInformation, 1f, mask) 
		                   || Physics.Raycast ( rayBottom, out hitInformation, 1f, mask ) );
		
		if( objectLeft == true )
		{
			Vector2 newPos = new Vector2( hitInformation.point.x + 1f, transform.position.y );
			transform.position = newPos;
		}
	}
	

	void DestroySelf()
	{
		transform.gameObject.SetActive(false);
		//Destroy ( transform.gameObject );
	}
}
