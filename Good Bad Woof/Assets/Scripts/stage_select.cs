using UnityEngine;
using System.Collections;

public class stage_select : MonoBehaviour {

	public Texture pigTexture01, pigTexture02, pigTexture03;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if( GUI.Button (new Rect( 700, 100, 80, 80 ), pigTexture01 ) )
		{
			Application.LoadLevel (02);
		}

		if( GUI.Button (new Rect( 500, 400, 80, 80 ), pigTexture02 ) )
		   {
			Application.LoadLevel (03);
		}

		if( GUI.Button (new Rect( 900, 400, 80, 80 ), pigTexture03 ) )
		   {
			Application.LoadLevel (04);
		}
	}
}
