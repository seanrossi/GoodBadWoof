using UnityEngine;
using System.Collections;

public class global_script : MonoBehaviour {

	public static string levelName;
	public static bool isPaused;
	public static Hashtable recyclingChart = new Hashtable();	//Hashtable to determine if all recycling is found in level
	public static Hashtable undefeatedChart = new Hashtable();
	public static Hashtable untouchedChart = new Hashtable();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static int setRecycling()
	{
		GameObject[] recyclingObjects = GameObject.FindGameObjectsWithTag("Recycling");
		return recyclingObjects.Length;
	}

	public static void resetRecycling()
	{
		GameObject[] recyclingObjects = GameObject.FindGameObjectsWithTag("Recycling");
		Debug.Log("Found " + recyclingObjects.Length.ToString() + " recycling objects");
		for( int i = 0; i < recyclingObjects.Length; i++ )
		{
			recyclingObjects[i].GetComponent<BoxCollider2D>().enabled = true;
			recyclingObjects[i].GetComponent<Renderer>().enabled = true;
		}
		//GameObject.F
	}

	public void resetEnemeis()
	{

	}
	
}
