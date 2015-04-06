using UnityEngine;
using System.Collections;

public class woof_logic_controller : MonoBehaviour {
	//Player Stats
	//Define coordinates where player spawns when starting or dead
	Vector3 spawnPoint;
	//string currentLevel;
	int hp, mHp, lives;
	int recycling, maxRecycling, cans, bottles;
	public Texture hpTexture, mHpTexture, statusTexture;
	public AudioClip hammerSfx, dieSfx, jumpSfx, barkSfx, biteSfx;
	private Transform thisTransform;
	Animator anim;
	short hitBuffer = 0;
	short direction = 1;
	short moveDir = 0;
	bool onGround = false;
	bool invincible;
	int atk = 0;
	float yVel, yAcc, xVel, xAcc, jumpVel, jumpTime, slide;
	private Vector3 movement;
	private RaycastHit hit;
	private int groundMask;
	protected bool movementConditions = ( xa.isAttack == false );
	bool isPuffed, bite;
	// Use this for initialization
	void Start () {
		//transform.gameObject.SetActive(true);
		//global_script.isPaused = false;
		invincible = false;
		recycling = 0;
		maxRecycling = global_script.setRecycling();
		//maxRecycling = GameObject.Find ("Start_Point_01").gameObject.GetComponent<levelVars>().maxRecycling;
		cans = 0;
		bottles = 0;
		lives = 5;
		spawnPoint = GameObject.Find ("Start_Point_01").transform.position;
		transform.position = spawnPoint;
		mHp = 5;
		hp = mHp;
		anim = GetComponent<Animator>();
		thisTransform = transform;
		xa.isRight = false;
		anim.SetBool ("isWalking", false);
		groundMask = LayerMask.NameToLayer("Ground");
		yVel = 0;
		slide = 0;
		isPuffed = false;
		bite = false;
		//animation.Stop();
	}

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	void resetTriggers()
	{
		transform.Translate( -100f, 0f, 0f );
	}

