using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {
	public string HighScoreString;
	public string ScoreString;
	public string ComboString;
	public Vector2 HighScoreStringPos;
	public Vector2 ScoreStringPos;
	public Vector2 ComboStringPos;

	public GUIStyle ScoreStyle;
	public GUIStyle ComboStyle;

	public Dictionary<string, int> Combo;

	public int BackflipValue;
	public int FrontflipValue;
	public int HandstandValue;
	public int KickflipValue;

	public Announcer announcer;

	public bool Comboing = false;

	public int MonstrousThreshold = 1500;

	private int score;
	private int highScore;

	private Vector2 highscoreStringPos;
	private Rect highscoreStringRect;
	private Vector2 scoreStringPos;
	private Rect scoreStringRect;
	private Vector2 comboStringPos;
	private Rect comboStringRect;
	private string[] tricks;
	void Start () 
	{
		score = 0;
		highScore = 0;

		Combo = new Dictionary<string, int>();
		Trick[] tempTricks = (Trick[])System.Enum.GetValues(typeof(Trick));
		tricks = new string[tempTricks.Length];
		for(int i = 0 ; i < tricks.Length ; i++)
		{
			tricks[i] = tempTricks[i].ToString();
			Combo.Add(tricks[i],0);
		}

		UpdateStrings();
	}
	
	void Update () 
	{
	
	}

	public void AddTrick(Trick t)
	{
		Comboing = true;
		Combo[t.ToString()] += 1;
		UpdateStrings();
	}

	private void UpdateStrings()
	{
		ScoreString = score.ToString();

		//I'm told that linq is slow in mono so I'm doing it iteratively instead.
		bool first = true;
		ComboString = "";
		foreach(KeyValuePair<string,int> kvp in Combo)
		{
			if(kvp.Value > 0)
			{
				if(first)
				{
					ComboString += kvp.Value + " x " + kvp.Key;
					first = false;
				} else {
					ComboString += " + " + kvp.Value + " x " + kvp.Key;
				}
			}
		}
	}

	public void Reset()
	{
		ResetCombo();
		score = 0;
		UpdateStrings();
	}

	public void EndCombo()
	{
		if(!Comboing)
			return;

		Comboing = false;

		//Debug.Log("Ending Combo");
		int addlScore = GetScore();
		score += addlScore;

		if(addlScore > MonstrousThreshold)
		{
			announcer.Monstrous();
		}

		if(highScore < score)
			highScore = score;

		ResetCombo();
	}

	public void ResetCombo()
	{
		foreach(string t in tricks)
		{
			Combo[t] = 0;
		}
		StartCoroutine(FadeComboString());
	}

	//dont worry little method, you'll do real math when you grow up
	public int GetScore()
	{
		int scoreOut = 0;
		int count = 0;
		foreach(string t in tricks)
		{
			//Debug.Log("scored "+Combo[t]+" for trick" + t);
			scoreOut += 100 * Combo[t];
			if(Combo[t] > 0)
				count++;
		}
		scoreOut *= count;
		return scoreOut;
	}

	public void DrawHighScore()
	{
		highscoreStringRect = GUILayoutUtility.GetRect(new GUIContent(HighScoreString),ScoreStyle);
		highscoreStringPos = new Vector2((Screen.width * HighScoreStringPos.x) - (highscoreStringRect.x/2), (Screen.height * (1-HighScoreStringPos.y)) + (highscoreStringRect.y/2));
		highscoreStringRect.center = highscoreStringPos;
		GUI.Label(highscoreStringRect, "High Score: "+highScore, ScoreStyle);
	}

	public void DrawScore()
	{
		scoreStringRect = GUILayoutUtility.GetRect(new GUIContent(ScoreString),ScoreStyle);
		scoreStringPos = new Vector2((Screen.width * ScoreStringPos.x) - (scoreStringRect.x/2), (Screen.height * (1-ScoreStringPos.y)) + (scoreStringRect.y/2));
		scoreStringRect.center = scoreStringPos;
		GUI.Label(scoreStringRect, ScoreString, ScoreStyle);
	}

	public void DrawCombo()
	{
		comboStringRect = GUILayoutUtility.GetRect(new GUIContent(ComboString),ScoreStyle);
		comboStringPos = new Vector2((Screen.width * ComboStringPos.x) - (comboStringRect.x/2), (Screen.height * (1-ComboStringPos.y)) + (comboStringRect.y/2));
		comboStringRect.center = comboStringPos;
		GUI.Label(comboStringRect, ComboString, ComboStyle);
	}

	//I don't really like how much math this is doing each tick...we should calculate the rect size once and only change
	//it later if the text has changed. 
	void OnGUI()
	{
		DrawScore();
		DrawHighScore();
		if(ComboString != "")
			DrawCombo();
	}

	IEnumerator FadeComboString() {
		for (float f = 1.0f; f >=0f; f -= 0.025f) {//-.5f; f -= 0.1f) {
			Color c = ComboStyle.normal.textColor;
			c.a = f;
			
			ComboStyle.normal.textColor = c;
			yield return 0;
		}
		UpdateStrings();
		Color col = ComboStyle.normal.textColor;
		col.a = 1.0f;
		ComboStyle.normal.textColor = col;

		yield break;
	}
}
