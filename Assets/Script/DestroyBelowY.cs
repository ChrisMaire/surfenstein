using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyBelowY : MonoBehaviour {
	public float MinY = 0.0f;

	private Transform thisTransform;
	void Awake () 
	{
		thisTransform = transform;
	}
	
	void Update () 
	{
		if(thisTransform.position.y < MinY)
			Destroy(gameObject);
	}
}
