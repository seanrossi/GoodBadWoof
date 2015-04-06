using UnityEngine;
using System.Collections;

public class checkPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter( Collider collision )
	{
		if( collision.CompareTag( "Player" ) )
		{
		   collision.gameObject.GetComponent<woof_logic_controller>().setSpawnPoint( transform.position );
			Debug.Log ("setSpawn called");
		}
	}
}
