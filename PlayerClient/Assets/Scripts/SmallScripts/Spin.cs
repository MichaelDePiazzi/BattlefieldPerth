using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour 
{

	public Vector3 spinVector;

	private void Update()
	{
		this.transform.Rotate (spinVector * Time.deltaTime);
	}

}
