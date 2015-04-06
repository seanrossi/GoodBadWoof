using UnityEngine;
using System.Collections;

public class blue_rooster : rooster {

	public int numJumps = 0;
	private short jumpCtr = 0;
	// Use this for initialization
	void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		base.FixedUpdate();
		UpdateRaycasts();
	}

	void UpdateRaycasts()
	{
		int mask = 1 << LayerMask.NameToLayer( "Ground" );
		RaycastHit hitInformation;
		Ray rayRight = new Ray( new Vector3( transform.position.x + 2f * moveDir, transform.position.y, transform.position.z), Vector3.down );
		//Ray rayLeft = new Ray( new Vector3( transform.position.x - 2f, transform.position.y, transform.position.z), Vector3.down );
		bool objectBelow = ( Physics.Raycast ( rayRight, out hitInformation, 1f, mask ) );
		if( objectBelow == false ) 
		{
			//onGround = true;
			//Readjust player to top of playform
			//Vector2 newPos = new Vector2( transform.position.x, hitInformation.point.y + distance );
			//transform.position = newPos;
			//Debug.Log ("Hit something");
			if( onGround == true )
			{
				if( jumpCtr < numJumps && numJumps > 0 )
				{
					yVel = 35;
					jumpCtr++;
				}
				else
				{
					jumpCtr = 0;
					moveDir *= -1;
				}
			}
		}
	}
	void OnTriggerEnter2D( Collider2D collision )
	{
		if( collision.gameObject.CompareTag( "Hammer" ) )
		{
			audio.PlayOneShot(dieSfx, .6f );
			isDead = true;
		}
	}

		public void dead()
	{
		transform.gameObject.SetActive(false);
	}
}
