using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GroundTile gt = null;
		foreach(Transform child in transform) {
			GroundTile _gt = child.GetComponent<GroundTile>();
			if (_gt) {
				gt = _gt;
				continue;
			}
		}
		if (Random.Range(0f, 1f) < 0.60f) {
			gt.SetType(GroundTile.Type.LOW);
		} else {
			gt.SetType(GroundTile.Type.HIGH);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
