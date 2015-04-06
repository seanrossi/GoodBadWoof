using UnityEngine;
using System.Collections;

public class levelEnd : MonoBehaviour {

	public string destination;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if( collision.gameObject.CompareTag("Player") )
		{
			collision.gameObject.GetComponent<woof_logic_controller>().endLevel();
			if( collision.gameObject.GetComponent<woof_logic_controller>().isRecyclingComplete() == true )
				global_script.recyclingChart[global_script.levelName] = 1;
			else
			{
				if( global_script.recyclingChart.ContainsKey(global_script.levelName) )
				{
					if (global_script.recyclingChart[global_script.levelName].ToString() != "1" )
						global_script.recyclingChart[global_script.levelName] = 0;
				}
				else 
					global_script.recyclingChart[global_script.levelName] = 0;
			}
			if( collision.gameObject.GetComponent<woof_logic_controller>().undefeated() == true )
				global_script.undefeatedChart[global_script.levelName] = 1;
			else
			{
				if( global_script.undefeatedChart.ContainsKey(global_script.levelName) )
				{
					if (global_script.undefeatedChart[global_script.levelName].ToString() != "1" )
						global_script.undefeatedChart[global_script.levelName] = 0;
				}
				else 
					global_script.undefeatedChart[global_script.levelName] = 0;
			}
			if( collision.gameObject.GetComponent<woof_logic_controller>().untouched() == true )
				global_script.untouchedChart[global_script.levelName] = 1;
			else
			{
				if( global_script.untouchedChart.ContainsKey(global_script.levelName) )
				{
					if (global_script.untouchedChart[global_script.levelName].ToString() != "1" )
						global_script.untouchedChart[global_script.levelName] = 0;
				}
				else 
					global_script.untouchedChart[global_script.levelName] = 0;
			}
			//Application.LoadLevel (destination);
		}
	}
}
