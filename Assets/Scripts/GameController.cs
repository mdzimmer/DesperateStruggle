using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utils;

public class GameController : MonoBehaviour {
	public int fieldWidth = 31;
	public int fieldHeight = 13;
	Dictionary<Offset, Tile> field;
	GameObject tileParent;
	EnemyController ec;

	// Use this for initialization
	void Start () {
		field = new Dictionary<Offset, Tile>();
		tileParent = GameObject.Find("_Tiles");
		BuildField();
		ClearEntrance();
		ec = GetComponent<EnemyController>();
		ec.CustomStart(this);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SpawnUnit(UnitType unitType, Vector3 location) {
		string prefabString = "";
		location = Util.TileCenter(location);
		switch(unitType) {
			case UnitType.BLUEHERO:
			prefabString = "Prefabs/BlueHero";
			break;
			case UnitType.GREENHERO:
			prefabString = "Prefabs/GreenHero";
			break;
			case UnitType.ORANGEHERO:
			prefabString = "Prefabs/OrangeHero";
			break;
			case UnitType.PURPLEHERO:
			prefabString = "Prefabs/PurpleHero";
			break;
			case UnitType.BLACKHERO:
			prefabString = "Prefabs/BlackHero";
			break;
			case UnitType.BLUESQUIRE:
			prefabString = "Prefabs/BlueSquire";
			break;
			case UnitType.GREENSQUIRE:
			prefabString = "Prefabs/GreenSquire";
			break;
			case UnitType.ORANGESQUIRE:
			prefabString = "Prefabs/OrangeSquire";
			break;
			case UnitType.PURPLESQUIRE:
			prefabString = "Prefabs/PurpleSquire";
			break;
			case UnitType.BLACKSQUIRE:
			prefabString = "Prefabs/BlackSquire";
			break;
		}
		GameObject prefab = (GameObject)Resources.Load(prefabString);
		Instantiate(prefab, location, Quaternion.identity);
	}

	public List<Tile> GetTiles(List<Offset> offsets) {
		List<Tile> tiles = new List<Tile>();
		foreach(Offset offset in offsets) {
			if (field.ContainsKey(offset)) {
				tiles.Add(field[offset]);
			}
		}
		return tiles;
	}

	public Tile GetTile(Offset offset) {
		if (field.ContainsKey(offset)) {
			return field[offset];
		}
		return null;
	}

	// public List<Tile> GetNeighbors(Offset offset) {
	// 	List<Tile> neighbors = new List<Tile>();
	// 	Cube cube = Util.OffsetToCube(offset);
	// 	List<Cube> cubes = new List<Cube>();
	// 	cubes.Add(new Cube(cube.x + 1, cube.y - 1, cube.z + 0));
	// 	cubes.Add(new Cube(cube.x + 1, cube.y + 0, cube.z - 1));
	// 	cubes.Add(new Cube(cube.x + 0, cube.y + 1, cube.z - 1));
	// 	cubes.Add(new Cube(cube.x - 1, cube.y + 1, cube.z + 0));
	// 	cubes.Add(new Cube(cube.x - 1, cube.y + 0, cube.z + 1));
	// 	cubes.Add(new Cube(cube.x + 0, cube.y - 1, cube.z + 1));
	// 	foreach(Cube _cube in cubes) {
	// 		Offset _offset = Util.CubeToOffset(_cube);
	// 		if (field.ContainsKey(_offset)) {
	// 			neighbors.Add(field[_offset]);
	// 		}
	// 	}
	// 	return neighbors;
	// }

	public Neighbors GetNeighbors(Offset offset) {
		// List<Tile> neighbors = new List<Tile>();
		Neighbors neighbors = new Neighbors();
		Cube cube = Util.OffsetToCube(offset);
		// List<Cube> cubes = new List<Cube>();
		Offset u = Util.CubeToOffset(new Cube(cube.x + 1, cube.y + 1, cube.z - 1));
		Offset ul = Util.CubeToOffset(new Cube(cube.x - 1, cube.y + 1, cube.z + 0));
		Offset ur = Util.CubeToOffset(new Cube(cube.x + 1, cube.y + 0, cube.z - 1));
		// Offset l = Util.CubeToOffset(new Cube(cube.x + 0, cube.y + 1, cube.z - 1));
		// Offset r = Util.CubeToOffset(new Cube(cube.x - 1, cube.y + 1, cube.z + 0));
		Offset bl = Util.CubeToOffset(new Cube(cube.x - 1, cube.y + 0, cube.z + 1));
		Offset br = Util.CubeToOffset(new Cube(cube.x + 1, cube.y - 1, cube.z + 0));
		Offset b = Util.CubeToOffset(new Cube(cube.x + 0, cube.y - 1, cube.z + 1));
		// cubes.Add(new Cube(cube.x + 1, cube.y + 0, cube.z - 1));
		// cubes.Add(new Cube(cube.x + 0, cube.y + 1, cube.z - 1));
		// cubes.Add(new Cube(cube.x - 1, cube.y + 1, cube.z + 0));
		// cubes.Add(new Cube(cube.x - 1, cube.y + 0, cube.z + 1));
		// cubes.Add(new Cube(cube.x + 0, cube.y - 1, cube.z + 1));
		// foreach(Cube _cube in cubes) {
		// 	Offset _offset = Util.CubeToOffset(_cube);
		// 	if (field.ContainsKey(_offset)) {
		// 		neighbors.Add(field[_offset]);
		// 	}
		// }
		neighbors.u = GetTile(u);
		neighbors.ul = GetTile(ul);
		neighbors.ur = GetTile(ur);
		// neighbors.l = GetTile(l);
		// neighbors.r = GetTile(r);
		neighbors.bl = GetTile(bl);
		neighbors.br = GetTile(br);
		neighbors.b = GetTile(b);
		return neighbors;
	}

	private void BuildField() {
		GameObject tilePrefab = (GameObject)(Resources.Load("Prefabs/Tile", typeof(GameObject)));
		for(int i = 0; i < fieldWidth; i++) {
			for(int j = 0; j < fieldHeight; j++) {
				Offset offset = new Offset(i, j);
				Point point = Util.OffsetToPoint(offset);
				Vector3 v3 = Util.PointToVector3(point);
				v3.y *= -1f;
				GameObject tileGO = (GameObject)Instantiate(tilePrefab, v3, Quaternion.identity);
				tileGO.transform.parent = tileParent.transform;
				Tile tile = tileGO.GetComponent<Tile>();
				tile.offset = offset;
				// if (Random.Range(0f, 1f) < 0.60f) {
				// 	tile.groundTileType = GroundTile.Type.LOW;
				// } else {
				// 	tile.groundTileType = GroundTile.Type.HIGH;
				// }
				field[offset] = tile;
			}
		}
	}

	private void ClearEntrance() {
		int halfway = fieldWidth / 2;
		for(int i = halfway - 1; i <= halfway + 1; i++) {
			Offset offset = new Offset(i, 0);
			Tile tile = field[offset];
			tile.groundTileType = GroundTile.Type.LOW;
			Offset offset2 = new Offset(i, fieldHeight - 1);
			Tile tile2 = field[offset2];
			tile2.groundTileType = GroundTile.Type.LOW;
		}
	}

	public enum UnitType {
		BLUEHERO,
		GREENHERO,
		ORANGEHERO,
		PURPLEHERO,
		BLACKHERO,
		BLUESQUIRE,
		GREENSQUIRE,
		ORANGESQUIRE,
		PURPLESQUIRE,
		BLACKSQUIRE
	}
}
