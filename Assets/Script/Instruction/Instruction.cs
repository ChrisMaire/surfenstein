using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Instruction : MonoBehaviour {
	public Instruction Next;
	public System.Action OnDestroy;
	public string[] Keys;
	public bool PlayerInMidair;

	private bool IsActive = false;
	private GUIText text;
	private GUITexture texture;
	private Player player;
	void Awake () 
	{
		player = GameObject.FindObjectOfType<Player>();
		if(guiText != null)
			text = guiText;
		if(guiTexture != null)
			texture = guiTexture;

		Deactivate();
	}

	void Start()
	{
	}
	
	void Update () 
	{
		if(IsActive)
		{
			if(Passes())
			{
				DestroySelf();
				if(Next != null)
					Next.Activate();
			}
		}
	}

	public void Activate()
	{
		if(!IsActive)
		{
			Show();
			IsActive = true;
			StartCoroutine(Blink());
		}
	}

	public void Deactivate()
	{
		IsActive = false;
		Hide();
	}

	public bool Passes()
	{
		if(PlayerInMidair && player.IsGrounded)
			return false;

		foreach(string k in Keys)
		{
			if(!Input.GetButton(k))
			{
				return false;
			}
		}
		return true;
	}
	
	private IEnumerator Blink(){
		bool blinkSwitch = true;
		while(IsActive)
		{
			if(blinkSwitch)
			{
				for (float f = 1.0f; f >= -.5f; f -= 0.025f) {
					Color c = GetElementColor();
					c.a = f;
					
					SetElementColor(c);
					yield return 0;
				}
			} else {
				for (float f = 0.0f; f <= -.5f; f += 0.025f) {
					Color c = GetElementColor();
					c.a = f;
					
					SetElementColor(c);
					yield return 0;
				}
			}
			blinkSwitch = !blinkSwitch;
		}

		if(Next!= null)
			Next.Activate();
		DestroySelf();
		yield break;
	}

	void DestroySelf()
	{
		if(OnDestroy != null)
			OnDestroy();
		Destroy(gameObject);
	}

	Color GetElementColor()
	{
		if(text != null)
			return text.color;
		if(texture != null)
			return texture.color;
		return Color.white;
	}

	void SetElementColor(Color c)
	{
		if(text != null)
			text.color = c;
		if(texture != null)
			texture.color = c;
	}

	void Show()
	{
		if(text != null)
			text.enabled = true;
		if(texture != null)
			texture.enabled = true;
	}

	void Hide()
	{
		if(text != null)
			text.enabled = false;
		if(texture != null)
			texture.enabled = false;
	}
}
