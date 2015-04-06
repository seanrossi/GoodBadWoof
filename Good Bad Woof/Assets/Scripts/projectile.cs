using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {

	protected int direction = 0;	// -1 for left, 0 for standstill, 1 for right
	protected int timer = 0;	//Determines if the object has travelled too far and must be destroyed
	// Use this for initialization
	void Start () {

	}

	public void spawn( Vector3 spawnPoint, int dir )
	{
		transform.position = spawnPoint;
		direction = dir;
		transform.gameObject.SetActive(true);
		timer = 0;

	}
	public void setDirection( int dir )
	{
		direction = dir;
	}

	// Update is called once per frame
	protected void FixedUpdate () {
		transform.Translate( direction * .3f, 0f, 0f );
		timer++;
		if( timer >= 60 )
			transform.gameObject.SetActive( false );
			//Destroy( transform.gameObject );
	}

	void OnTriggerEnter( Collider collision )
	{
		if( collision.gameObject.CompareTag( "Player" ) )
			transform.gameObject.SetActive(false);
		if( collision.gameObject.CompareTag( "Wind") )
		{
			if( collision.gameObject.transform.position.x < transform.position.x )
				direction = 1;
			else if( collision.gameObject.transform.position.x > transform.position.x )
				direction = -1;
		}
	}

	
}
