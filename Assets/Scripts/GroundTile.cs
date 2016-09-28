using UnityEngine;
using System.Collections;
using Utils;

public class GroundTile : MonoBehaviour {
	public int spriteLayerModfifier = 0;
	public GameObject centerPoint;

	SpriteRenderer sr;
	Tile tile;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(Tile _tile) {
		sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
		centerPoint = transform.GetChild(1).gameObject;
		tile = _tile;
		Offset offset = tile.offset;
		int indent = offset.col % 2;
		sr.sortingOrder = offset.row * 2 + indent + spriteLayerModfifier;
	}

	public enum Type {
		HIGH,
		LOW,
		NONE
	}
}
