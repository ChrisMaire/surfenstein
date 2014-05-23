using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour {
	public List<GameObject> SpawnList;
	public float Chance;
	public bool Active = true;

	private float Y;
	private Transform thisTransform;
	void Awake () 
	{
		thisTransform = transform;
		Y = thisTransform.position.y;
	}
	
	void Update () 
	{
	
	}

	public void SpawnNew(float x)
	{
		if(!Active)
			return;

		float roll = Random.Range(0f,1f);
		if(roll < Chance)
		{
			GameObject toSpawn = SpawnList[Random.Range(0,SpawnList.Count)];
			toSpawn = Instantiate(toSpawn) as GameObject;
			Transform spawnedTransform = toSpawn.transform;
			Vector3 pos = spawnedTransform.position;
			pos.x = x;
			pos.y = Y;
			spawnedTransform.position = pos;
			spawnedTransform.parent = thisTransform;
		}
	}
}
