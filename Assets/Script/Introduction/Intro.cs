using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Intro : MonoBehaviour {
	public List<IntroElement> elements;
	public int remaining;
	public bool SkipIntro = false;
	
	private Player player;
	void Awake()
	{
		remaining = elements.Count;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void Start() 
	{
		if(SkipIntro)
		{
			remaining = 0;
		}
		for(int i = 0 ; i < elements.Count ; i++)
		{
			if(i < elements.Count - 1)
				elements[i].Next = elements[i+1];
			elements[i].OnDestroy += () => remaining--;
		}
		elements[0].Activate();
	}
	
	void Update() 
	{
		if(remaining <= 0)
		{
			GameManager.StartGame();
			Destroy(gameObject);
		}
	}
}
