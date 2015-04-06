using UnityEngine;
using System.Collections;

public class rooster : enemy {

	public int moveDir;
	//private Vector3 movement;
	protected int yVel;
	//protected bool onGround;
	// Use this for initialization
	protected void Start () {
		anim = GetComponent<Animator>();
	}
	void Awake()
	{
		base.Awake();
	}
	void OnEnable()
	{
		base.OnEnable();
	}
	// Update is called once per frame
	protected void FixedUpdate () {
		transform.localScale =  new Vector3( -moveDir, 1, 1 );
		transform.Translate ( .1f * moveDir, 0f, 0f );
		//if( onGround == false ) 
		{
			movement = new Vector3( moveDir * .1f, yVel ,0f );
			if( yVel > -8 )
				yVel--;
		}
		//onGround = false;
		
		movement *= Time.deltaTime*1;
		
		transform.Translate(movement.x ,movement.y, 0f);
		UpdateRaycasts();

		if( isDead == true )
			anim.SetBool( "isDead", true );
		if( yVel < -1 )
			anim.SetBool ("onGround", false);
		else
			anim.SetBool ("onGround", true);
	}

	void UpdateRaycasts()
	{
		//Debug.Log ("Inside UpdateRaycasts");
		//Raycast constants
		//int mask = LayerMask.NameToLayer( "Ground" );
		int mask = 1 << LayerMask.NameToLayer( "Ground" ); 
		Ray rayRight = new Ray( new Vector3( transform.position.x + .05f, transform.position.y, transform.position.z), Vector3.down ); //Detect objects below player
		Ray rayLeft = new Ray( new Vector3( transform.position.x - .05f, transform.position.y, transform.position.z), Vector3.down ); //Detect objects below player
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
		{	
			onGround = false;
		}

		/*rayRight = new Ray( new Vector3( transform.position.x + 2f, transform.position.y, transform.position.z), Vector3.down );
		objectBelow = ( Physics.Raycast ( rayRight, out hitInformation, 1f, mask) 
		                     );
		if( objectBelow == false ) 
		{
			//onGround = true;
			//Readjust player to top of playform
			//Vector2 newPos = new Vector2( transform.position.x, hitInformation.point.y + distance );
			//transform.position = newPos;
			//Debug.Log ("Hit something");
			if( onGround == true )
				yVel = 35;
		}*/

		Ray rayTop = new Ray( new Vector3( transform.position.x, transform.position.y + distance, transform.position.z ), Vector3.right );
		Ray rayBottom = new Ray( new Vector3( transform.position.x, transform.position.y - .01f, transform.position.z ), Vector3.right );
		
		bool objectRight = ( Physics.Raycast ( rayTop, out hitInformation, 1f, mask) 
		                    || Physics.Raycast ( rayBottom, out hitInformation, 1f, mask ) );
		
		if( objectRight == true )
		{
			Vector2 newPos = new Vector2( hitInformation.point.x - 1f, transform.position.y );
			transform.position = newPos;
			if( onGround == true )
				yVel = 25;
		}
		
		rayTop = new Ray( new Vector3( transform.position.x, transform.position.y + distance, transform.position.z ), Vector3.left );
		rayBottom = new Ray( new Vector3( transform.position.x, transform.position.y - .01f, transform.position.z ), Vector3.left );
		
		bool objectLeft = ( Physics.Raycast ( rayTop, out hitInformation, 1f, mask) 
		                   || Physics.Raycast ( rayBottom, out hitInformation, 1f, mask ) );
		
		if( objectLeft == true )
		{
			Vector2 newPos = new Vector2( hitInformation.point.x + 1f, transform.position.y );
			transform.position = newPos;
			if( onGround == true )
				yVel = 25;
		}
	}

		override
		protected void OnTriggerEnter2D( Collider2D collision )
	{
		if( collision.gameObject.CompareTag( "Hammer" ) )
		{
			audio.PlayOneShot( dieSfx, .6f );
			isDead = true;
			anim.SetBool( "isDead", true );
		}
		if( collision.gameObject.CompareTag( "Biter" ) )
		{
			audio.PlayOneShot( dieSfx, .6f );
			gameObject.SetActive(false);
			//isDead = true;
			//anim.SetBool( "isDead", true );
		}
		if( collision.gameObject.CompareTag( "Wind" ) )
			transform.Translate ( -2f * moveDir, 0f, 0f );
	}

	public void dead()
	{
		transform.gameObject.SetActive(false);
	}
}


