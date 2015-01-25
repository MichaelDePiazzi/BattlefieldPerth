using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public const string SERVER_URL = "http://10.1.47.31:9000/";
	
	public int playerRequired;
	private int currentPlayers = 0;

	private int centreScreenX;
	private int centreScreenY;

	//public GUISkin skin;
	private GUIStyle gameTitle;
	private GUIStyle connectionInfo;
	private GUIStyle connectHow;


	public Texture backgroundTexture;

	private string networkIP;
	private string[] networkOctet;


	private string networkName;

	private GUIContent connectionInfoLabel;

	private string[] playerName;


	public void Start()
	{
		playerName = new string[6];


		StartCoroutine (PollServer ());
	}



	private void OnGUI(){


		if (currentPlayers == playerRequired)
		{
			//Load next scene
			StartCoroutine( BeginGame() );

		}



		centreScreenX = Screen.width / 2;
		centreScreenY = Screen.height / 2;


		//GUI.skin = skin;

		gameTitle = new GUIStyle ();
		connectionInfo = new GUIStyle ();
		connectHow = new GUIStyle ();

		gameTitle.normal.textColor = Color.grey;
		connectionInfo.normal.textColor = Color.red;
		connectHow.normal.textColor = Color.white;

		gameTitle.fontSize = 48;
		gameTitle.alignment = TextAnchor.MiddleCenter;

		connectionInfo.fontSize = 32;
		connectionInfo.alignment = TextAnchor.MiddleCenter;

		connectHow.fontSize = 32;
		connectHow.alignment = TextAnchor.MiddleCenter;


		networkIP = Network.player.externalIP;

		networkOctet = networkIP.Split ("." [0]);

		networkName = System.Environment.MachineName;

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);



		//GUI.Label (new Rect (centreScreenX - 250, centreScreenY - 200, 500, 100), "Project Choice: Battlefield Perth", gameTitle);
		GUI.Label (new Rect (centreScreenX - 250, centreScreenY + 10, 500, 100), "Numbers connected: " + currentPlayers + " of " + playerRequired, connectionInfo);
		GUI.Label (new Rect (centreScreenX - 250, centreScreenY + 120, 500, 100), "Join using the server name: " + networkName, connectHow);
		GUI.Label (new Rect (0, centreScreenY - 50, 400, 80), "Players Connected", connectionInfo);
		GUI.Label (new Rect (10, centreScreenY + 50, 400, 500), DisplayPlayers());



		if (GUI.Button (new Rect (Screen.width - 130, Screen.height - 60, 120, 50), "EXIT")) {
			//currentPlayers++;
				Application.Quit();
		}
		if (GUI.Button (new Rect (10, Screen.height - 60, 120, 50), "RESET")) {
			//call method to reset game play
			currentPlayers = 0;

		}
	}
	private string DisplayPlayers()
	{
		string outVal = "";
		int i =0;
		while ( i < playerName.Length && playerName[i] != null) {

			outVal = outVal + "\n" + playerName[i];
			i++;
				}
		return outVal;
	}
	private IEnumerator BeginGame()
	{
		yield return new WaitForSeconds(3); 

		Application.LoadLevel (1);
	}
	private IEnumerator PollServer()
	{
		while(this.gameObject)
		{
			// do stuff
			
			Debug.Log("Polling...");
			
			WWW pollWWW = new WWW(SERVER_URL+"api/requestupdatedisplay");
			yield return pollWWW;
			
			if(pollWWW.error != null )
			{
				Debug.LogError("Error While Polling: " + pollWWW.error);
			}
			else
			{
				Debug.Log ("Poll Successful: " + pollWWW.text );
				
				JSONObject obj = new JSONObject(pollWWW.text);
				UtilityMethods.ReadJSONString(obj, CallAPI, "");
			}
			
			yield return new WaitForSeconds(1);
			
		}
	}


	public void CallAPI(string key, object item, int indexVal)
	{
		Debug.Log (key + " " + item.ToString());
		switch (key) {

		case "PlayerNames":
				playerName [indexVal] = item.ToString ();
			if (currentPlayers != null)
			{
				if (playerName[currentPlayers] != null)
				{
					currentPlayers++;
				}
				break;
			}
		}

	}
}
