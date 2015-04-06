using UnityEngine;
using System.Collections;

public class recycling : MonoBehaviour {

	public AudioClip crumple;
	public int value;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D( Collider2D collision) 
	{
		if( collision.gameObject.CompareTag("Player") )
		{
			audio.PlayOneShot( crumple, .5f );
			collision.gameObject.GetComponent<woof_logic_controller>().addRecycling(value);
			transform.gameObject.renderer.enabled = false;
			transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			//transform.gameObject.SetActive(false);
			//transform.gameObject.SetActive(false);
		}
	}
}
