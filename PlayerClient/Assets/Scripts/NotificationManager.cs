using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Displays a message to the player if something goes wrong.
/// </summary>
public class NotificationManager : MonoBehaviour 
{

	// --------------------------------------------------------
	// INSTANCING
	// --------------------------------------------------------

	private static NotificationManager _instance;

	public static NotificationManager Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType<NotificationManager>();
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
	// PUBLIC VARIABLES
	// --------------------------------------------------------

	public GameObject notificationContainer;

	public Text notificationTitle;
	public Text notificationDescription;




	// --------------------------------------------------------
	// METHODS
	// --------------------------------------------------------

	public void Notify(string title, string description)
	{
		this.notificationTitle.text = title;
		this.notificationDescription.text = description;
		this.notificationContainer.SetActive (true);
	}

}
