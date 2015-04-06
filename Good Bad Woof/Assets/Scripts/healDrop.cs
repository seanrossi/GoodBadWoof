using UnityEngine;
using System.Collections;

public class healDrop : MonoBehaviour {

	public int hpValue = 2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D collision )
	{
		if( collision.gameObject.CompareTag("Player") )
			collision.gameObject.GetComponent<woof_logic_controller>().restoreHp( hpValue );
		transform.gameObject.SetActive(false);
	}
}
