using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
	private static MusicManager music;
	private static EffectPlayer effects;

	private static List<MoveForward> pieces;
	private static Player player;
	private static ScoreManager score;
	private static Instructor instructor;
	void Awake () 
	{
		effects = GameObject.FindObjectOfType<EffectPlayer>();
		pieces = GameObject.FindObjectsOfType<MoveForward>().ToList();
		player = GameObject.FindObjectOfType<Player>();
		music = GetComponent<MusicManager>();
		score = GetComponent<ScoreManager>();
		instructor = GameObject.FindObjectOfType<Instructor>();
	}
	
	void Update () 
	{
	
	}

	public static void StartGame()
	{
		music.StartMusic();
		instructor.StartInstruction();
		player.Reset();
	}

	public static void Reset()
	{
		effects.PlayReset();

		score.Reset();

		foreach(MoveForward m in pieces)
			m.Reset();

		player.Reset();
	}
}
