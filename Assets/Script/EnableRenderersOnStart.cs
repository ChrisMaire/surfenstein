using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnableRenderersOnStart : MonoBehaviour {

	void Start () 
	{
		foreach(Renderer r in GetComponentsInChildren<Renderer>())
		{
			r.enabled = true;
		}
		foreach(GUIText gt in GetComponentsInChildren<GUIText>())
		{
			gt.enabled = true;
		}
		foreach(GUITexture gtx in GetComponentsInChildren<GUITexture>())
		{
			gtx.enabled = true;
		}
	}
	
	void Update () 
	{
	
	}
}
