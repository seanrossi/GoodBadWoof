using UnityEngine;
using System.Collections;

public class deactivateTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter( Collider collision )
	{
		if( collision.gameObject.CompareTag("Player") )
			transform.GetChild(0).transform.gameObject.SetActive(false);
	}
}
