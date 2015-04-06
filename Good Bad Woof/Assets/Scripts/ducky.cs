using UnityEngine;
using System.Collections;

public class ducky : enemy {

	GameObject go;
	public string projectileName = "Egg_Object_Arc";
	public float xRange = 10, xPos = 5; //Defines the range that the duck will walk back and forth
	short dir = -1;
	public int atkDir = 1;
	public int atkH = 10;
	int eggTimer;	//Used to determine when to throw egg
	bool isEggActive;
	// Use this for initialization

	void OnEnable()
	{
		xPos = 5;
		dir = -1;
		base.OnEnable();
	}

	void setDead( bool dead )
	{
		anim.SetBool ( "isDead", dead );

	}

	void Start () {
		go = (GameObject)Instantiate( Resources.Load ( projectileName ), transform.renderer.bounds.center, transform.rotation );
		go.transform.position = transform.renderer.bounds.center;
		eggTimer = 0;
		isEggActive = false;
		moveDir = -1;
		base.Start ();
		anim.SetBool( "isWalking", true );
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if( !global_script.isPaused )
		{
		base.FixedUpdate ();
		if( isDead == false )
		{	
			if( xPos > 0 && moveDir == -1 )
			{
				transform.Translate ( new Vector2( moveDir * .1f, 0f ) );
				xPos += moveDir * .1f;
				if( xPos <= 0 )
					moveDir = 1;
			}
			if( xPos < xRange && moveDir == 1 )
			{
				transform.Translate ( new Vector2( moveDir * .1f, 0f ) );
				xPos += moveDir * .1f;
				if( xPos >= xRange )
					moveDir = -1;
			}

			//Determine if egg should be launched
			if( isEggActive == false )
			{
				//GameObject go = (GameObject)Instantiate( Resources.Load ( "Egg_Object_Arc" ), transform.renderer.bounds.center, transform.rotation );
				go.GetComponent<projectile>().spawn(transform.renderer.bounds.center, atkDir );
				go.GetComponent<projectile>().setDirection( atkDir );
				go.GetComponent<projectile_arc>().setAtkH( atkH );
				isEggActive = true;
				eggTimer = 120;	//Counts down to 0 before creating a new egg
			}

			if( isEggActive == true )
			{
				eggTimer--;
				if( eggTimer == 0 )
					isEggActive = false;
			}
		//transform.localScale =  new Vector3( moveDir, 1, 1 );
		}
		//Destroy enemy if dying animation is done
		if( isDead == true && anim.GetCurrentAnimatorStateInfo(0).IsName( "dead" ) )
				transform.gameObject.SetActive(false);
			//Destroy( transform.gameObject );
	}
	}
	override
	protected void OnTriggerEnter2D( Collider2D collision )
	{
		if( collision.gameObject.CompareTag( "Hammer" ) )
		{
			audio.PlayOneShot( dieSfx, 1f );
			isDead = true;
			anim.SetBool( "isDead", true );
		}
		if( collision.gameObject.CompareTag( "Wind" ) )
			transform.Translate ( -2f * moveDir, 0f, 0f );
	}

	public void PlayDeathClip()
	{
		audio.PlayOneShot( dieSfx, 1f );
	}

}
