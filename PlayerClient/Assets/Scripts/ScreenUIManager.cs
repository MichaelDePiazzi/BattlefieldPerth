using UnityEngine;
using System.Collections;

/// <summary>
/// The ScreenUIManager is a central area for switching between UI screens.
/// </summary>
public class ScreenUIManager : MonoBehaviour 
{

	// --------------------------------------------------------
	// INSTANCING
	// --------------------------------------------------------
	
	private static ScreenUIManager _instance;
	
	public static ScreenUIManager Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType<ScreenUIManager>();
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
	// VARIABLES
	// --------------------------------------------------------


	/// <summary>
	/// The list of UI screens to switch between
	/// </summary>
	public GameObject[] screenList;





	// --------------------------------------------------------
	// METHODS
	// --------------------------------------------------------


	public void SwitchToScreen(string screenName)
	{
		Debug.Log ("Switching Screens: " + screenName);
		foreach(GameObject currentScreen in screenList)
		{
			// only set the screens with the screen name
			currentScreen.SetActive(currentScreen.name.ToLower() == screenName.ToLower());
		}
	}

}
