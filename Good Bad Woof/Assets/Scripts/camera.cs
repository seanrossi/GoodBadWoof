using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

	Transform player;	//Player Transform to follow
	Vector3 newCamPos, targetCamPos;
	bool isShifting = false;
	//public AudioClip levelMusic;
	public float xMin = 0, xMax = 100, yMin = -15, yMax = 30;
	//float horzExtent = (Camera.main.orthographicSize * Screen.width / Screen.height ) / 2;
	//float vertExtent = Camera.main.orthographicSize;
	// Use this for initialization
	public void Start () {
		//audio.Play ( 
		isShifting = false;
		player = GameObject.Find("Player").transform;
		if(player.position.x - (Camera.main.orthographicSize * Screen.width / Screen.height ) / 2 > xMin && player.position.x + Camera.main.orthographicSize * Screen.width / Screen.height  / 2  < xMax )
			newCamPos.x = player.position.x;
		else
			newCamPos.x = xMin + (Camera.main.orthographicSize * Screen.width / Screen.height ) / 2;
		//if( player.position.y  + vertExtent < yMax && player.position.y - vertExtent > yMin )
			//newCamPos.y = player.position.y;
		//else 
		//{
			//newCamPos.y = yMin;
			//Debug.Log ("yMin: " + yMin.ToString() );
			//Debug.Log ("Camera.orth.size: " + (Camera.main.orthographicSize / 2).ToString() );
			//Debug.Log( newCamPos.y.ToString() );
		//}
		newCamPos.y = transform.position.y;
		newCamPos.z = transform.position.z;
		transform.position = newCamPos;
	}
	
	// Update is called once per frame
	void Update () {
		if( isShifting == true )
		{
			if( transform.position.x < targetCamPos.x )
				newCamPos.x += 1f;
			else if( transform.position.x > targetCamPos.x )
				newCamPos.x -= 1f;
			if( transform.position.y < targetCamPos.y )
				newCamPos.y += 1f;
			else if( transform.position.y > targetCamPos.y )
				newCamPos .y-= 1f;
			if( newCamPos.x == targetCamPos.x && newCamPos.y == targetCamPos.y )
			{
				global_script.isPaused = false;
				isShifting = false;
			}
		}
		else
		{
		/*if( player.position.y > yMax )
		{
			newCamPos.y = transform.position.y + Camera.main.orthographicSize*2;
			yMin = yMax;
			yMax += Camera.main.orthographicSize*2;
		}
		else if( player.position.y < yMin )
		{
			newCamPos.y = transform.position.y - Camera.main.orthographicSize*2;
			yMax = yMin;
			yMin -= Camera.main.orthographicSize*2;
		}*/
		if(player.position.x - (Camera.main.orthographicSize * Screen.width / Screen.height ) / 2 > xMin 
			   && player.position.x + (Camera.main.orthographicSize * Screen.width / Screen.height ) / 2 < xMax )
			newCamPos.x = player.position.x;
			else if( player.position.x - (Camera.main.orthographicSize * Screen.width / Screen.height ) / 2 < xMin )
				newCamPos.x = xMin + (Camera.main.orthographicSize * Screen.width / Screen.height ) / 2;
			else if( player.position.x + (Camera.main.orthographicSize * Screen.width / Screen.height ) / 2 > xMax )
				newCamPos.x = xMax - (Camera.main.orthographicSize * Screen.width / Screen.height ) / 2;
		//if( player.position.y  + vertExtent < yMax && player.position.y - vertExtent > yMin )
			//newCamPos.y = player.position.y;
		//else 
			//newCamPos.y = yMin + (Camera.main.orthographicSize);

		
		}
		transform.position = newCamPos;
	}

	public void shiftCamera( GameObject newSection )
	{
		//global_script.isPaused = true;
		//targetCamPos.y = newSection.transform.position.y;
		newCamPos.y = newSection.transform.position.y;
		Debug.Log ("shoftCamera called");
		//yMin = newSection.y - ;
		//yMax += Camera.main.orthographicSize*2;
		xMin = newSection.transform.position.x - newSection.GetComponent<BoxCollider2D>().size.x/2 + (Camera.main.orthographicSize * Screen.width / Screen.height ) / 2;
		xMax = newSection.transform.position.x + newSection.GetComponent<BoxCollider2D>().size.x/2;
		//targetCamPos.x = xMin;
		//targetCamPos.x = newSection.transform.position.x;
		//targetCamPos.z = transform.position.z;
		//transform.position = targetCamPos;
		newCamPos.x = xMin;
		newCamPos.z = transform.position.z;
		//isShifting = true;
		transform.position = newCamPos;
	}
}
