using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//the 'shift' value used here is kind of a hack. rather than moving by that shift amount, this should
//calculate the width of the renderer (using renderer.bounds) and shift by that * number of elements.
//that wasn't working for some reason...possibly need to multiply it by scale?

//also rather than this component, there probably should be a bigger class managing a list of transforms 
public class MoveForward : MonoBehaviour {
	public int NumObjects = 3;
	public float Shift;
	public bool CanSpawn;
	public bool SpawnOnStart;

	private Transform player;
	private Transform thisTransform;
	private ObstacleSpawner spawner;

	private Vector3 startPosition;

	void Start () 
	{
		spawner = GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		thisTransform = transform;
		startPosition = thisTransform.position;

		if(SpawnOnStart)
		{
			spawner.SpawnNew(thisTransform.position.x);
		}
	}
	
	void FixedUpdate () 
	{
		if(player.position.x > (thisTransform.position.x + (Shift*2)))
		{
			Vector3 newPosition = thisTransform.localPosition;
			newPosition.x += ((NumObjects) * Shift);
			thisTransform.localPosition = newPosition;
			if(CanSpawn)
			{
				//Debug.Log("spawned");
				spawner.SpawnNew(thisTransform.position.x);
			}
		}
	}

	public void Reset()
	{
		thisTransform.position = startPosition;
	}
}
