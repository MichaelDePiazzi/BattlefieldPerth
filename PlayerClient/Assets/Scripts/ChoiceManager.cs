using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net.NetworkInformation;
using System.Xml;
using SimpleJSON;

/// <summary>
/// Grabs choice text from the server, and places them on the choice buttons.
/// 
/// Sends back the choice to the server when the player has locked in their
/// answer, and the time has ran out.
/// </summary>
public class ChoiceManager : MonoBehaviour 
{

	// --------------------------------------------------------
	// INSTANCING
	// --------------------------------------------------------

	private static ChoiceManager _instance;

	public static ChoiceManager Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType<ChoiceManager>();
			}
			return _instance;
		}
		private set{}
	}

	private void Awake()
	{
		if(!_instance)
		{
			_instance = this;
		}
		else if(_instance != this)
		{
			DestroyObject(this.gameObject);
		}
	}





	// --------------------------------------------------------
	// CONSTANTS
	// --------------------------------------------------------

	/// <summary>
	/// The amount of time in seconds to wait
	/// between polls to the server.
	/// </summary>
	public const float POLL_DELAY = 1;

	/// <summary>
	/// The base URL the server is at.
	/// </summary>
	public const string SERVER_URL = "http://10.1.47.31:9000/";
	//public const string SERVER_URL = "http://nemesis:9000/";
	//public const string SERVER_URL = "http://localhost:9000/";
	//public const string SERVER_URL = "http://10.1.47.127:9000/"; // me
	//public const string SERVER_URL = "http://10.1.47.172:9000/";

	public enum GAME_STATE{LoggingIn, WaitingForStart, GettingInfo,MakingChoice,WaitingForOthers, Dead};




	// --------------------------------------------------------
	// PUBLIC VARIABLES
	// --------------------------------------------------------

	/// <summary>
	/// The loading text that will give a message to the player while loading.
	/// </summary>
	public Text loadingText;

	/// <summary>
	/// The title of the scene
	/// </summary>
	public Text title;

	/// <summary>
	/// The text UI elements of the 4 choice buttons.
	/// </summary>
	public Text[] choiceTextList;


	/// <summary>
	/// Used to send debug messages on screen.
	/// </summary>
	public Text debugText;





	// --------------------------------------------------------
	// PRIVATE VARIABLES
	// --------------------------------------------------------

	/// <summary>
	/// The current choice the player has chosen.
	/// </summary>
	private int choiceSelected = -1;



	/// <summary>
	/// Set to true whenever data is being loaded from the server
	/// </summary>
	private bool loading = false;

	/// <summary>
	/// Determines what is happening within the game.
	/// </summary>
	private GAME_STATE gameState = GAME_STATE.LoggingIn;

	private int playerNumber = 0;

	/// <summary>
	/// The current scenario the player is on.
	/// </summary>
	private string currentScenario;








	// --------------------------------------------------------
	// METHODS
	// --------------------------------------------------------

	private void Start()
	{
		StartCoroutine (RequestUpdateClient ());
	}

	/// <summary>
	/// Called as soon as the player logs into the game
	/// </summary>
	public void LoggedIn(int playerNumber)
	{
		if( gameState == GAME_STATE.LoggingIn )
		{
			gameState = GAME_STATE.WaitingForStart;

			// set the player number
			this.playerNumber = playerNumber;
		}
	}

	/// <summary>
	/// Changes the choice the player will choose at the end of the round.
	/// </summary>
	public void ChoiceSelected(int choiceNumber)
	{
		// if the player is currently making a choice
		if( gameState == GAME_STATE.MakingChoice )
		{
			// show loading screen while waiting
			ScreenUIManager.Instance.SwitchToScreen("LoadingScreen");
			loadingText.text = "Sending Choice...";

			// Send the choice to the server
			StartCoroutine (MakeChoice (choiceNumber));
		}
	}

	/// <summary>
	/// Sends the player's choice to the server.	
	/// </summary>
	private IEnumerator MakeChoice(int choiceNumber)
	{
		string url = SERVER_URL + "api/makechoice/?playerNumber=" + playerNumber + "&choiceNumber=" + choiceNumber;

		// send the choice to the server
		Debug.Log ("Making choice: " + url);
		WWW choiceWWW = new WWW (url);
		yield return choiceWWW;

		// if there was an error
		if( choiceWWW.error != null )
		{
			Debug.LogError( "Make Choice Error: " + choiceWWW.error);

			// go back to the choice screen
			ScreenUIManager.Instance.SwitchToScreen("ChoiceScreen");
		}
		// otherwise
		else
		{
			// the choice was sent successfully
			Debug.Log ( "Choice Sent" );
			this.choiceSelected = choiceNumber;

			// if the player was making a choice
			if( gameState == GAME_STATE.MakingChoice )
			{
				// set the state to waiting for other players
				gameState = GAME_STATE.WaitingForOthers;

				// change the loading text
				ScreenUIManager.Instance.SwitchToScreen("LoadingScreen");
				loadingText.text = "Waiting For\nOther Players...";
			}
		}
	}

	/// <summary>
	/// Asks the server to give this client information once per second	
	/// </summary>
	private IEnumerator RequestUpdateClient()
	{
		while(this.gameObject)
		{
			// only poll while the player is logged in
			if( gameState != GAME_STATE.LoggingIn )
			{
				// poll server
				//Debug.Log ("Polling..." + SERVER_URL + "api/request_update_client/" + playerNumber);


				//WWWForm newForm = new WWWForm();
				//newForm.AddField("player_number", playerNumber);

				WWW pollWWW = new WWW(SERVER_URL + "api/requestupdateclient/" + playerNumber );
				yield return pollWWW;
				
				// retrieve results
				if( pollWWW.error != null )
				{
					Debug.LogError("Error While Polling: " + pollWWW.error);
					debugText.text = "Error While Polling: " + pollWWW.error;

					// go back to the login screen
					ScreenUIManager.Instance.SwitchToScreen("LoginScreen");

					// set status back to loggin in
					gameState = GAME_STATE.LoggingIn;

					// show notification to player
					NotificationManager.Instance.Notify("Disconnected", pollWWW.error );
				}
				else
				{

					debugText.text = "Poll Successfull: " + pollWWW.text;

					Debug.Log ("Poll Successfull: " + pollWWW.text);

					yield return 1;


					// if the player is not making a choice
					//if(gameState != GAME_STATE.MakingChoice)
					//{
						Debug.Log ("Poll Successfull: " + pollWWW.text);
						// change state to making choice
						//gameState = GAME_STATE.MakingChoice;

						//JSONObject obj = new JSONObject(pollWWW.text);
						//UtilityMethods.ReadJSONString(obj, ReadPlayerUpdate);

						ReadPlayerUpdate(pollWWW.text);
					//}
				}
			}

			//debugText.text = "Status: " + gameState;

			yield return new WaitForSeconds (POLL_DELAY);
		}
	}

	/// <summary>
	/// Retrieves choices from the server and then starts the round.
	/// </summary>
	private IEnumerator RetrieveChoices()
	{
		// ask the server for each choice and then set
		// the choice text

		yield return 1;

		// ask the server for the time left?

		yield return 1;

		// start a new round
		choiceSelected = -1;

	}

	private void ReadPlayerUpdate(string jsonString)
	{
		JSONNode infoNode = JSON.Parse (jsonString);

		// if not making a choice
		if( gameState != GAME_STATE.MakingChoice )
		{
			// if the player is currently waiting
			if(gameState == GAME_STATE.WaitingForStart)
			{
				// if the server is no longer waiting
				if( !infoNode["IsWaiting"].AsBool )
				{
					// start getting information
					gameState = GAME_STATE.GettingInfo;
				}
			}
			// otherwise
			else
			{
				// if the scenario has changed
				string newScenario = infoNode["ScenarioId"];
				Debug.Log ("Scenario: " + newScenario);
				if( !string.IsNullOrEmpty(newScenario) && currentScenario != newScenario )
				{
					// go back to making choices
					currentScenario = newScenario;
					ScreenUIManager.Instance.SwitchToScreen("ChoiceScreen");
					gameState = GAME_STATE.MakingChoice;
				}

				// set the title if it has changed
				string titleText = infoNode["Title"];
				if( !string.IsNullOrEmpty(titleText) && title.text != titleText )
					title.text = titleText;

				// grab the array of choices
				JSONArray choiceArray = infoNode["Choices"].AsArray;
				if( choiceArray != null )
				{
					for(int i = 0; i < 4; i++)
					{
						if( !string.IsNullOrEmpty(choiceArray[i]) )
						{
							choiceTextList[i].text = choiceArray[i];
						}
					}
				}
			}
		}

		// if the game needs to reset
		if( infoNode["IsReset"].AsBool )
		{
			// reset the game

			// set state to logging in
			gameState = GAME_STATE.LoggingIn;
			ScreenUIManager.Instance.SwitchToScreen("LoginScreen");
		}

		Debug.Log ("Is Alive: " + infoNode ["IsAlive"].AsBool);
	}
	
}