	void reSpawn()
	{
		//anim.SetTrigger( "deathTrigger" );
		invincible = false;
		lives--;

		if( lives == 0 )
			Application.LoadLevel(1);
		global_script.resetRecycling();
		recycling = 0;
		cans = 0;
		bottles = 0;
		transform.position = spawnPoint;
		hp = mHp;
		yVel = 0;
		xVel = 0;
		slide = 0;
		anim.SetBool ("isWalking", false);
		GameObject.Find ("Main Camera").GetComponent<camera>().Start ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if( !global_script.isPaused )
		{
		//Update stats
		/*for( int i = 0; i < hp; i++ )
		{

		}*/

		//CHECK INPUT

		if( xa.isJump == true )
		{
			anim.SetBool ( "isJumping", true );
			anim.SetBool ( "isWalking", false );
		}
		else if( onGround == true )
		{
			anim.SetBool ( "isJumping", false );
		}
		if( xa.isDown == true && xa.isSlide == true )
		{
			if( onGround == true )
				slide = 20;
		}
		else if( xa.isRight == true && xa.isDown == false && slide == 0f && ( atk == 0 || onGround == false ) && xa.isBite == false )
		{
			if( onGround == true )
			{
				anim.SetBool ("isWalking", true);
				direction = 1;
			}
			moveDir = 1;
				if( isPuffed == false )
				{
			/*if( xVel < 0f )
				xVel += .07f;
			else if( xVel < .28f )
				xVel += .01f;
				}
				*/
					if( xVel < 0f )
						xVel += 1f;
					else if( xVel < 15f )
						xVel += .5f;
				}
				else
					xVel = .1f;
			//transform.Translate (new Vector2( xVel, 0 ) );
		}
		else if( xa.isLeft == true && xa.isDown == false && slide == 0 && ( atk == 0 || onGround == false ) )
		{
			if( onGround == true )
			{
				anim.SetBool ("isWalking", true);
				direction = -1;
			}
			moveDir = -1;
				if( isPuffed == false )
				{
			/*if( xVel > 0 )
				xVel -= .07f;
			else if( xVel > -0.28f )
				xVel -= .01f;
				}*/
				if( xVel > 0 )
					xVel -= 1f;
				else if( xVel > -15f )
					xVel -= .5f;
			}
				else
					xVel = -.1f;
				//transform.Translate (new Vector2( xVel, 0 ) );
		}
		else if( slide == 0 )
		{
			anim.SetBool ("isWalking", false);
			moveDir = 0;
			if( xVel > 0 )
				xVel -= 1f;
			else if( xVel < 0 )
				xVel += 1f;
			//transform.Translate (new Vector2( xVel, 0 ) );
			//Debug.Log ("Standing still, xVel = " + xVel.ToString() );
		}

		if( slide > 0 )
		{
			anim.SetBool( "isSliding", true );
			//Debug.Log ("sliding");
			transform.Translate (new Vector2( direction * 0.4f, 0 ) );
			slide--;
			if( slide == 0 )
				anim.SetBool( "isSliding", false );
		}

		if( xa.isDown == true && slide == 0 )
		{
			if( anim.GetBool ("isDucking") == false )
				transform.Translate ( new Vector2( 0, -1.75f) );
			anim.SetBool ("isDucking", true);
		}
		else if ( slide == 0 )
		{
			//if( anim.GetBool ("isDucking") == true )
				//transform.Translate ( new Vector2( 0, 0f) );
			anim.SetBool ("isDucking", false);
			//transform.Translate ( new Vector2( 0, .05f) );
		}
		if( xa.isUp == true )
			anim.SetBool("isInhaling", true);
		else
			anim.SetBool("isInhaling", false);
		//Flip character around if movement direction switches (and not in mid-jump)
		if( onGround == true )
			transform.localScale =  new Vector3( direction, 1, 1 );
		if( xa.isAttack == true )
		{
			moveDir = 0;
			if( atk == 0 )
				atk++;
			anim.SetInteger( "atk", atk );
		}

		//UPDATE MOVEMENT AND ATTACK
		if( atk < 35 && atk > 0 )
			atk++;
		else if( atk == 35 )
		{
			atk = 0;
			anim.SetInteger( "atk", atk );
		}

		if( xa.isBite == true )
		{
				moveDir = 0;
				anim.SetBool( "isBite", true );
				//xa.isBite = false;
		}




		//Jumping mechanics
		/*if((!xa.falling || xa.onLadder || xa.onRope)) 
		{
			if( xa.attacking )
				direction = 0;
			if( jumpTime < 20 && xa.jumping && !xa.onLadder && !xa.onRope ) 
			{
				//int yVel = 4;
				movement = new Vector3(moveDir*.2f, jumpVel - (.02f * jumpTime) ,0f);
				if( jumpVel - .04f*jumpTime > -2.5 ) 
					jumpTime++;
			}
			else 
				movement = new Vector3(moveDir*.2f, 0,0f);
			movement *= Time.deltaTime*1;
			thisTransform.Translate(movement.x,movement.y, 0f);
		}*/
		
		// player is falling so apply gravity
		//else if( xa.falling )
		{
			//if( jumpTime < 15 && xa.jumping ) 
			if( xa.isJump && onGround == true && xa.isDown == false ) 
			{
					audio.PlayOneShot(jumpSfx, .6f );
				//if( jumpVel > .02f*jumpTime )
				//Jump height/vel set here
				yVel = 28;
				movement = new Vector3(moveDir*.2f, (jumpVel) - (.04f * jumpTime) ,0f);
				movement = new Vector3(moveDir*.2f, yVel ,0f);
				if( jumpVel - .04f*jumpTime > -2.0 ) 
					jumpTime++;
			}
			//movement = new Vector3(0f,-1f,0f);
			//else if( jumpTime == 25 )
			//else if (xa.isJump) movement = new Vector3(moveDir*.2f, 0f ,0f);
			else if( xa.isJump == false && yVel > 0 )
				yVel -= 2;
			if( onGround == false ) 
			{
				//if( yVel == 0 )
					//Debug.Log( "Max pos = " + transform.position.y.ToString () );
				movement = new Vector3(moveDir*.2f, yVel ,0f);
				//Terminal velocity set here
				if( yVel > -28 )
					yVel--;
			}
			//onGround = false;
			movement.x = xVel;
			movement *= Time.deltaTime*1;
			
			thisTransform.Translate(movement.x ,movement.y, 0f);
			if( hitBuffer > 0 )
			{
				hitBuffer--;
				if( hitBuffer % 2 == 1 )
					transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
				else
					transform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			}
			UpdateRaycasts();
		}
	}
	}

