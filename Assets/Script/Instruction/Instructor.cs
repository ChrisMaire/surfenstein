using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Instructor : MonoBehaviour {
	public List<Instruction> elements;
	public int remaining;
	public bool ShowInstructions = true;
	
	void Awake()
	{
		remaining = elements.Count;
	}

	void Start() 
	{
		if(!ShowInstructions)
		{
			remaining = 0;
		}
		for(int i = 0 ; i < elements.Count ; i++)
		{
			if(i < elements.Count - 1)
				elements[i].Next = elements[i+1];
			elements[i].OnDestroy += () => remaining--;
		}

	}

	public void StartInstruction()
	{
		elements[0].Activate();
	}
	
	void Update() 
	{
		if(remaining <= 0)
		{
			Destroy(gameObject);
		}
	}
}
