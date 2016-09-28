using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

public class EnemyController : MonoBehaviour {
	GameController gc;
	List<Tile> entryTiles;
	GameObject enemyPrefab;
	float enemyOffscreenStart = -1f;

	// Use this for initialization
	void Start () {
		// gc = GetComponent<GameController>();
		// openTiles = new List<Tile>();
		// GetEntryTiles();
		enemyPrefab = (GameObject)Resources.Load("Prefabs/Enemy", typeof(GameObject));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Tile goalTile = entryTiles[Random.Range(0, entryTiles.Count)];
			Vector3 spawnPoint = goalTile.transform.position;
			spawnPoint.y -= enemyOffscreenStart;
			GameObject enemyGO = (GameObject)Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
			Enemy enemy = enemyGO.GetComponent<Enemy>();
			enemy.goal = goalTile;
			enemy.gc = gc;
		}
	}

	public void CustomStart(GameController _gc) {
		gc = _gc;
		GetEntryTiles();
	}

	private void GetEntryTiles() {
		entryTiles = new List<Tile>();
		List<Offset> offsets = new List<Offset>();
		for (int i = 0; i < gc.fieldWidth; i++) {
			offsets.Add(new Offset(i, 0));
		}
		//
		// foreach(Offset offset in offsets) {
		// 	print(offset);
		// }
		//
		List<Tile> tilesGot = gc.GetTiles(offsets);
		foreach(Tile tile in tilesGot) {
			if (tile.groundTileType == GroundTile.Type.LOW) {
				entryTiles.Add(tile);
			}
		}
		//
		// foreach(Tile tile in entryTiles){
		// 	print(tile.offset);
		// }
	}
}
