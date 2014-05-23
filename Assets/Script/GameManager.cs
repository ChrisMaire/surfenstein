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
	private static GameObject obstacles;

	void Awake () 
	{
		effects = GameObject.FindObjectOfType<EffectPlayer>();
		pieces = GameObject.FindObjectsOfType<MoveForward>().ToList();
		player = GameObject.FindObjectOfType<Player>();
		music = GetComponent<MusicManager>();
		score = GetComponent<ScoreManager>();
		instructor = GameObject.FindObjectOfType<Instructor>();
		obstacles = GameObject.Find("Obstacle");
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

		List<GameObject> o = new List<GameObject>();
		foreach(Transform t in obstacles.transform)
		{
			o.Add(t.gameObject);
		}
		foreach(GameObject g in o)
		{
			Destroy(g);
		}

		foreach(MoveForward m in pieces)
			m.Reset();

		player.Reset();
	}
}
