using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugUI : MonoBehaviour {
	public AudioSource music;
	public AudioSource effects;

	public bool DisableMusic = false;
	public bool DisableEffects = false;
	public bool DisableIntro = false;
	Player p;

	public GUIStyle style;
	Intro introduction;
	void Awake()
	{
		introduction = GameObject.FindObjectOfType<Intro>();
		if(Application.isEditor)
		{
			if(introduction!=null)
				introduction.SkipIntro = DisableIntro;

			if(DisableMusic)
			{
				music.volume = 0.0f;
			} else {
				music.volume = 1.0f;
			}

			if(DisableEffects)
			{
				effects.volume = 0.0f;
			} else {
				effects.volume = 1.0f;
			}
		}
	}

	void Start () 
	{
		p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		Texture2D texture = new Texture2D(1,1);
		Color c = Color.black;
		c.a = .5f;
		texture.SetPixel(1,1,c);
		style.normal.background = texture;
	}
	
	void Update () 
	{
		
	}

	void OnGUI()
	{
		if(Application.isEditor)
		{
			if(DisableMusic)
			{
				music.volume = 0.0f;
			} else {
				music.volume = 1.0f;
			}
			
			if(DisableEffects)
			{
				effects.volume = 0.0f;
			} else {
				effects.volume = 1.0f;
			}

			if(p.IsJumping)
			{
				GUI.Label(new Rect(10, 10, 150, 20),"Player is Jumping",style);
			}	
			if(p.IsGrounded)
			{
				GUI.Label(new Rect(10, 35, 150, 20),"Player is Grounded",style);
			}	
		}
	}
}
