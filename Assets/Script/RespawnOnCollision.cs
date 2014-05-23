using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RespawnOnCollision : MonoBehaviour {
	public PhysicsMaterial2D[] collideWith;
	private PhysicsMaterial2D playerMaterial;
	private Player player;
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		playerMaterial = player.PlayerMaterial;
	}
	
	void Update () 
	{
	
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collideWith.Any(c => c == collision.collider.sharedMaterial))
		{
			GameManager.Reset();
		}
	}
}
