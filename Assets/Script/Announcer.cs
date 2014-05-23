using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Announcer : MonoBehaviour {
	public GameObject FrontFlipPrefab;
	public GameObject BackFlipPrefab;
	public GameObject KickFlipPrefab;
	public GameObject HandstandPrefab;
	public GameObject MonstrousPrefab;
	public GameObject SupermanPrefab;

	public Transform Parent;
	public float Spin = 30;
	private Transform thisTransform;

	private ScoreManager scoreDisplay;

	void Awake () 
	{
		scoreDisplay = GameObject.FindObjectOfType<ScoreManager>();
		thisTransform = transform;
	}
	
	void Update () 
	{
	
	}

	public void AnnounceFrontFlip()
	{
		scoreDisplay.AddTrick(Trick.Frontflip);
		Spawn(FrontFlipPrefab);
	}

	public void AnnounceBackFlip()
	{
		scoreDisplay.AddTrick(Trick.Backflip);
		Spawn(BackFlipPrefab);
	}

	public void AnnounceKickFlip()
	{
		scoreDisplay.AddTrick(Trick.Kickflip);
		Spawn(KickFlipPrefab);
	}

	public void AnnounceHandstand()
	{
		scoreDisplay.AddTrick(Trick.Handstand);
		Spawn(HandstandPrefab);
	}

	public void AnnounceSuperman()
	{
		scoreDisplay.AddTrick(Trick.Superman);
		Spawn(SupermanPrefab);
	}

	public void Monstrous()
	{
		Spawn(MonstrousPrefab);
	}

	public void EndCombo()
	{

	}

	public void Spawn(GameObject g)
	{
		GameObject toSpawn = g;
		toSpawn = Instantiate(toSpawn) as GameObject;
		Transform spawnedTransform = toSpawn.transform;
		Vector3 pos = thisTransform.localPosition;
		pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(.25f,.9f), 1, Camera.main.nearClipPlane));
		pos.z = -pos.z;
		//pos.x = Random.Range(10,20);//x,Camera.main.ViewportToWorldPoint(new Vector3(Screen.width,0)).x);
		//Debug.Log(pos.x);
		spawnedTransform.localPosition = pos;
		spawnedTransform.parent = thisTransform;

		Rigidbody2D body = spawnedTransform.rigidbody2D;
		float roll = Random.Range(-1f,1f);
		body.AddTorque(roll > 0 ? Spin : -Spin);
	}
}
