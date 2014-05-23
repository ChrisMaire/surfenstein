using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectPlayer : MonoBehaviour {
	public AudioSource EffectAudioSource;
	public AudioClip ResetNoise;

	void Start () 
	{
	}
	
	void Update () 
	{
	
	}

	public void PlayReset()
	{
		EffectAudioSource.PlayOneShot(ResetNoise);
		//EffectAudioSource.clip = ResetNoise;
	}
}
