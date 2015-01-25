using UnityEngine;
using System.Collections;

public class MainDisplay : MonoBehaviour {

	public const string SERVER_URL = "http://10.1.47.31:9000/";

	public Texture[] backgroundTexture;

	private int twoThirdMarkWidth;
	private int twoThirdMarkHeight;

	//declaration of Styles
	public GUIStyle whatDoStyle;
	public GUIStyle flavourTextStyle;
	public GUIStyle playerBaseStyle;
	public GUIStyle playerChosenStyle;
	public GUIStyle playerDeadStyle;

	//private int start = 0;  //test value 

	private GUIStyle[] playerStyle;
	private string[] playerName;
	private int[] playerHitpoints;
	private int backgroundIndex=0;

	public int countdownTimer;

	private string sceneTitle;
	private string flavourText;
	private string sceneUpdate;

	private int currentScene;

	private int playerCount;


	void Start(){

		//Geometric Sizes
		twoThirdMarkWidth = Screen.width * 2 / 3;
		twoThirdMarkHeight = Screen.height * 2 / 3;




		//GUI Element Styles
//		whatDoStyle = new GUIStyle();
//		whatDoStyle.alignment = TextAnchor.MiddleCenter;
//		whatDoStyle.fontSize = 48;


		flavourTextStyle = new GUIStyle ();
		flavourTextStyle.alignment = TextAnchor.UpperLeft;
		flavourTextStyle.fontSize = 24;
		flavourTextStyle.wordWrap = true;
		flavourTextStyle.normal.textColor = Color.white;

		playerBaseStyle = new GUIStyle ();
		playerBaseStyle.alignment = TextAnchor.MiddleCenter;
		playerBaseStyle.fontSize = 24;

		playerBaseStyle.normal.textColor = Color.white;

		playerChosenStyle = new GUIStyle ();
		playerChosenStyle.alignment = TextAnchor.MiddleCenter;
		playerChosenStyle.fontSize = 24;
		playerChosenStyle.normal.textColor = Color.green;

		playerDeadStyle = new GUIStyle ();
		playerDeadStyle.alignment = TextAnchor.MiddleCenter;
		playerDeadStyle.fontSize = 24;
		playerDeadStyle.normal.textColor = Color.red;




		playerStyle = new GUIStyle[6];
		changeScenario (-1);

		playerName = new string[6];
		playerName[0] = "Player 1";
		playerName[1] = "Player 2";
		playerName[2] = "Player 3";
		playerName[3] = "Player 4";
		playerName[4] = "Player 5";
		playerName[5] = "Player 6";


		playerHitpoints = new int[6];
		//playerTest = playerBaseStyle;

		StartCoroutine (PollServer ());
		}



