using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyBehindPlayer : MonoBehaviour {
	public float MinX = 0.0f;

	private Transform playerTransform;
	private Transform thisTransform;
	void Awake () 
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		thisTransform = transform;
	}
	
	void Update () 
	{
		if(thisTransform.position.x < playerTransform.position.x + MinX)
			Destroy(gameObject);
	}
}
