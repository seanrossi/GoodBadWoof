using UnityEngine;
using System.Collections;

public class cat : enemy {

	public AudioClip meowSfx;
	private short meowTimer;
	public float xVel = .25f;
	bool onGround = false;
	int yVel = 0;
	public int rotation = 0;	//0 for upright, 1 for facing left, 2 for upsidedown, 3 for facing right
	public int moveDir = -1;
	// Use this for initialization
	void Start () {
		meowTimer = 120;
		transform.localScale =  new Vector3( -moveDir, 1, 1 );
		/*if( rotation == 1 )
			transform.Rotate ( 0f, 0f, 90f );
		if( rotation == 2 )
			transform.Rotate ( 0f, 0f, 180f );
		if( rotation == 3 )
			transform.Rotate ( 0f, 0f, -90f );*/
		base.Start ();
	}
	public void OnEnable()
	{
		anim.SetBool("isWalking", false );
		yVel = 0;
		transform.position = spawnPoint;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if( !global_script.isPaused )
		{
		transform.Translate ( xVel * moveDir, yVel * .1f, 0f );
		//base.Update ();
		UpdateRaycasts ();
		if( onGround == false )
			yVel--;
		if( yVel <= -6 )
		{
			if( moveDir == -1 )
			{
				if( rotation < 3 )
					rotation++;
				else
					rotation = 0;
				transform.Rotate (0f, 0f, 90f);
			}
			else if( moveDir == 1 )
			{
				if( rotation > 0 )
					rotation--;
				else
					rotation = 3;
				transform.Rotate (0f, 0f, -90f);
			}

			yVel = 0;
			
		}
		if( meowTimer == 0 )
		{
			//audio.PlayOneShot( meowSfx, 1f );
			meowTimer = 120;
		}
		else
		{
			meowTimer--;
		}
	}
	}

	void UpdateRaycasts()
	{
		int mask = 1 << LayerMask.NameToLayer("Ground");
		Ray rayRight = new Ray( new Vector3( transform.position.x + .5f, transform.position.y, transform.position.z), Vector3.down ); //Detect objects below player
		Ray rayLeft = new Ray( new Vector3( transform.position.x - .5f, transform.position.y, transform.position.z), Vector3.down ); //Detect objects below player
		RaycastHit hitInformation;
		float distance = .8f;
		
		bool objectBelow = ( Physics.Raycast ( rayRight, out hitInformation, distance, mask) 
		                    || Physics.Raycast ( rayLeft, out hitInformation, distance, mask ) );
		
		if( objectBelow == true && rotation == 0 ) 
		{
			if( rotation == 0 )
				onGround = true;
			//Readjust player to top of playform
			Vector2 newPos = new Vector2( transform.position.x, hitInformation.point.y + distance );
			transform.position = newPos;
			//Debug.Log ("Hit something");
			yVel = 0;
		}
		else
			onGround = false;
		rayRight = new Ray( new Vector3( transform.position.x + .5f, transform.position.y, transform.position.z), Vector3.up ); //Detect objects below player
		rayLeft = new Ray( new Vector3( transform.position.x - .5f, transform.position.y, transform.position.z), Vector3.up ); //Detect objects below player
		bool objectAbove = ( Physics.Raycast ( rayRight, out hitInformation, distance, mask) 
		                    || Physics.Raycast ( rayLeft, out hitInformation, distance, mask ) );
		
		if( objectAbove == true && rotation == 2) 
		{
			if( rotation == 2 )
				onGround = true;
			//Readjust player to top of playform
			Vector2 newPos = new Vector2( transform.position.x, hitInformation.point.y - distance );
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
		
		if( objectRight == true && rotation == 1 && moveDir == -1 )
		{
			if( rotation == 1 )
			{
				onGround = true;
				yVel = 0;
			}
			Vector2 newPos = new Vector2( hitInformation.point.x - distance, transform.position.y );
			transform.position = newPos;
		}

		else if( objectRight == true && rotation == 1 && moveDir == 1)
		{
			if( rotation == 1 )
			{
				onGround = true;
				yVel = 0;
			}
			Vector2 newPos = new Vector2( hitInformation.point.x - .5f, transform.position.y );
			transform.position = newPos;
		}
		
		rayTop = new Ray( new Vector3( transform.position.x, transform.position.y + distance, transform.position.z ), Vector3.left );
		rayBottom = new Ray( new Vector3( transform.position.x, transform.position.y - .01f, transform.position.z ), Vector3.left );
		
		bool objectLeft = ( Physics.Raycast ( rayTop, out hitInformation, 1f, mask) 
		                   || Physics.Raycast ( rayBottom, out hitInformation, 1f, mask ) );
		
		if( objectLeft == true && rotation == 3 )
		{
			if( rotation == 3 )
			{
				onGround = true;
				yVel = 0;
			}
			Vector2 newPos = new Vector2( hitInformation.point.x + distance, transform.position.y );
			transform.position = newPos;
		}

	}



}
