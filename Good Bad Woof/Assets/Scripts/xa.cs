using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
//using Input = Moga_Input;
 // Use this for initialization
#endif
// static class courtesy of Michael Todd http://twitter.com/thegamedesigner
// This script is part of the tutorial series "Making a 2D game with Unity3D using only free tools"
// http://www.rocket5studios.com/tutorials/make-a-2d-game-in-unity3d-using-only-free-tools-part-1

public class xa : MonoBehaviour {

	//public static Scoring sc; // scoring will be added in an upcomming tutorial
	//public static Weapon weapon;
#if UNITY_ANDROID
	//Moga_ControllerManager mogaControllerManager;
#endif
	public static float orthSize;
	public static float orthSizeX;
	public static float orthSizeY;
	public static float camRatio;

	public static bool blockedRight = false;
	public static bool blockedLeft = false;
	public static bool blockedUp = false;
	public static bool blockedDown = false;

	public static float playerHitboxX = 0.225f; // player x = 0.45
	public static float playerHitboxY = 0.5f; // 0.5 is correct for ladders while player actual y = 0.6

	public static bool isLeft;
	public static bool isRight;
	public static bool isUp;
	public static bool isDown;
	public static bool isShoot;
	public static bool isJump;
	public static bool isAttack;
	public static bool isSlide;

	public static bool alive;
	public static bool onLadder;
	public static bool onRope; 
	public static bool falling;
	public static bool shooting;
	public static bool jumping;
	public static bool attacking;
	public static bool blocking;
    public static bool grappling; 
	public static bool stunned;
	public static bool lifting;
	public static bool isWeb;
	public static bool isMenu;
	public static bool isBite;
	
	public static int facingDir = 1; // 1 = left, 2 = right, 3 = up, 4 = down
	public enum anim { None, Grappling, Attack, AttackLeft, WalkLeft, WalkRight, RopeLeft, RopeRight, Climb, ClimbStop, StandLeft, StandRight, HangLeft, HangRight, FallLeft, FallRight , ShootLeft, ShootRight }

	public static Vector3 glx;

	public void Start()
	{
		isMenu = false;
		Application.targetFrameRate = 60;
		//sc = (Scoring)(this.gameObject.GetComponent("Scoring")); // scoring will be added in an upcomming tutorial

		// gather information from the camera to find the screen size
#if UNITY_ANDROID
		/*Input.RegisterMogaController();
		mogaControllerObject = GameObject.Find(“MogaControllerManager”); 
		if (mogaControllerObject != null) 
		{ // Check to see if the Moga_ControllerManager Script is attached. 
			mogaControllerManager = mogaControllerObject.GetComponent<Moga_ControllerManager>(); 
		} 
	}*/
#endif
		xa.camRatio = 1.333f; // 4:3 is 1.333f (800x600) 
		xa.orthSize = Camera.mainCamera.camera.orthographicSize;
		xa.orthSizeX = xa.orthSize * xa.camRatio;
	}

	public void Awake()
	{
		//DontDestroyOnLoad( this.gameObject );
	}
	
	public void Update() 
	{
		// these are false unless one of keys is pressed
		isLeft = false;
		isRight = false;
		isUp = false;
		isDown = false;
		isShoot = false;
		isJump = false;
		isAttack = false; 
		isWeb = false;
		isBite = false;
		isSlide = false;
		//Android Input
#if UNITY_ANDROID
		if(Input.GetKey(KeyCode.Joystick1Button6) )
			isUp = true;
		if(Input.GetKey(KeyCode.Joystick1Button7) )
			isDown = true;
		if(Input.GetKey(KeyCode.Joystick1Button4) )
			isLeft = true;
		if(Input.GetKey(KeyCode.Joystick1Button5) )
			isRight = true;
		if(Input.GetKey(KeyCode.Joystick1Button15) )
			isAttack = true;
		if(Input.GetKey(KeyCode.Joystick1Button14) )
			isJump = true;

#endif
		
		// keyboard input
		float value = Input.GetAxis("Horizontal");
		if( value < 0 )
		  isLeft = true;
		if( value > 0 )
		  isRight = true;
		
		float v_value = Input.GetAxis("Vertical");
		if( v_value < -.1 )
		  isDown = true;
		if( v_value > 0 )
		  isUp = true;

		/*if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
		{ isLeft = true; }
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
		{ isRight = true; }
		*/
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
		{ isUp = true; }
		//if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
		//if( Input.GetKeyDown ( KeyCode.DownArrow ) )
		//{ isDown = true; }

		if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.E) 
		  || Input.GetKey( "joystick button 0" )) 
		{ isJump = true; }
		if( Input.GetKeyDown (KeyCode.Space) || Input.GetKey(KeyCode.E) 
		   || Input.GetKey( "joystick button 0" ))
			isSlide = true;


		if( Input.GetKey(KeyCode.F) || Input.GetKey("joystick button 2") && attacking == false )
		{
			isAttack = true;
		}
		if( Input.GetKeyDown(KeyCode.G) || Input.GetKey("joystick button 1") )
		{
			isBite = true;
		}
		if( Input.GetKey(KeyCode.R) )
		{
			isWeb = true;
		}
		if( Input.GetKeyDown (KeyCode.Escape ) )
		{
			if( isMenu == false )
				isMenu = true;
			else if( isMenu == true )
				isMenu = false;
			if( global_script.isPaused == false )
				global_script.isPaused = true;
			else
				global_script.isPaused = false;
		}
		int touchCount = Input.touchCount;
		if( touchCount > 0 )
		{
			Touch touch = Input.GetTouch(0);
			TouchPhase tp = touch.phase;
			switch (tp)
			{
				case TouchPhase.Began:
				Debug.Log ("Touch began here");
				break;
			case TouchPhase.Ended:
				Debug.Log ("Touch Ended here");
				break;
			}
		}

		//if( Input.GetKeyUp( KeyCode.DownArrow ) )
		//	isDown = false;
		/*if( Input.GetKey(KeyCode.P) )
		{
			if( Globals.isPaused == false )
				Globals.isPaused = true;
			else 
				Globals.isPaused = false;
			if( Globals.isMenu == false )
				Globals.isMenu = true;
			else 
				Globals.isMenu = false;
		}*/
		}
		
}
