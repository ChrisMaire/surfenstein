using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour {
	public AudioSource MusicSource;

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

	public void StartMusic()
	{
		if(!MusicSource.isPlaying)
		{
			MusicSource.loop = true;
			MusicSource.Play();
		}
	}
}
