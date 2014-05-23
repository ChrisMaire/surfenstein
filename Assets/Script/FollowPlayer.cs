using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	private Player player;
	private Vector3 relativePostion;
	private Transform thisTransform;

	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		thisTransform = transform;
		relativePostion = thisTransform.position;
	}

	
	void Update () {
		Vector3 newPos = player.Position + relativePostion;
		newPos.y = relativePostion.y;
		thisTransform.position = newPos;
	}
}
