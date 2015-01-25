using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Creates a UI list from a list of strings.
/// </summary>
public class SimpleUIList : MonoBehaviour 
{

	public float buttonHeight = 50;

	public string[] listItems;

	private void Start()
	{
		UpdateList ();
	}

	/// <summary>
	/// Reads the values within the string list to create a new UI list
	/// </summary>
	public void UpdateList()
	{
		// remove all existing list elements
		foreach(Transform listElement in this.transform)
			DestroyObject(listElement.gameObject);

		// grab the template to create items
		RectTransform itemTemplate = Resources.Load<RectTransform> ("UI/InventoryItem");
		
		float currentPosition = 0;
		foreach(string item in listItems)
		{
			RectTransform newButton = (RectTransform) Instantiate(itemTemplate);
			newButton.transform.parent = this.transform;
			
			newButton.offsetMin = new Vector2(0,-currentPosition-buttonHeight);
			newButton.offsetMax = new Vector2(0,-currentPosition);
			
			Text buttonText = newButton.GetComponentInChildren<Text>();
			if(buttonText)
				buttonText.text = item;
			
			currentPosition += buttonHeight;
		}
	}

}