	void OnGUI(){



		GUI.Label (new Rect (250, 200, 500, 100), "Main Screen 2");


		//GUI.DrawTexture (new Rect (0, 0, twoThirdMarkWidth, Screen.height), backgroundTexture[backgroundIndex],ScaleMode.ScaleAndCrop);

		//float aspectRatio = backgroundTexture [backgroundIndex].width / backgroundTexture [backgroundIndex].height;
		float textureHeight = Screen.height;
		//float textureWidth = textureHeight * aspectRatio;
		//GUI.DrawTexture (new Rect (0, 0, textureWidth, textureHeight), backgroundTexture[backgroundIndex]);
		GUI.DrawTextureWithTexCoords(new Rect (0, 0, twoThirdMarkWidth, textureHeight), backgroundTexture[backgroundIndex], new Rect(0,0,0.7f,1));

		GUI.Label (new Rect (twoThirdMarkWidth + 10, 10, Screen.width - twoThirdMarkWidth, 50), sceneTitle, whatDoStyle);
		GUI.Label (new Rect (twoThirdMarkWidth + 10, 70, Screen.width - twoThirdMarkWidth -20, twoThirdMarkHeight), sceneUpdate + "\n\n" + flavourText , flavourTextStyle);
		
		GUI.Label (new Rect (twoThirdMarkWidth, twoThirdMarkHeight, (Screen.width - twoThirdMarkWidth)/3, (Screen.height - twoThirdMarkHeight)/2), playerName[0] + "\nHP: " + playerHitpoints[0],	playerStyle[0]);
		GUI.Label (new Rect (twoThirdMarkWidth + (Screen.width - twoThirdMarkWidth)/3, twoThirdMarkHeight, (Screen.width - twoThirdMarkWidth)/3, (Screen.height - twoThirdMarkHeight)/2), playerName[1] + "\nHP: " + playerHitpoints[1],playerStyle[1]);
		GUI.Label (new Rect (twoThirdMarkWidth +((Screen.width - twoThirdMarkWidth)/3)*2, twoThirdMarkHeight, (Screen.width - twoThirdMarkWidth)/3, (Screen.height - twoThirdMarkHeight)/2), playerName[2] + "\nHP: " + playerHitpoints[2], playerStyle[2]);
		GUI.Label (new Rect (twoThirdMarkWidth, twoThirdMarkHeight +(Screen.height - twoThirdMarkHeight)/2, (Screen.width - twoThirdMarkWidth)/3, (Screen.height - twoThirdMarkHeight)/2), playerName[3] + "\nHP: " + playerHitpoints[3], playerStyle[3]);
		GUI.Label (new Rect (twoThirdMarkWidth + (Screen.width - twoThirdMarkWidth)/3, twoThirdMarkHeight +(Screen.height - twoThirdMarkHeight)/2, (Screen.width - twoThirdMarkWidth)/3, (Screen.height - twoThirdMarkHeight)/2), playerName[4] + "\nHP: " + playerHitpoints[4], playerStyle[4]);
		GUI.Label (new Rect (twoThirdMarkWidth +((Screen.width - twoThirdMarkWidth)/3)*2, twoThirdMarkHeight+(Screen.height - twoThirdMarkHeight)/2, (Screen.width - twoThirdMarkWidth)/3, (Screen.height - twoThirdMarkHeight)/2), playerName[5] + "\nHP: " + playerHitpoints[5],playerStyle[5]);
		
		GUI.Label (new Rect (twoThirdMarkWidth + 5, twoThirdMarkHeight -50, Screen.width - twoThirdMarkWidth - 10, 50), "WHAT DO WE DO NOW?", whatDoStyle);


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
				playerCount = 0;

				JSONObject obj = new JSONObject(pollWWW.text);
				UtilityMethods.ReadJSONString(obj, CallAPI, "");
				if (playerCount < 6)
				{
					//terminate
					Application.LoadLevel (0);
				}
			}

			yield return new WaitForSeconds(1);

		}
		Debug.Log ("exited loop");
	}

	public void CallAPI(string key, object item, int indexVal)
	{
		Debug.Log (key + " " + item.ToString());
		switch(key)
		{
		case "Countdown" :
			countdownTimer =  System.Convert.ToInt32(item);
				break;
		case "TextUpdate" :
			sceneUpdate =  item.ToString();
			break;
		case "TextNewScenario" :
			flavourText = item.ToString();
			break;
		case "PlayerNames" :
			playerName[indexVal] = item.ToString();
			playerCount++;
			break;
		case "CharacterHp" :
			if (System.Convert.ToInt32(item) <= 0){
				playerStyle[indexVal] = playerDeadStyle;
			}
			playerHitpoints[indexVal] = System.Convert.ToInt32(item);

			break;
		case "CharacterResponded" :
			if (item.ToString() == "True"){
				playerStyle[indexVal] = playerChosenStyle;
			}


			break;
		case "Title" :
			sceneTitle = item.ToString();
			break;
		case "ImageIndex" :
			backgroundIndex = System.Convert.ToInt32(item);

			if (currentScene != backgroundIndex)
			{
				changeScenario(backgroundIndex);
			}

			break;
		}
	}
	private void changeScenario(int newScene)
	{
		playerStyle[0] = playerBaseStyle;
		playerStyle[1] = playerBaseStyle;
		playerStyle[2] = playerBaseStyle;
		playerStyle[3] = playerBaseStyle;
		playerStyle[4] = playerBaseStyle;
		playerStyle[5] = playerBaseStyle;
		if (newScene != -1)
		{
			currentScene = newScene;
		}
	}

	
}
