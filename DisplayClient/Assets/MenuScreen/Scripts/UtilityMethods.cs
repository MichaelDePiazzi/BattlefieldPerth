using UnityEngine;
using System.Collections;

/// <summary>
/// Holds a group od reusable methods.
/// </summary>
public class UtilityMethods : MonoBehaviour 
{
	
	public delegate void JSONCallback(string key, object item, int index);
	
	public static void ReadJSONString( JSONObject obj, JSONCallback callbackMethod, string key = null, int index = -1 )
	{
		switch(obj.type)
		{
		case JSONObject.Type.OBJECT:
			//Debug.Log ("Json Object: ");
			for(int i = 0;  i < obj.list.Count; i++)
			{
				key = (string) obj.keys[i];
				JSONObject childObject = (JSONObject) obj.list[i];
				
				ReadJSONString(childObject, callbackMethod, key, index);
			}
			break;
			
		case JSONObject.Type.ARRAY:
			int childIndex = 0;
			foreach( JSONObject childObject in obj.list)
			{
				ReadJSONString(childObject, callbackMethod, key, childIndex);
				childIndex ++;
			}
			break;
			
		case JSONObject.Type.STRING:
			callbackMethod( key, obj.str, index);
			break;
			
		case JSONObject.Type.NUMBER:
			callbackMethod( key, obj.n, index);
			break;
			
		case JSONObject.Type.BOOL:
			callbackMethod( key, obj.b, index);
			break;
			
		case JSONObject.Type.NULL:
			Debug.LogError("Null object found in Json file");
			break;
		}
	}
	
}