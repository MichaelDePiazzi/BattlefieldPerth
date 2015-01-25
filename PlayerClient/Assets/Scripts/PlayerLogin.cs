using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;

public class PlayerLogin : MonoBehaviour 
{

	// --------------------------------------------------------
	// PUBLIC VARIABLES
	// --------------------------------------------------------


	public InputField playerName;
	public Text errorText;

	/// <summary>
	/// The loading text that will give a message to the player while loading.
	/// </summary>
	public Text loadingText;

	/// <summary>
	/// Used to send debug messages on screen.
	/// </summary>
	public Text debugText;





	// --------------------------------------------------------
	// METHODS
	// --------------------------------------------------------

	public void Login()
	{
		bool errorFound = false;

		// clear error fields
		errorText.text = "";

		// if the name field is empty
		if( string.IsNullOrEmpty(playerName.text))
		{
			// send error to player
			errorFound = true;
			errorText.text = "*Type in your name";
		}

		// if nore errors were found
		if(!errorFound)
		{
			// send login to server
			StartCoroutine(ServerLogin());

			// open loading screen
			ScreenUIManager.Instance.SwitchToScreen("LoadingScreen");
			loadingText.text = "Logging In...";
		}
	}

	private IEnumerator ServerLogin()
	{
		string url = ChoiceManager.SERVER_URL + "api/requestjoin/?playerName=" + playerName.text;

		debugText.text = "logging in..." + url;

		Debug.Log ("logging in..." + url);
		WWW loginWWW = new WWW (url);
		yield return loginWWW;

		Debug.Log ("response received");

		// if there was an error
		if(loginWWW.error != null)
		{
			Debug.LogError("Log in Error: " + loginWWW.error);

			// go back to login screen
			ScreenUIManager.Instance.SwitchToScreen("LoginScreen");

			NotificationManager.Instance.Notify( "Could not connect", loginWWW.error );
		}
		// otherwise
		else
		{
			// check the log in data
			Debug.Log(loginWWW.text);
			//JSONObject obj = new JSONObject(loginWWW.text);
			//UtilityMethods.ReadJSONString(obj, ReadJSONObject);

			// load the JSON string
			JSONNode infoNode = JSON.Parse(loginWWW.text);

			// if the player's number is valid
			if( infoNode["PlayerValid"].AsBool )
			{
				// then let the ChoiceManager know that the player is now logged in
				ChoiceManager.Instance.LoggedIn( infoNode["PlayerNumber"].AsInt );
			}
			// otherwise
			else
			{
				// display a message to the player
				NotificationManager.Instance.Notify( "Could not connect", infoNode["PlayerMessage"] );
			}



			// change the loading message
			ScreenUIManager.Instance.SwitchToScreen("LoadingScreen");
			loadingText.text = "Waiting For\nOther Players...";
		}
	}

}
