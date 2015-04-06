using UnityEngine;
using System.Collections;

public class spawnTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		for( int i = 0; i < transform.childCount; i++ )
		{
			transform.GetChild(i).transform.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if( collision.gameObject.CompareTag("Player") )
		{
			for( int i = 0; i < transform.childCount; i++ )
			{
				transform.GetChild(i).transform.gameObject.SetActive(true);
				//transform.GetChild(i).transform.gameObject.GetComponent<enemy>().respawn();
			}
		}

	}
	void OnTriggerExit2D(Collider2D collision)
	{
		if( collision.gameObject.CompareTag("Player") )
		{
			for( int i = 0; i < transform.childCount; i++ )
			{
				transform.GetChild(i).transform.gameObject.SetActive(false);
			}
		}
	}
}
