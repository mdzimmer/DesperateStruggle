using UnityEngine;
using System.Collections;
using Utils;

public class Tile : MonoBehaviour {
	public GroundTile gt;
	public GroundTile.Type groundTileType = GroundTile.Type.NONE;
	public Offset offset;

	// Use this for initialization
	void Start () {
		if (groundTileType == GroundTile.Type.NONE) {
			if (Random.Range(0f, 1f) < 0.60f) {
				groundTileType = GroundTile.Type.LOW;
			} else {
				groundTileType = GroundTile.Type.HIGH;
			}
		}
		InstantiateGroundTile();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeGroundType(GroundTile.Type type) {
		Destroy(transform.GetChild(0).gameObject);
		groundTileType = type;
		InstantiateGroundTile();
	}

	private void InstantiateGroundTile() {
		string resource = "";
		switch(groundTileType) {
			case GroundTile.Type.HIGH:
			resource = "Prefabs/HighTile";
			break;
			case GroundTile.Type.LOW:
			resource = "Prefabs/LowTile";
			break;
		}
		GameObject prefabGO = (GameObject)Resources.Load(resource, typeof(GameObject));
		GameObject groundTileGO = (GameObject)Instantiate(prefabGO);
		groundTileGO.transform.parent = this.transform;
		groundTileGO.transform.localPosition = Vector3.zero;
		GroundTile groundTile = groundTileGO.GetComponent<GroundTile>();
		groundTile.Initialize(this);
	}
}
