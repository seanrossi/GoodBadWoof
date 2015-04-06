using UnityEngine;
using System.Collections;

public class movingPlatform : MonoBehaviour {

	public int maxHeight, minHeight, maxWidth, minWidth, xDir, yDir;
	private int currentHeight, currentWidth;
	public Vector3 movement;
	public int xVel, yVel;
	private Vector3 spawnPoint;
	// Use this for initialization
	void Awake()
	{
		spawnPoint = transform.position;
	}

	void OnEnable()
	{
		transform.position = spawnPoint;
		currentHeight = 0;
		currentWidth = 0;
		movement.x = 0;
		movement.y = 0;
		movement.z = 0;
	}

	void Start () {
		currentHeight = 0;
		currentWidth = 0;
		movement.x = 0;
		movement.y = 0;
		movement.z = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if( currentHeight < maxHeight && yDir == 1 )
		{
			currentHeight++;
			movement.y = 1f * yVel;
			//movement *= Time.deltaTime;
			//transform.Translate (new Vector3( 0f, movement.y, 0f ) );
			if( currentHeight >= maxHeight )
				yDir = -1;
		}
		else if( currentHeight > minHeight && yDir == -1 )
		{
			currentHeight--;
			movement.y = -1f * yVel;
			//movement *= Time.deltaTime;
			//transform.Translate (new Vector3( 0f, movement.y, 0f ) );
			if( currentHeight <= minHeight )
				yDir = 1;
		}
		if( currentWidth < maxWidth && xDir == 1 )
		{
			currentWidth++;
			movement.x = 1f * xVel;
			//movement *= Time.deltaTime;
			//transform.Translate (new Vector3( movement.x, 0f, 0f ) );
			if( currentWidth >= maxWidth )
				xDir = -1;
		}
		else if( currentWidth > minWidth && xDir == -1 )
		{
			currentWidth--;
			movement.x = -1f * xVel;
			//movement *= Time.deltaTime;
			//transform.Translate (new Vector3( movement.x, 0f, 0f ) );
			if( currentWidth <= minWidth )
				xDir = 1;
		}
		movement *= Time.deltaTime;
		transform.Translate( movement.x, movement.y, 0f );
		Debug.Log ("Platform is moving " + movement.x );
	}
}
