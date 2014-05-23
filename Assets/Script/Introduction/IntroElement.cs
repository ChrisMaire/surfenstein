using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntroElement : MonoBehaviour {
	public enum RemoveType
	{
		Fade,
		Slide,
		Disappear
	}
	public RemoveType ElementType;
	public float NextStep = -1f;
	public float SlideSpeed = 1f;
	public IntroElement Next;
	public System.Action OnDestroy;

	private bool IsActive = false;
	private GUIText text;
	private GUITexture texture;
	private Transform thisTransform;
	void Awake () 
	{
		thisTransform = transform;
		if(guiText != null)
			text = guiText;
		if(guiTexture != null)
			texture = guiTexture;
	}

	void Start()
	{
	}
	
	void Update () 
	{
	}

	public void Activate()
	{
		if(!IsActive)
		{
			IsActive = true;
			//Debug.Log(gameObject.name);
			switch(ElementType)
			{
				case RemoveType.Fade:
					StartCoroutine(Fade());
					break;
				case RemoveType.Slide:
					StartCoroutine(Slide());
					break;
				case RemoveType.Disappear:
					Invoke("DestroySelf",NextStep);
					break;
			}
		}
	}
	
	IEnumerator Fade() {
		for (float f = 1.0f; f >=0f; f -= 0.1f) {//-.5f; f -= 0.1f) {
			Color c = GetElementColor();
			c.a = f;

			SetElementColor(c);
			yield return 0;
		}

		if(Next!= null)
			Next.Invoke("Activate",NextStep);
		Invoke("DestroySelf",NextStep);
		yield break;
	}

	IEnumerator Slide() {
		while(thisTransform.position.x < 1.5) {
			Vector3 position = thisTransform.localPosition;
			position.x += SlideSpeed;
			thisTransform.localPosition = position;

			yield return 0;
		}
		if(Next != null)
			Next.Invoke("Activate",NextStep);
		Invoke("DestroySelf",NextStep);
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
}
