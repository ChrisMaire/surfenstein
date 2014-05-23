using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationHelper : MonoBehaviour {

	Announcer announcer;
	void Awake()
	{
		announcer = GameObject.FindObjectOfType<Announcer>();
	}

	public void AnnounceFlip()
	{
		announcer.AnnounceKickFlip();
	}

	public void AnnounceHandstand()
	{
		announcer.AnnounceHandstand();
	}

	public void AnnounceSuperman()
	{
		announcer.AnnounceSuperman();
	}
}