	void OnTriggerEnter2D( Collider2D collision )
	{
		if( collision.gameObject.CompareTag( "Enemy" ) )
		{
			//Debug.Log ( "Hit enemy!" );
			if( hitBuffer == 0 )
			{

				hp--;
				if( hp == 0 )
				{
					//lives--;
					audio.PlayOneShot(dieSfx, 1f);
						anim.SetTrigger( "deathTrigger" );
						//reSpawn ();
				}
				else
				{
					audio.PlayOneShot( barkSfx, .6f );
					hitBuffer = 60;
					if( collision.gameObject.transform.position.x > transform.position.x )
						transform.Translate ( new Vector2( -1f, 0f ) );
					else if( collision.gameObject.transform.position.x < transform.position.x )
						transform.Translate ( new Vector2( 1f, 0f ) );
					yVel = 8;
				}
			}

			//Vector3 newPos = new Vector3( transform.position.x, collision.gameObject.transform.position.y 
			                       //      + collision.gameObject.renderer.bounds.extents.y * 2 + 0.085f, 0 );
			//transform.position = newPos;
			//transform.Translate( 0, .019f, 0 );
			//onGround = true;
		}
		if( collision.gameObject.CompareTag( "Spike" ) && invincible == false )
		{
			invincible = true;
			//hitBuffer = 12;
			//transform.Translate( -100f, 0f, 0f );
			//lives--;
			{
				anim.SetTrigger( "deathTrigger" );
				audio.PlayOneShot(dieSfx, 1f);
				//reSpawn();
			}
		}
		//if( collision.gameObject.CompareTag("LevelEnd") )
			//Application.LoadLevel(1);

		if( collision.CompareTag("Section") )
		{
			//Vector3 newPos = new Vector3( collision.gameObject.transform.position.x - collision.gameObject.GetComponent<BoxCollider2D>().size.x/2, collision.gameObject.transform.position.y, 0f);
			//Vector2 newBounds = new Vector2( collision.gameObject.transform.position.x - collision.gameObject.GetComponent<BoxCollider2D>().size.x/2, collision.gameObject.transform.position.y - collision.gameObject.GetComponent<BoxCollider2D>().size.y/2 );
			GameObject.Find ("Main Camera").GetComponent<camera>().shiftCamera( collision.gameObject );
		}
	}

	void OnTriggerExit( Collider collision )
	{
		/*if( collision.gameObject.CompareTag( "Ground" ) )
		{
			//Vector3 newPos = new Vector3( transform.position.x, collision.gameObject.transform.position.y 
			//      + collision.gameObject.renderer.bounds.extents.y * 2 + 0.085f, 0 );
			//transform.position = newPos;
			transform.Translate( 0, .025f, 0 );
			onGround = false;
		}*/
	}
	//Update Raycasts to detect collisions
	void UpdateRaycasts()
	{
		//Debug.Log ("Inside UpdateRaycasts");
		//Raycast constants
		//int mask = LayerMask.NameToLayer( "Ground" );
		//int mask = ~(0 << 8); 
		int mask = 1 << LayerMask.NameToLayer("Ground");

		Ray rayRight = new Ray( new Vector3( transform.position.x + .65f, transform.position.y, transform.position.z), Vector3.down ); //Detect objects below player
		Ray rayLeft = new Ray( new Vector3( transform.position.x - .65f, transform.position.y, transform.position.z), Vector3.down ); //Detect objects below player
		RaycastHit hitInformation; 
		float distance = 2.75f;
		if( xa.isDown )
			distance = 1.75f;
		bool objectBelow = ( Physics.Raycast ( rayRight, out hitInformation, distance, mask) 
		                    || Physics.Raycast ( rayLeft, out hitInformation, distance, mask ) );
		 
		if( objectBelow == true ) 
		{
			onGround = true;
			Vector2 newPos = new Vector2( 0f, 0f);
			//Readjust player to top of playform
			if( hitInformation.transform.gameObject.CompareTag("Moving") )
			{
				transform.Translate(hitInformation.transform.gameObject.GetComponent<movingPlatform>().movement.x ,
				                    0f, 0f );
				Debug.Log ("Player is moving " + hitInformation.transform.gameObject.GetComponent<movingPlatform>().movement.x*3 );
			}
				//transform.parent = hitInformation.transform;
			newPos = new Vector2( transform.position.x, hitInformation.point.y + distance - .25f );
			//Vector2 newPos = new Vector2( transform.position.x + hitInformation.point.x, hitInformation.point.y + distance );
			transform.position = newPos;
			//Debug.Log ("Hit something");
			yVel = 0;
		}
		else
			onGround = false;

		//Reuse raycasts to detect collision above player
		rayRight = new Ray( new Vector3( transform.position.x + .65f, transform.position.y, transform.position.z), Vector3.up ); //Detect objects below player
		rayLeft = new Ray( new Vector3( transform.position.x - .65f, transform.position.y, transform.position.z), Vector3.up ); //Detect objects below player
		distance = 2.75f;
		
		bool objectAbove = ( Physics.Raycast ( rayRight, out hitInformation, distance, mask) 
		                    || Physics.Raycast ( rayLeft, out hitInformation, distance, mask ) );
		
		if( objectAbove == true ) 
		{
			//onGround = true;
			//Readjust player to top of playform
			Vector2 newPos = new Vector2( transform.position.x, hitInformation.point.y - distance );
			transform.position = newPos;
			//Debug.Log ("Hit something");
			yVel = 0;
		}

		if( xa.isDown == true )
			distance = 1f;
		else distance = 2f;

		Ray rayTop = new Ray( new Vector3( transform.position.x, transform.position.y + distance, transform.position.z ), Vector3.right );
		Ray rayBottom = new Ray( new Vector3( transform.position.x, transform.position.y - distance, transform.position.z ), Vector3.right );

		bool objectRight = ( Physics.Raycast ( rayTop, out hitInformation, 1f, mask) 
		                    || Physics.Raycast ( rayBottom, out hitInformation, 1f, mask ) );

		if( objectRight == true )
		{
			Vector2 newPos = new Vector2( hitInformation.point.x - 1f, transform.position.y );
			transform.position = newPos;
		}

		rayTop = new Ray( new Vector3( transform.position.x, transform.position.y + distance, transform.position.z ), Vector3.left );
		rayBottom = new Ray( new Vector3( transform.position.x, transform.position.y - distance, transform.position.z ), Vector3.left );
		
		bool objectLeft = ( Physics.Raycast ( rayTop, out hitInformation, 1f, mask) 
		                    || Physics.Raycast ( rayBottom, out hitInformation, 1f, mask ) );
		
		if( objectLeft == true )
		{
			Vector2 newPos = new Vector2( hitInformation.point.x + 1f, transform.position.y );
			transform.position = newPos;
		}
	}

