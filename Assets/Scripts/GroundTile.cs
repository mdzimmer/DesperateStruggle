using UnityEngine;
using System.Collections;

public class GroundTile : MonoBehaviour {
	SpriteRenderer sr;
	Type type;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		switch(type) {
			case Type.HIGH:
			sr.sprite = (Sprite)Resources.Load("Sprites/Dirt", typeof(Sprite));
			break;
			case Type.LOW:
			sr.sprite = (Sprite)Resources.Load("Sprites/Grass", typeof(Sprite));
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetType(Type _type) {
		type = _type;
	}

	public enum Type {
		HIGH,
		LOW
	}
}
