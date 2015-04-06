using UnityEngine;
using System.Collections;

public class title : MonoBehaviour {

	public Texture startButton, optionsButton, exitButton;
	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate()
	{
		Debug.Log ("Fixed Update Time: " + Time.deltaTime );
	}
	// Update is called once per frame
	void Update () {
		Debug.Log ("Update Time: " + Time.deltaTime );
	}

	void OnGUI()
	{
		if( GUI.Button ( new Rect( 600, 100, 200, 60 ), startButton ) )
		{
			Application.LoadLevel ("Straw_Sub_Select");
		}
		if( GUI.Button ( new Rect( 600, 200, 200, 60 ), optionsButton ) )
		{
			Debug.Log ("Start button pressed");
		}
		if( GUI.Button ( new Rect( 600, 300, 200, 60 ), exitButton ) )
		{
			Application.Quit();
			Debug.Log ("Start button pressed");
		}
	}
}