	void setAtkFinished()
	{
		anim.SetInteger( "atk", 0 );
		atk = 0;
	}

	void setBiteFalse()
	{
		anim.SetBool( "isBite", false );
	}

	void OnGUI()
	{
		GUI.skin.label.fontSize = 15;
		GUI.DrawTexture( new Rect( 0, 0, 1365, 50 ), statusTexture );
		GUI.Label(new Rect( 8, 4, 50, 50 ), "Health" );
		for( int i = 0; i < mHp; i++ )
			GUI.DrawTexture( new Rect( i * 10f + 8, 24f, 10f, 20f ), mHpTexture );
		for( int j = 0; j < hp; j++ )
			GUI.DrawTexture( new Rect( j * 10f + 8, 24f, 10f, 20f ), hpTexture );
		//GUI.contentColor
		GUI.skin.label.fontSize = 20;
		GUI.Label(new Rect( 200, 24, 100, 50 ), "Lives: " + lives.ToString() );
		GUI.Label(new Rect( 300, 24, 200, 50 ), "Recycling: " + recycling.ToString() );
		GUI.Label(new Rect( 440, 24, 200, 50 ), " / " + maxRecycling.ToString() );

		if( global_script.isPaused && xa.isMenu == true )
		{
			if(GUI.Button ( new Rect(Screen.width/2, Screen.height/2 - 200, 100, 100 ), "Resume" ) )
				global_script.isPaused = false;
			if(GUI.Button ( new Rect(Screen.width/2, Screen.height/2 - 100, 100, 100 ), "Exit Level" ) )
			{
				//global_script.isPaused = false;
				Application.LoadLevel(1);
			}
			if(GUI.Button ( new Rect(Screen.width/2, Screen.height/2, 100, 100 ), "Exit Game" ) )
				Application.Quit();
		}
	}

	public void setSpawnPoint( Vector3 newSpawnPoint )
	{
		spawnPoint = newSpawnPoint;
	}

	void playBiteSfx()
	{
		audio.PlayOneShot( biteSfx, .6f );
	}

	void playHammerSfx()
	{
		audio.PlayOneShot( hammerSfx, .6f );
	}

	void playDieSfx()
	{
		audio.PlayOneShot( dieSfx, .6f );
	}

	void playWoofSfx()
	{
		audio.PlayOneShot( barkSfx, .6f );
	}

	void OnLevelWasLoaded( int level )
	{
		if( level > 1 )	//If level is title screen or stage select
		//	transform.gameObject.SetActive(false);
		//else
		{
			Start ();

		}
		else{
			transform.position = new Vector3( -1000, -1000, -2 );
		}
		global_script.isPaused = false;
	}

	public void enablePuff()
	{
		isPuffed = true;
	}

	public void disablePuff()
	{
		isPuffed = false;
	}

	public void addRecycling( int value )
	{
		//recycling += value;
		if( value == 5 )
			cans++;
		else if( value == 10 )
			bottles++;
		recycling ++;
	}

	public void restoreHp( int value )
	{
		hp += value;
		if( hp > mHp )
			hp = mHp;
	}

	public void increaseMaxHp( int value )
	{
		mHp += value;
	}

	public bool isRecyclingComplete()
	{
		if( recycling == maxRecycling )
			return true;
		else return false;
	}

	public bool undefeated()
	{
		if( lives == 5 )
			return true;
		else 
			return false;
	}

	public bool untouched()
	{
		if( hp == mHp )
			return true;
		else
			return false;
	}

	public void endLevel()
	{
		GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
		global_script.isPaused = true;
		anim.SetBool( "victory", true );
	}
	
	public void transitionLevel()
	{
		global_script.isPaused = false;
		anim.SetBool( "victory", false );
		Application.LoadLevel( GameObject.Find( "exit_sign" ).GetComponent<levelEnd>().destination );
	}
}
