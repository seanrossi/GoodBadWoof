using UnityEngine;
using System.Collections;

public class subSelect : MonoBehaviour {

	public string stage01, stage02, stage03, stage04;
	string complete0 = "";
	string complete1 = "";
	string complete2 = "";
	string complete3 = "";

	string[] undefeated = new string[4]{ "", "", "", ""};
	string[] untouched = new string[4]{ "", "", "", ""};

	string[] displayString = new string[4]{ "01", "02", "03", "04" };
	// Use this for initialization
	void Start () {

		if( global_script.recyclingChart.ContainsKey(stage01) )
			complete0 = global_script.recyclingChart[stage01].ToString();
		if( global_script.recyclingChart.ContainsKey(stage02) )
			complete1 = global_script.recyclingChart[stage02].ToString();
		if( global_script.recyclingChart.ContainsKey(stage03) )
			complete2 = global_script.recyclingChart[stage03].ToString();
		if( global_script.recyclingChart.ContainsKey(stage04) )
			complete3 = global_script.recyclingChart[stage04].ToString();
		/*for( int i = 0; i < undefeated.Length; i++ )
			if( global_script.undefeatedChart.ContainsKey(i) )
				complete0 = global_script.undefeatedChart[i].ToString();*/
		if( global_script.undefeatedChart.ContainsKey(stage01) )
			undefeated[0] = global_script.undefeatedChart[stage01].ToString();
		if( global_script.undefeatedChart.ContainsKey(stage02) )
			undefeated[1] = global_script.undefeatedChart[stage02].ToString();
		if( global_script.undefeatedChart.ContainsKey(stage03) )
			undefeated[2] = global_script.undefeatedChart[stage03].ToString();
		if( global_script.undefeatedChart.ContainsKey(stage04) )
			undefeated[3] = global_script.undefeatedChart[stage04].ToString();

		if( global_script.untouchedChart.ContainsKey(stage01) )
			untouched[0] = global_script.untouchedChart[stage01].ToString();
		if( global_script.untouchedChart.ContainsKey(stage02) )
			untouched[1] = global_script.untouchedChart[stage02].ToString();
		if( global_script.untouchedChart.ContainsKey(stage03) )
			untouched[2] = global_script.untouchedChart[stage03].ToString();
		if( global_script.untouchedChart.ContainsKey(stage04) )
			untouched[3] = global_script.untouchedChart[stage04].ToString();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{

		if(GUI.Button ( new Rect(Screen.width/2 - 200, Screen.height/2, 100, 50 ), "01 " + complete0 + " " + undefeated[0] + " " + untouched[0]) )
		{
			global_script.levelName = stage01;
			Application.LoadLevel(stage01);
		}
		if(GUI.Button ( new Rect(Screen.width/2 - 100, Screen.height/2, 100, 50 ), "02 " + complete1 + " " + undefeated[1] + " " + untouched[1]) )
		{
			global_script.levelName = stage02;
			Application.LoadLevel(stage02);
		}
		if(GUI.Button ( new Rect( Screen.width/2 + 100, Screen.height/2, 100, 50 ), "03 " + complete2 + " " + undefeated[2] + " " + untouched[2]) )
		{
			global_script.levelName = stage03;
			Application.LoadLevel(stage03);
		}
		if(GUI.Button ( new Rect( Screen.width/2 + 200, Screen.height/2, 100, 50 ), "04 " + complete3 + " " + undefeated[3] + " " + untouched[3]) )
		{
			global_script.levelName = stage04;
			Application.LoadLevel(stage04);
		}
	}
}
