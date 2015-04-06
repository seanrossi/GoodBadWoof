using UnityEngine;
using System.Collections;

public class projectile_arc : projectile {
	protected int yVel = 0;
	// Use this for initialization
	void Start () {
		//yVel = 10;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if( !global_script.isPaused )
		{
		base.FixedUpdate();
		transform.Translate( 0f, yVel * .1f, 0f );
		if( yVel > -6)
			yVel--;
		}
	}

	public void setAtkH( int h )
	{
		yVel = h;
	}
}
